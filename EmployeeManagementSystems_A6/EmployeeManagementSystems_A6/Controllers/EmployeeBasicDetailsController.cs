// Controllers/EmployeeBasicDetailsController.cs
using EmployeeManagementSystems_A6.Models;
using EmployeeManagementSystems_A6.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystems_A6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBasicDetailsController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public EmployeeBasicDetailsController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBasicDetails>>> Get()
        {
            var items = await _cosmosDbService.GetItemsAsync<EmployeeBasicDetails>("SELECT * FROM c");
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBasicDetails>> Get(string id)
        {
            var item = await _cosmosDbService.GetItemAsync<EmployeeBasicDetails>(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmployeeBasicDetails item)
        {
            await _cosmosDbService.AddItemAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] EmployeeBasicDetails item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            await _cosmosDbService.UpdateItemAsync(id, item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
