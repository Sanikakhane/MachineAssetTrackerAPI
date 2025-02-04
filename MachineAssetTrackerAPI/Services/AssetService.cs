using MachineAssetTrackerAPI.Data;
using MachineAssetTrackerAPI.Interfaces;
using MachineAssetTrackerAPI.Models;

namespace MachineAssetTrackerAPI.Services
{
    public class AssetService : IAssetService
    {
        private AssetData _assetData = new AssetData();
        public void DeleteAsset(string id)
        {
            _assetData.DeleteAsset(id);
        }

        public List<Asset> GetAll()
        {
            return _assetData.GetAll();
        }

        public Asset GetAssetById(string id)
        {
            return _assetData.GetAssetById(id);
        }

        public void InsertAsset(Asset asset)
        {
            _assetData.InsertAsset(asset);
        }

        public void UpdateAssetDetails(string Id, Asset asset)
        {
            _assetData.UpdateAsset(Id, asset);
        }
    }
}
