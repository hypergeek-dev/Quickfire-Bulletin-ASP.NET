namespace Quickfire_Bulletin.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
