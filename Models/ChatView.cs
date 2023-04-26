using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatManager.Models
{
    public class ChatView
    {
        
        public int Id { get; set; }
        public int ListenerId { get; set; }
        public List<Message> Messages { get; set; }

    }
}