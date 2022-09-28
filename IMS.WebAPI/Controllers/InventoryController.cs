using AutoMapper;
using IMS.Core.Entities;
using IMS.Core.Interfaces;
using IMS.WebApp.Responses;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;

namespace IMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public InventoryController(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        [HttpGet("GetInventoryList/{name}")]
        public async Task<IActionResult> GetInventoryList(string name)
        {
            try
            {
                var result = await _inventoryRepository.GetInventoriesByName(name);
                var response = new ApiResponse<List<Inventory>>(result);

                return Ok(response);
            }
            catch (Exception exception)
            {
                Log.Write(LogEventLevel.Error, exception, exception.Message);
                return StatusCode(500);
            }
        }
    }
}
