
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//--------------------------------------------------------------------------------------------------------------------

using GroupToSection.Logic.Model;
using GroupToSection.Logic.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Reflection;

namespace GroupToSection.Web.ApiController
{ 
    [Route("api/v1/[controller]s")]
    [ApiController]
    public partial class GroupController: ControllerBase
    {
        protected readonly ILogger<GroupController> logger;
        protected readonly IGroupService service;

        public GroupController(ILogger<GroupController> logger, IGroupService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var items = await service.GetAll();
            if (!items.Any()) logger.LogInformation("No content found.");
            return Ok(items);            
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            
            var item = await service.Get(id);
            if (item == null) return NotFound();
            return Ok(item);            
        }

        [HttpGet("search")]
        public async Task<IActionResult> Get([FromQuery] Dictionary<string, string> filters)
        {
            if (filters == null) throw new ArgumentNullException(nameof(filters));

            var modelType = typeof(Group);
            foreach (var key in filters.Keys)
            {
                var propertyInfo = modelType.GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null) throw new ArgumentException($"Invalid filter parameter: {key}");
            }

            var items = await service.Get(filters);
            if (!items.Any()) return NotFound();
            return Ok(items);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] dynamic value)
        {
            var item = JsonConvert.DeserializeObject<Group>(value.ToString());
            var newItem = await service.Insert(item);
            return CreatedAtAction(nameof(Post), new {id = newItem.Id }, newItem);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(int id, [FromBody] dynamic value)
        {
            if (!await service.Exists(id)) return NotFound();
            var item = JsonConvert.DeserializeObject<Group>(value.ToString());
            item.Id = id;
            await service.Update(item);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            if (!await service.Exists(id)) return NotFound();
            await service.Delete(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}