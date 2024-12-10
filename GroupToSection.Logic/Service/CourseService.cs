using GroupToSection.Logic.Http;
using GroupToSection.Logic.Model;
using GroupToSection.Logic.Services;
using GroupToSection.Logic.Settings;
using Logic.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Service
{
    public interface ICourseService : IHttpService<Course>
    {
        Task<IEnumerable<Section>> GetSections(int courseId);
        Task<IEnumerable<Group>> GetGroups(int courseId);
        Task<IEnumerable<GroupCategory>> GetGroupCategories(int courseId);
    }

    public class CourseService : HttpService<Course>, ICourseService
    {
        private readonly IGroupService groupHttpService;
        private readonly CanvasApiSettings canvasApiSettings;
        private readonly string coursesUrl;

        public CourseService(ILogger<CourseService> logger,
            IGroupService groupHttpService,
            ICourseHttpClient courseHttpClient,
            IOptions<CanvasApiSettings> canvasApiSettingsOptions )
           : base(courseHttpClient, logger)
        {
            this.groupHttpService = groupHttpService;
            this.canvasApiSettings = canvasApiSettingsOptions.Value;
            coursesUrl = $"{canvasApiSettings.BaseUrl}/courses/";
        }

        public async Task<IEnumerable<GroupCategory>> GetGroupCategories(int courseId)
        {
            var groupCategories = await GetGroupCategoriesForCourse(courseId);
            var groups = await groupHttpService.Get($"{coursesUrl}/{courseId}/groups"); 
            foreach (var category in groupCategories)
            {
                category.Groups = groups.Where(x => x.Group_Category_Id == category.Id);
            }
            return groupCategories;
        }

        public Task<IEnumerable<Group>> GetGroups(int courseId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Section>> GetSections(int courseId)
        {
            throw new System.NotImplementedException();
        }

        #region private 
        private async Task<IEnumerable<GroupCategory>> GetGroupCategoriesForCourse(int courseId)
        {
            var groupCategories = await groupHttpService.GetGroupCategoriesWithGroupsByCourseId(coursesUrl, courseId);
            return groupCategories;
        }
        #endregion

    }
}
