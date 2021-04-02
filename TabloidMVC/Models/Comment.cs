using System;

namespace TabloidMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public int UserProfileId { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
