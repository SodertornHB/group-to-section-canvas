using EnrollmentToSection.Logic.Services;
using GroupToSection.Logic.Http;
using GroupToSection.Logic.Settings;
using Logic.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Logic.Service
{
    public interface IEnrollmentService : IHttpService<Enrollment>
    {
        Task EnrollUserInSection(int sectionId, int userId);
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
        public async Task EnrollUserInSection(int sectionId, int userId)
        {
            var enrollmendWrapper = new { enrollment = new Enrollment(userId, sectionId) };

            var content = JsonConvert.SerializeObject(enrollmendWrapper);

            await Post($"{canvasApiSettings.BaseUrl}/sections/{sectionId}/enrollments", content);
        }
        public override Task Delete(string url, string id)
        {
            return client.Delete(new System.Uri($"{url}/{id}"), new EnrollmentTask("delete"));
        }

    }
}
