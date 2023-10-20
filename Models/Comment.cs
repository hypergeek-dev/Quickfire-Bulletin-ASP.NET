namespace Quickfire_Bulletin.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string ArticleId { get; set; } 
        public NewsArticle Article { get; set; }
        public string Name { get; set; }
        public string? CommentContent { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Approved { get; set; }


    }
}
