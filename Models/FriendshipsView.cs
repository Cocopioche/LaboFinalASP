using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChatManager.Models
{
    public class FriendshipsView
    {
        public int Id { get; set; } //Id of the user
        
        public List<Relation> Relations { get; set; }
        

        [JsonIgnore]
        public User User => DB.Users.Get(Id);

        public FriendshipsView(int id, List<Relation> relations)
        {
            Id = id;
            Relations = relations;
        }

        public FriendshipsView()
        {
            
        }
        
    }
}