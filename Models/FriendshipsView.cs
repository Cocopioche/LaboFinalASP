using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChatManager.Models
{
    public class FriendshipsView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        
        public List<Relation> Relations { get; set; }
        

        [JsonIgnore]
        public User User => DB.Users.Get(UserId);

        public FriendshipsView(int id, List<Relation> relations)
        {
            UserId = id;
            Relations = relations;
        }

        public FriendshipsView()
        {
            
        }

        public Relation GetRelation(User otherUser)
        {
            foreach (Relation relation in Relations)
            {
                if (relation.OtherUserId == otherUser.Id)
                    return relation;
            }
            return null;
        }
    }
}