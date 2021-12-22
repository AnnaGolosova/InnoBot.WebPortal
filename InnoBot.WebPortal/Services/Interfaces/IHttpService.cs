using InnoBot.WebPortal.Models;

namespace InnoBot.WebPortal.Services.Interfaces
{
    public interface IHttpService
    {
        Task<List<QuestionModel>> GetQuestionsAsync();
        Task<List<FeedbackModel>> GetFeedbacksAsync();
    }
}
