using GroupToSection.Web.ViewModel;
using Logic.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModel;

namespace GroupToSection.Web.Controllers
{
    [Route("lti")]
    public class LtiController : Controller
    {
        private readonly ICourseService service;    
            public LtiController(
            ICourseService service
            )
        {
            this.service = service;
           
        }

#if DEBUG
        [HttpGet("group-to-section/{courseId}")]
        public async Task<IActionResult> GroupToSectionForTest([FromRoute] int courseId)
        {
            var groupCategories = await service.GetGroupCategories(courseId);
            var viewModel = new GroupToSectionViewModel
            {
                GroupCategories = groupCategories.Select(x => new GroupCategoryViewModel { Name = x.Name, Groups = x.Groups.Select(y => new GroupViewModel { Id = y.Id, Name = y.Name }) })
            };
            return View("GroupToSection", viewModel);
        }
#endif


        [HttpPost("group-to-section")]
        public async Task<IActionResult> GroupToSection()
        {
            var groupCategories = await service.GetGroupCategories(int.Parse(Request.Form["custom_canvas_course_id"].ToString()));
            var viewModel = new GroupToSectionViewModel
            {
                GroupCategories = groupCategories.Select(x => new GroupCategoryViewModel { Name = x.Name, Groups = x.Groups.Select(y => new GroupViewModel { Id = y.Id, Name = y.Name }) })
            };
            return View("GroupToSection", viewModel);
        }

        //[HttpPost("update-group-to-section")]
        //public async Task<IActionResult> UpdateGroupToSection(int id)
        //{
        //    try
        //    {
        //        await groupService.CreateOrUpdateSectionFromGroup(id);
        //        return new OkResult();
        //    }
        //    catch (Exception e)
        //    {
        //        return new BadRequestObjectResult("Error: " + e.Message);
        //    }
        //}
       
    }
}
