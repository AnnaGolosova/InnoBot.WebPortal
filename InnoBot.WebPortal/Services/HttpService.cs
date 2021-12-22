using InnoBot.WebPortal.Models;
using InnoBot.WebPortal.Services.Interfaces;
using Newtonsoft.Json;

namespace InnoBot.WebPortal.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public HttpService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();

            _apiUrl = configuration.GetSection("environmentVariables").GetValue<string>("ApiUrl");
            if (string.IsNullOrEmpty(_apiUrl))
            {
                throw new Exception("Not found api url in configuration.");
            }
        }

        public async Task<List<QuestionModel>> GetQuestionsAsync()
        {
            var serverResponse = await _httpClient.GetAsync(_apiUrl + "api/questions");

            if (serverResponse.IsSuccessStatusCode)
            {
                var stringContent = await serverResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<QuestionModel>>(stringContent) ?? new List<QuestionModel>();
            }

            return new List<QuestionModel>();
        }

        public async Task<List<FeedbackModel>> GetFeedbacksAsync()
        {
            var serverResponse = await _httpClient.GetAsync(_apiUrl + "api/feedbacks");

            if (serverResponse.IsSuccessStatusCode)
            {
                var stringContent = await serverResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<FeedbackModel>>(stringContent) ?? new List<FeedbackModel>();
            }

            return new List<FeedbackModel>();
        }
    }
}
