// Controllers/EmployeeAdditionalDetailsController.cs
using EmployeeManagementSystems_A6.Models;
using EmployeeManagementSystems_A6.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystems_A6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAdditionalDetailsController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public EmployeeAdditionalDetailsController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeAdditionalDetails>>> Get()
        {
            var items = await _cosmosDbService.GetItemsAsync<EmployeeAdditionalDetails>("SELECT * FROM c");
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeAdditionalDetails>> Get(string id)
        {
            var item = await _cosmosDbService.GetItemAsync<EmployeeAdditionalDetails>(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmployeeAdditionalDetails item)
        {
            await _cosmosDbService.AddItemAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] EmployeeAdditionalDetails item)
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
