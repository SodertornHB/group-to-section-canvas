using AutoMapper;
using EnrollmentToSection.Logic.Services;
using GroupToSection.Logic.Http;
using GroupToSection.Logic.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Logic.Service
{
    public interface IEnrollmentService : IHttpService<Enrollment>
    {
        Task EnrollInSection(int sectionId, int userId);
    }

    public class EnrollmentService : HttpService<Enrollment>, IEnrollmentService
    {
        private readonly CanvasApiSettings canvasApiSettings;

        public EnrollmentService(ILogger<EnrollmentService> logger,
            IEnrollmentHttpClient httpClient,
            IOptions<CanvasApiSettings> canvasApiSettingsOptions)
           : base(httpClient, logger)
        {
            this.canvasApiSettings = canvasApiSettingsOptions.Value;
        }
        public async Task EnrollInSection(int sectionId, int userId)
        {
            var content = JsonConvert.SerializeObject(new { enrollment = new Enrollment(userId, sectionId) });

            await Post($"{canvasApiSettings.BaseUrl}/sections/{sectionId}/enrollments", content);
        }

    }
    public class Enrollment
    {
        public Enrollment(int userId, int sectionId)
        {
            type = "StudentEnrollment";
            enrollment_state = "active";
            user_id = userId;
            course_section_id = sectionId;
            self_enrolled = false;
        }

        public int id { get; set; }
        public int user_id { get; private set; }

        public int course_section_id { get; private set; }
        public string type { get; private set; }
        public string enrollment_state { get; private set; }
        public bool self_enrolled { get; private set; }
        //public int course_id { get; set; }
        public int? course_enrollment_id { get; private set; }
        //public int? role_id { get; set; }
    }
}
