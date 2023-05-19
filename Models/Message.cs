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
        public int IdUserMain { get; set; }
        public int IdUserOther { get; set; }
        public string Text { get; set; }
        public DateTime PostingTime { get; set; }

        public Message(int idUserMain, int idUserOther, string text)
        {
            IdUserMain = idUserMain;
            IdUserOther = idUserOther;
            Text = text;
        }
    }
}