using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChatManager.Models
{
    public class FriendshipsView
    {
        public int Id { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public bool Accepted { get; set; }
        public bool Decline { get; set; }
        

        [JsonIgnore]
        public User User1 => DB.Users.Get(UserId1);
        [JsonIgnore]
        public User User2 => DB.Users.Get(UserId2);

        public FriendshipsView(int idUser1,int idUser2)
        {
            UserId1 = idUser1;
            UserId2 = idUser2;
            Accepted = false;
            Decline = false;
        }

        public FriendshipsView()
        {
            
        }
        
        
        
    }
}