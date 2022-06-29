using InnoBot.WebPortal.Models;
using InnoBot.WebPortal.Services.Interfaces;
using Newtonsoft.Json;

namespace InnoBot.WebPortal.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public HttpService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;

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

        public async Task<List<QuestionModel>> GetQuestionsForPresentationAsync(Guid meetupId, Guid presentationId)
        {
            var serverResponse = await _httpClient.GetAsync($"{_apiUrl}api/meetups/{meetupId}/presentations/{presentationId}/questions");

            if (serverResponse.IsSuccessStatusCode)
            {
                var stringContent = await serverResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<QuestionModel>>(stringContent) ?? new List<QuestionModel>();
            }

            return new List<QuestionModel>();
        }

        public async Task<List<FeedbackModel>> GetFeedbacksAsync(Guid meetupId)
        {
            var serverResponse = await _httpClient.GetAsync(_apiUrl + $"api/meetups/{meetupId}/feedbacks");

            if (serverResponse.IsSuccessStatusCode)
            {
                var stringContent = await serverResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<FeedbackModel>>(stringContent) ?? new List<FeedbackModel>();
            }

            return new List<FeedbackModel>();
        }

        public async Task<Dictionary<Guid, List<QuestionModel>>> GetGroupedQuestions(Guid meetupId, List<Guid> presentationIds)
        {
            if (presentationIds == null || presentationIds.Count <= 0)
            {
                return new Dictionary<Guid, List<QuestionModel>>();
            }

            var groupedPresentations = new Dictionary<Guid, List<QuestionModel>>();
            foreach (var presentationId in presentationIds)
            {
                var questions = await GetQuestionsForPresentationAsync(meetupId, presentationId);

                groupedPresentations.Add(presentationId, questions);
            }

            return groupedPresentations;
        }

        public async Task<List<PresentationModel>> GetPresentationsForMeetupAsync(Guid meetupId)
        {
            var serverResponse = await _httpClient.GetAsync(_apiUrl + $"api/meetups/{meetupId}/presentations");

            if (serverResponse.IsSuccessStatusCode)
            {
                var stringContent = await serverResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<PresentationModel>>(stringContent) ?? new List<PresentationModel>();
            }

            return new List<PresentationModel>();
        }
    }
}
