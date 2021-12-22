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

        public async Task<List<QuestionModel>> GetQuestionsForPresentationAsync(Guid presentationId)
        {
            var serverResponse = await _httpClient.GetAsync($"{_apiUrl}api/presentations/{presentationId}/questions");

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

        public async Task<Dictionary<Guid, List<QuestionModel>>> GetGroupedQuestions()
        {
            var getPresentationsResponse = await _httpClient.GetAsync(_apiUrl + "api/presentations");
            List<PresentationModel> allPresentations = null;
            if (getPresentationsResponse.IsSuccessStatusCode)
            {
                var stringContent = await getPresentationsResponse.Content.ReadAsStringAsync();

                allPresentations = JsonConvert.DeserializeObject<List<PresentationModel>>(stringContent) ?? new List<PresentationModel>();
            }

            if (allPresentations == null || allPresentations.Count <= 0)
            {
                return new Dictionary<Guid, List<QuestionModel>>();
            }

            var groupedPresentations = new Dictionary<Guid, List<QuestionModel>>();
            foreach (var presentationId in allPresentations.Select(p => p.Id))
            {
                var questions = await GetQuestionsForPresentationAsync(presentationId);

                groupedPresentations.Add(presentationId, questions);
            }

            return groupedPresentations;
        }
    }
}
