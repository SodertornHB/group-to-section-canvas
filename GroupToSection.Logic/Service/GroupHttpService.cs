using AutoMapper;
using GroupToSection.Logic.Http;
using GroupToSection.Logic.Model;
using GroupToSection.Logic.Services;
using GroupToSection.Logic.Settings;
using Logic.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Service
{
    public interface IGroupService : IHttpService<Group>
    {
        Task<IEnumerable<GroupCategory>> GetGroupCategoriesWithGroupsByCourseId(string url, int courseId);

        //Task CreateOrUpdateSectionFromGroup(int id);
    }

    public class GroupService : HttpService<Group>, IGroupService
    {
        private readonly IMapper mapper;
        private readonly IGroupHttpClient groupHttpClient;
        private readonly IGroupCategoryHttpService groupCategoryHttpService;
        private readonly CanvasApiSettings canvasApiSettings;
        private readonly string groupsUrl;

        public GroupService(ILogger<GroupService> logger,
            IGroupHttpClient httpClient,
            IMapper mapper,
            IGroupHttpClient groupHttpClient,
            IGroupCategoryHttpService groupCategoryHttpService,
            IOptions<CanvasApiSettings> canvasApiSettingsOptions )
           : base(httpClient, logger)
        {
            this.mapper = mapper;
            this.groupHttpClient = groupHttpClient;
            this.groupCategoryHttpService = groupCategoryHttpService;
            this.canvasApiSettings = canvasApiSettingsOptions.Value;
            groupsUrl = $"{canvasApiSettings.BaseUrl}/groups/";
        }
        public async Task<IEnumerable<GroupCategory>> GetGroupCategoriesWithGroupsByCourseId(string url, int courseId)
        {
            var groupCategories = await groupCategoryHttpService.Get($"{url}/{courseId}/group_categories");
            await GetGroupsAndAddToCategories(url, courseId, groupCategories);
            return groupCategories;
        }

        //public async Task CreateOrUpdateSectionFromGroup(int groupId)
        //{
        //    var group = await GetGroup(groupId);
        //    var sectionId = await CreateSectionIfNotExist(group);
        //    await EnrollGroupUsersInSection(groupId, sectionId);
        //}

        #region private 

        private async Task GetGroupsAndAddToCategories(string url, int courseId, IEnumerable<GroupCategory> groupCategories)
        {
            var groups = await Get($"{url}/{courseId}/groups");
            foreach (var category in groupCategories)
            {
                category.Groups = groups.Where(x => x.Group_Category_Id == category.Id);
            }
        }

        //private async Task<Group> GetGroup(int groupId)
        //{            
        //    return await base.Get(groupsUrl,groupId.ToString());
        //}
        //private async Task<int> CreateSectionIfNotExist(Group group)
        //{
        //    var sectionIdentifier = $"{group.Course_Id}:{group.Id}";
        //    var sections = JsonConvert.DeserializeObject<IEnumerable<Section>>(await coursesHttpClient.GetSections(group.Course_Id));
        //    var section = sections.SingleOrDefault(x => x.Sis_section_id == sectionIdentifier);

        //    if (section == null)
        //    {
        //        var sectionResponse = await CreateSection(group, sectionIdentifier);
        //        return sectionResponse.id;
        //    }
        //    else
        //    {
        //        await enrollmentService.RemoveEnrollmentsFromSection(section.Id);
        //        return section.Id;
        //    }
        //}

        //private async Task<IdResponseModel> CreateSection(Group group, string sectionIdentifier)
        //{
        //    var sectionJson = await coursesHttpClient.CreateSectionForCourse(group.Course_Id, JsonConvert.SerializeObject(new SectionRequestModel(group.Name, sectionIdentifier)));
        //    var sectionResponse = (JsonConvert.DeserializeObject<IdResponseModel>(sectionJson));
        //    return sectionResponse;
        //}

        //private async Task EnrollGroupUsersInSection(int groupId, int sectionId)
        //{
        //    var users = JsonConvert.DeserializeObject<IEnumerable<CanvasUser>>(await groupsHttpClient.GetUsersByGroupId(groupId));
        //    foreach (var user in users)
        //    {
        //        await enrollmentService.EnrollUserInSection(user.Id, sectionId, EnrollmentType.Student);
        //    }
        //}

        #endregion
    }
}
