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

        public Relation(int otherUserId)
        {
            OtherUserId = otherUserId;
            IdTypeRelation = 0;
        }

    }
}