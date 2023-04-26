using System;

namespace ChatManager.Models
{
    public class Message
    {
        public Message()
        {
            PostingTime = DateTime.Now;
        }
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Text { get; set; }
        public DateTime PostingTime { get; set; }
    }
}