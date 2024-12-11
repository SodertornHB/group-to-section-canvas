
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//--------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using GroupToSection.Logic.Services;
using GroupToSection.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace GroupToSection.Web.Controllers
{
    public partial class LogController : Controller
    {
        private readonly ILogService service;
        private readonly IMapper mapper;

        public LogController(ILogService service, 
        IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public virtual async Task<IActionResult> Index()
        {
            var list = await service.GetAll();
            var viewModels = mapper.Map<IEnumerable<LogViewModel>>(list);
            return View(viewModels.OrderByDescending(x => x.Id).Take(10000));
        }
    }
}