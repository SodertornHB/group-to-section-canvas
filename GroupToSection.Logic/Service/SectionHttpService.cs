using AutoMapper;
using GroupToSection.Logic.Http;
using GroupToSection.Logic.Services;
using GroupToSection.Logic.Settings;
using Logic.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Service
{
    public interface ISectionService : IHttpService<Section>
    {

        Task<IEnumerable< Section>> GetSectionsByCourseId(int courseId);
    }

    public class SectionService : HttpService<Section>, ISectionService
    {
        private readonly IMapper mapper;
        private readonly ISectionHttpClient sectionHttpClient;
        private readonly CanvasApiSettings canvasApiSettings;
        private readonly string sectionsUrl;

        public SectionService(ILogger<SectionService> logger,
            ISectionHttpClient httpClient,
            IMapper mapper,
            ISectionHttpClient sectionHttpClient,
            IOptions<CanvasApiSettings> canvasApiSettingsOptions )
           : base(httpClient, logger)
        {
            this.mapper = mapper;
            this.sectionHttpClient = sectionHttpClient;
            this.canvasApiSettings = canvasApiSettingsOptions.Value;
            sectionsUrl = $"{canvasApiSettings.BaseUrl}/sections/";
        }

        public Task<IEnumerable<Section>> GetSectionsByCourseId(int courseId)
        {
            throw new System.NotImplementedException();
        }
    }
}
