
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//--------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using GroupToSection.Logic.Model;
using GroupToSection.Logic.Services;
using GroupToSection.Web.ViewModel;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupToSection.Web.Controllers
{
    public partial class GroupCategoryController : Controller
    {
        private readonly ILogger<GroupCategoryController> logger;
        private readonly IGroupCategoryService service;
        private readonly IMapper mapper;

        public GroupCategoryController(ILogger<GroupCategoryController> logger, 
        IGroupCategoryService service, 
        IMapper mapper)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
        }

        public virtual async Task<IActionResult> Index()
        {
            var list = await service.GetAll();
            var viewModels = mapper.Map<IEnumerable<GroupCategoryViewModel>>(list);
            return View(viewModels.OrderByDescending(x => x.Id));
        }
        
        public ActionResult Create()
        {
            return View(new GroupCategoryViewModel());
        }

        [HttpPost]
        public virtual async Task<ActionResult> Create([FromForm]GroupCategoryViewModel viewModel)
        {
            var model = mapper.Map<GroupCategory>(viewModel);
            await service.Insert(model);
            return RedirectToAction(nameof(Index));
        }

        public virtual async Task<ActionResult> Edit(int id)
        {
            var entity = await service.Get(id);
            return View(mapper.Map<GroupCategoryViewModel>(entity));
        }


        [HttpPost]
        public virtual async Task<ActionResult> Edit([FromForm]GroupCategoryViewModel viewModel)
        {
            var model = mapper.Map<GroupCategory>(viewModel);
            await service.Update(model);
            return RedirectToAction(nameof(Index));         
        }

        public virtual async Task<ActionResult> Remove(int id)
        {
            var entity = await service.Get(id);
            return View(mapper.Map<GroupCategoryViewModel>(entity));        
        }

        [HttpPost]
        public virtual async Task<ActionResult> Remove([FromForm]GroupCategoryViewModel viewModel)
        {
            var model = mapper.Map<GroupCategory>(viewModel);
            await service.Delete(viewModel.Id);
            return RedirectToAction(nameof(Index));         
        }
    }
}