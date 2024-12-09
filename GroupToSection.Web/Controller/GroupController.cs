
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//--------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using GroupToSection.Web.ViewModel;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using GroupToSection.Logic.Settings;
using Logic.Service;
using Microsoft.Extensions.Options;
using GroupToSection.Logic.Services;

namespace GroupToSection.Web.Controllers
{
    public partial class GroupController : Controller
    {
        public partial class GroupCategoryController : Controller
        {
            private readonly ILogger<GroupCategoryController> logger;
            protected readonly IGroupService service;
            private readonly CanvasApiSettings settings;
            private readonly string resourceUrl;
            private readonly IMapper mapper;

            public GroupCategoryController(ILogger<GroupCategoryController> logger,
            IGroupService service, IOptions<CanvasApiSettings> settingsOptions,
            IMapper mapper)
            {
                this.logger = logger;
                this.service = service;
                this.mapper = mapper;
                this.settings = settingsOptions.Value;
                resourceUrl = $"{settings.BaseUrl}/groups";
            }

            public virtual async Task<IActionResult> Index()
            {
                var list = await service.Get(resourceUrl);
                var viewModels = mapper.Map<IEnumerable<GroupCategoryViewModel>>(list);
                return View(viewModels.OrderByDescending(x => x.Id));
            }
        }
    }
}