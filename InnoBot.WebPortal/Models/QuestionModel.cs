namespace InnoBot.WebPortal.Models
{
    public class QuestionModel
    {
        public string QuestionText { get; set; }
        public DateTime Asked { get; set; }
        public string AuthorName { get; set; }
        public Guid? PresentationId { get; set; }
    }
}
