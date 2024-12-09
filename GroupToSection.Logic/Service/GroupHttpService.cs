using AutoMapper;
using GroupToSection.Logic.Http;
using GroupToSection.Logic.Model;
using GroupToSection.Logic.Settings;
using Logic.Model;
using Logic.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace GroupToSection.Logic.Services
{
    public interface IGroupService : IHttpService<Group>
    {
        Task<Group> GetById(int id);
        Task<IEnumerable<GroupCategory>> GetGroupCategoriesWithGroupsByCourseId(string url, int courseId);
        Task<int[]> GetUserIds(int groupId);
    }

    public class GroupService : HttpService<Group>, IGroupService
    {
        private readonly IMapper mapper;
        private readonly IGroupHttpClient groupHttpClient;
        private readonly IGroupCategoryHttpService groupCategoryHttpService;
        private readonly IEntityService entityService;
        private readonly CanvasApiSettings canvasApiSettings;
        private readonly string groupsUrl;

        public GroupService(ILogger<GroupService> logger,
            IGroupHttpClient httpClient,
            IMapper mapper,
            IGroupHttpClient groupHttpClient,
            IGroupCategoryHttpService groupCategoryHttpService,
            IEntityService entityService,
            IOptions<CanvasApiSettings> canvasApiSettingsOptions )
           : base(httpClient, logger)
        {
            this.mapper = mapper;
            this.groupHttpClient = groupHttpClient;
            this.groupCategoryHttpService = groupCategoryHttpService;
            this.entityService = entityService;
            this.canvasApiSettings = canvasApiSettingsOptions.Value;
            groupsUrl = $"{canvasApiSettings.BaseUrl}/groups";
        }

        public async Task<Group> GetById(int id) => await Get(groupsUrl, id.ToString());

        public async Task<IEnumerable<GroupCategory>> GetGroupCategoriesWithGroupsByCourseId(string url, int courseId)
        {
            var groupCategories = await groupCategoryHttpService.Get($"{url}/{courseId}/group_categories");
            await GetGroupsAndAddToCategories(url, courseId, groupCategories);
            return groupCategories;
        }

        public async Task<int[]> GetUserIds(int groupId)
        {
            var entities = await entityService.Get($"{groupsUrl}/{groupId}/users");
            return entities.Select(x => x.Id).ToArray();
        }

        #region private 

        private async Task GetGroupsAndAddToCategories(string url, int courseId, IEnumerable<GroupCategory> groupCategories)
        {
            var groups = await Get($"{url}/{courseId}/groups");
            foreach (var category in groupCategories)
            {
                category.Groups = groups.Where(x => x.Group_Category_Id == category.Id);
            }
        }
        #endregion
    }
}
