
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//--------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Logic.Service;
using Microsoft.Extensions.Options;
using GroupToSection.Logic.Settings;
using GroupToSection.Logic.Services;

namespace GroupToSection.Web.ApiController
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public partial class GroupController: ControllerBase
    {
        protected readonly ILogger<GroupController> logger;
        protected readonly IGroupService service;
        private readonly CanvasApiSettings settings;
        private readonly string resourceUrl;

        public GroupController(ILogger<GroupController> logger, IGroupService service, IOptions<CanvasApiSettings> settingsOptions)
        {
            this.logger = logger;
            this.service = service;
            this.settings = settingsOptions.Value;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var items = await service.Get($"{settings.BaseUrl}/courses/{262}/groups");
            if (!items.Any()) logger.LogInformation("No content found.");
            return Ok(items);            
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            
            var item = await service.Get($"{settings.BaseUrl}/courses/{id}/groups");
            if (item == null) return NotFound();
            return Ok(item);            
        }
    }
}