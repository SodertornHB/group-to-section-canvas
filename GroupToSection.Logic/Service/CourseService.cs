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
        Task CreateOrUpdateSectionFromGroup(int groupId);
        Task<IEnumerable<GroupCategory>> GetGroupCategories(int courseId);
    }

    public class CourseService : HttpService<Course>, ICourseService
    {
        private readonly IGroupService groupService;
        private readonly ISectionService sectionService;
        private readonly CanvasApiSettings canvasApiSettings;
        private readonly string coursesUrl;

        public CourseService(ILogger<CourseService> logger,
            IGroupService groupHttpService,
            ISectionService sectionService,
            ICourseHttpClient courseHttpClient,
            IOptions<CanvasApiSettings> canvasApiSettingsOptions )
           : base(courseHttpClient, logger)
        {
            this.groupService = groupHttpService;
            this.sectionService = sectionService;
            this.canvasApiSettings = canvasApiSettingsOptions.Value;
            coursesUrl = $"{canvasApiSettings.BaseUrl}/courses/";
        }

        public async Task<IEnumerable<GroupCategory>> GetGroupCategories(int courseId)
        {
            var groupCategories = await GetGroupCategoriesForCourse(courseId);
            var groups = await groupService.Get($"{coursesUrl}/{courseId}/groups"); 
            foreach (var category in groupCategories)
            {
                category.Groups = groups.Where(x => x.Group_Category_Id == category.Id);
            }
            return groupCategories;
        }

        public async Task CreateOrUpdateSectionFromGroup(int groupId)
        {
            var group = await groupService.GetById(groupId);
            var sectionId = await sectionService.CreateOrUpdateSection(group.Course_Id, group.Name, group.GetIdentifier());
            var userIds = await groupService.GetUserIdsFromGroup(group.Id);
            await sectionService.EnrollUsersInSection(sectionId, userIds);
        }

        #region private 
        private async Task<IEnumerable<GroupCategory>> GetGroupCategoriesForCourse(int courseId)
        {
            var groupCategories = await groupService.GetGroupCategoriesWithGroupsByCourseId(coursesUrl, courseId);
            return groupCategories;
        }
        #endregion

    }
}
