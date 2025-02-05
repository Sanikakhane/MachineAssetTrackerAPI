using MachineAssetTrackerAPI.Services;
using MachineAssetTrackerAPI.Interfaces;
using MachineAssetTrackerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MachineAssetTrackerAPI.Controllers
{
    [Route("api/assets")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        /// <summary>
        /// Get all assets
        /// </summary>
        /// <returns>List of All assets</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var assets = _assetService.GetAll();
            if (assets.Count == 0 || assets == null)
            {
                return NotFound("The List empty");
            }
            return Ok(_assetService.GetAll());
        }

        /// <summary>
        /// Insert a new asset
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>Returns created status</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult InsertAsset([FromBody] Asset asset)
        {
            _assetService.InsertAsset(asset);
            return Ok("Asset Inserted successfully");
        }

        /// <summary>
        /// Update asset details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asset"></param>
        /// <returns>Updated asset object</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateAssetDetails(string id, [FromBody] Asset asset)
        {
            var assetData = _assetService.GetAssetById(id);
            if (assetData == null)
            {
                return NotFound("Id not found");
            }
            _assetService.UpdateAssetDetails(id, asset);
            return Ok(asset);
        }

        /// <summary>
        /// Delete an asset
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deleted Asset</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAsset(string id)
        {
            var assetData = _assetService.GetAssetById(id);
            if (assetData == null)
            {
                return NotFound("Id Not found");
            }
            _assetService.DeleteAsset(id);
            return Ok(assetData);
        }

        /// <summary>
        /// Get asset by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Asset object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAssetById(string id)
        {
            var assetData = _assetService.GetAssetById(id);
            if (assetData == null)
            {
                return NotFound("The Id found");
            }
            return Ok(_assetService.GetAssetById(id));
        }
    }
}
