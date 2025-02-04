﻿using MachineAssetTrackerAPI.Models;
using MachineAssetTrackerAPI.Data;


public class DataLoader : IHostedService
{
    private readonly MachineAssetData _machineAssests = new MachineAssetData();
    private readonly MachineData _machines = new MachineData();
    private readonly AssetData _assets = new AssetData();

    private readonly ILogger<DataLoader> _logger;
    string FilePath = Environment.GetEnvironmentVariable("MATRIX_FILE_PATH") ?? "/app/matrix.txt";

    public DataLoader(MachineAssetData mongoDbContext, ILogger<DataLoader> logger) 
    {
        _machineAssests = mongoDbContext;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting data loader service...");

        if (File.Exists(FilePath))
        {
            //Adding the data to in MachineAsset collection
            var machineAssets = File.ReadAllLines(FilePath)
                .Select(line => line.Split(','))
                .Where(parts => parts.Length == 3)
                .Select(parts => new MachineAsset
                {
                    MachineType = parts[0].Trim(),
                    
                    Asset = parts[1].Trim(),
                    Series = parts[2].Trim()
                }).ToList();
            if(machineAssets.Count == 0)
            {
                throw new ArgumentException($"No data found in file {FilePath}!");
            }
             _machineAssests.InsertMany(machineAssets);  // Using the injected MongoDBContext
            _logger.LogInformation("Machine asset data successfully loaded into MongoDB.");

            //Adding the data to in Machine collection
            var machines = machineAssets
                        .GroupBy(ma => ma.MachineType)
                        .Select(g => new Machine
                        {
                            MachineType = g.Key,
                            Assets = g.Select(ma => new Asset
                            {
                                AssetName = ma.Asset,
                                Series = new List<string> { ma.Series }  
                            }).ToList()
                        }).ToList();
            _machines.InsertMany(machines);

            //Adding the data to asset collection
            var assets = machineAssets
                .GroupBy(ma => ma.Asset)
                .Select(g => new Asset
                {
                    AssetName = g.Key,
                    Series = g.Select(ma => ma.Series).ToList()
                }).ToList();
            _assets.InsertMany(assets);


        }
        else
        {
            throw new ArgumentException($"File {FilePath} not found!");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping data loader service...");
        return Task.CompletedTask;
    }
}