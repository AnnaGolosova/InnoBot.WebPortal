namespace InnoBot.WebPortal.Models
{
    public class FeedbackModel
    {
        public string AuthorName { get; set; }
        public string Message { get; set; }
        public string FutureProposal { get; set; }
        public DateTime Sent { get; set; }
    }
}
