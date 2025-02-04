﻿using MachineAssetTrackerAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MachineAssetTrackerAPI.Controllers
{
    [Route("api/machineassets")]
    [ApiController]
    public class MachineAssetsController : ControllerBase
    {
        private readonly IMachineAssetsService _machineAssetsService;
        public MachineAssetsController(IMachineAssetsService machineAssetsService)
        {
            _machineAssetsService = machineAssetsService;
        }

        /// <summary>
        /// Get All Assets
        /// </summary>
        /// <returns>Returns a list</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var machineAssets = _machineAssetsService.GetAll();
            if (machineAssets.Count == 0 || machineAssets == null)
            {
                return NotFound();
            }
            return Ok(_machineAssetsService.GetAll());
        }

        /// <summary>
        /// Get Assets By MachineType
        /// </summary>
        /// <param name="machineType"></param>
        /// <returns>List Of Asset for Provided machine type</returns>
        [HttpGet("byMachineType/{machineType}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAssetsByMachineType(string machineType)
        {
            var machineAssets = _machineAssetsService.GetAssetsByMachineType(machineType);
            if (machineAssets.Count == 0 || machineAssets == null)
            {
                return NotFound();
            }
            return Ok(_machineAssetsService.GetAssetsByMachineType(machineType));
        }

        /// <summary>
        /// Get Machines By Asset
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns>List Of machines which using given Asset</returns>
        [HttpGet("byAssetName/{assetName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMachinesByAsset(string assetName)
        {
            var machineAssets = _machineAssetsService.GetMachinesByAsset(assetName);
            if (machineAssets.Count == 0 || machineAssets == null)
            {
                return NotFound();
            }
            return Ok(machineAssets);
        }

        /// <summary>
        /// Get Machines Using Latest Series
        /// </summary>
        /// <returns>Returns machine which are using latest assets</returns>
        [HttpGet("GetMachinesUsingLatestSeries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMachinesUsingLatestSeries()
        {
            var machineAssets = _machineAssetsService.GetMachinesUsingLatestSeries();
            if (machineAssets.Count == 0 || machineAssets == null)
            {
                return NotFound();
            }
            return Ok(_machineAssetsService.GetMachinesUsingLatestSeries());
        }
    }
}
