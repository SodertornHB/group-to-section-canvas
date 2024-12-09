using AutoMapper;
using GroupToSection.Logic.Services;
using GroupToSection.Web.ViewModel;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using GroupToSection.Logic.Settings;
using Microsoft.Extensions.Options;

namespace GroupToSection.Web.Controllers
{
    public partial class GroupCategoryController : Controller
    {
        private readonly ILogger<GroupCategoryController> logger;
        private readonly IGroupCategoryHttpService service;
        private readonly IMapper mapper;
        private readonly CanvasApiSettings canvasApiSettings;

        public GroupCategoryController(ILogger<GroupCategoryController> logger, 
        IGroupCategoryHttpService service, 
        IMapper mapper,
            IOptions<CanvasApiSettings> canvasApiSettingsOptions)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
            this.canvasApiSettings = canvasApiSettingsOptions.Value;
        }

        public virtual async Task<IActionResult> Index()
        {
            var list = await service.Get($"{canvasApiSettings.BaseUrl}/group_categories");
            var viewModels = mapper.Map<IEnumerable<GroupCategoryViewModel>>(list);
            return View(viewModels.OrderByDescending(x => x.Id));
        }      
    }
}