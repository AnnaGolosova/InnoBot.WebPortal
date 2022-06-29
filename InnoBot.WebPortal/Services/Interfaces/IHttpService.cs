using InnoBot.WebPortal.Models;

namespace InnoBot.WebPortal.Services.Interfaces
{
    public interface IHttpService
    {
        Task<List<QuestionModel>> GetQuestionsAsync();
        Task<List<FeedbackModel>> GetFeedbacksAsync(Guid meetupId);
        Task<Dictionary<Guid, List<QuestionModel>>> GetGroupedQuestions(Guid meetupId, List<Guid> presentationIds);
        Task<List<QuestionModel>> GetQuestionsForPresentationAsync(Guid meetupId, Guid presentationId);
        Task<List<PresentationModel>> GetPresentationsForMeetupAsync(Guid meetupId);
    }
}
