using GroupToSection.Logic.Http;
using GroupToSection.Logic.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace GroupToSection.Logic.Services
{
    public interface IGroupHttpClient : IHttpClient
    {
        //Task<string> GetGroupCategoriesByCourseId(string url, int courseId);
        //Task<string> GetGroupCategoriesByCourseId(string url, int courseId, int page);
    }

    public class GroupHttpClient : HttpClient, IGroupHttpClient
    {
        public GroupHttpClient(System.Net.Http.IHttpClientFactory factory,
            ILogger<GroupHttpClient> logger,
            IOptions<AuthenticationSettings> authenticationSettingsOptions)
           : base(factory, logger, authenticationSettingsOptions)
        {
        }

        //public async Task<string> GetGroupCategoriesByCourseId(string url, int courseId)
        //{
        //    return await GetGroupCategoriesByCourseId(url, courseId, 1);
        //}

        //public async Task<string> GetGroupCategoriesByCourseId(string url, int courseId, int page)
        //{
        //    var uri = new Uri($"{url}/{courseId}/group_categories?per_page=100&page={page}");
        //    var response = await Get(uri);
        //    return response.Content;
        //}
    }
}
