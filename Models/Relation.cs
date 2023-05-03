using Newtonsoft.Json;

namespace ChatManager.Models
{
    [JsonObject]
    public class Relation
    {
        public int OtherUserId { get; set; }
        public int IdTypeRelation { get; set; }

        [JsonIgnore]
        public string RelationName => DB.TypeRelations.Get(IdTypeRelation).Name;
        [JsonIgnore]
        public User OtherUser => DB.Users.Get(OtherUserId);

        public Relation(int otherUserId)
        {
            OtherUserId = otherUserId;
            IdTypeRelation = 0;
        }

        public Relation()
        {
            OtherUserId = 0;
            IdTypeRelation = 0;
        }

    }
}