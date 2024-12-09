using AutoMapper;
using GroupToSection.Logic.Http;
using GroupToSection.Logic.Model;
using GroupToSection.Logic.Settings;
using Logic.Model;
using Logic.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupToSection.Logic.Services
{
    public interface ISectionService : IHttpService<Section>
    {
        Task EnrollUsersInSection(int sectionId, int[] userIds);
        Task<IEnumerable<Section>> GetSectionsByCourseId(int courseId);
        /// <returns>new section id</returns>
        Task<int> CreateSectionFromGroup(Group group);
        Task<int> CreateSectionIfNotExist(Group group);
    }

    public class SectionService : HttpService<Section>, ISectionService
    {
        private readonly IMapper mapper;
        private readonly ISectionHttpClient sectionHttpClient;
        private readonly IEnrollmentService enrollmentService;
        private readonly CanvasApiSettings canvasApiSettings;
        private readonly string sectionsUrl;
        private readonly string coursesUrl;

        public SectionService(ILogger<SectionService> logger,
            ISectionHttpClient httpClient,
            IMapper mapper,
            ISectionHttpClient sectionHttpClient,
            IEnrollmentService enrollmentService,
            IOptions<CanvasApiSettings> canvasApiSettingsOptions )
           : base(httpClient, logger)
        {
            this.mapper = mapper;
            this.sectionHttpClient = sectionHttpClient;
            this.enrollmentService = enrollmentService;
            this.canvasApiSettings = canvasApiSettingsOptions.Value;
            sectionsUrl = $"{canvasApiSettings.BaseUrl}/sections";
            coursesUrl = $"{canvasApiSettings.BaseUrl}/courses";
        }

        public Task<IEnumerable<Section>> GetSectionsByCourseId(int courseId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> CreateSectionFromGroup(Group group)
        {
            var content = JsonConvert.SerializeObject(new { course_section = new Section(group.Name, group.GetIdentifier()) });
            var response = await Post($"{coursesUrl}/{group.Course_Id}/sections", content);
            return response.Id;
        }

        public async Task<int> CreateSectionIfNotExist(Group group)
        {
            var sections = await Get($"{coursesUrl}/{group.Course_Id}/sections");
            var section = sections.SingleOrDefault(x => x.Sis_section_id == group.GetIdentifier());

            if (section == null)
            {
                return await CreateSectionFromGroup(group);
            }
            else
            {
                var enrollments = await GetEnrollments(section.Id);
                foreach (var enrollment in enrollments)
                {
                    await enrollmentService.Delete($"{coursesUrl}/{group.Course_Id}/enrollments", enrollment.id.ToString());
                }
                return section.Id;
            }
        }


        public async Task EnrollUsersInSection(int sectionId, int[] userIds)
        {
            foreach (var userId in userIds)
            {
                await enrollmentService.EnrollInSection(sectionId, userId);
            }
        }


        #region private 

        private async Task<IEnumerable<Enrollment>> GetEnrollments(int sectionId) => await enrollmentService.Get($"{sectionsUrl}/{sectionId}/enrollments");

        #endregion
    }
   
}
