using InnoBot.WebPortal.Models;

namespace InnoBot.WebPortal.Services.Interfaces
{
    public interface IHttpService
    {
        Task<List<QuestionModel>> GetQuestionsAsync();
        Task<List<FeedbackModel>> GetFeedbacksAsync();
        Task<Dictionary<Guid, List<QuestionModel>>> GetGroupedQuestions();
        Task<List<QuestionModel>> GetQuestionsForPresentationAsync(Guid presentationId);
        Task<List<PresentationModel>> GetPresentationsAsync();
    }
}
