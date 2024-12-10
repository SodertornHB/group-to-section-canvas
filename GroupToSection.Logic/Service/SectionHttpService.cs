using GroupToSection.Logic.Http;
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
        /// <returns>new section id</returns>
        Task<int> CreateOrUpdateSection(int courseId, string sectionName, string sectionIdentifier);
    }

    public class SectionService : HttpService<Section>, ISectionService
    {
        private readonly IEnrollmentService enrollmentService;
        private readonly CanvasApiSettings canvasApiSettings;
        private readonly string sectionsUrl;
        private readonly string coursesUrl;

        public SectionService(ILogger<SectionService> logger,
            ISectionHttpClient httpClient,
            IEnrollmentService enrollmentService,
            IOptions<CanvasApiSettings> canvasApiSettingsOptions )
           : base(httpClient, logger)
        {
            this.enrollmentService = enrollmentService;
            this.canvasApiSettings = canvasApiSettingsOptions.Value;
            sectionsUrl = $"{canvasApiSettings.BaseUrl}/sections";
            coursesUrl = $"{canvasApiSettings.BaseUrl}/courses";
        }

        public async Task<int> CreateOrUpdateSection(int courseId, string sectionName, string sectionIdentifier)
        {
            var sections = await Get($"{coursesUrl}/{courseId}/sections");
            var section = sections.SingleOrDefault(x => x.Sis_section_id == sectionIdentifier);

            if (section == null)
            {
                return await CreateSectionFromGroup(courseId, sectionName, sectionIdentifier);
            }
            else
            {
                var enrollments = await GetEnrollments(section.Id);
                foreach (var enrollment in enrollments)
                {
                    await enrollmentService.Delete($"{coursesUrl}/{courseId}/enrollments", enrollment.id.ToString());
                }
                return section.Id;
            }
        }
        
        public async Task EnrollUsersInSection(int sectionId, int[] userIds)
        {
            foreach (var userId in userIds)
            {
                await enrollmentService.EnrollUserInSection(sectionId, userId);
            }
        }


        #region private 
        private async Task<int> CreateSectionFromGroup(int courseId, string sectionName, string sectionIdentifier)
        {
            var content = JsonConvert.SerializeObject(new { course_section = new Section(sectionName, sectionIdentifier) });
            var response = await Post($"{coursesUrl}/{courseId}/sections", content);
            return response.Id;
        }

        private async Task<IEnumerable<Enrollment>> GetEnrollments(int sectionId) => await enrollmentService.Get($"{sectionsUrl}/{sectionId}/enrollments");

        #endregion
    }
   
}
