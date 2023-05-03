using Newtonsoft.Json;

namespace ChatManager.Models
{
    public class Relation
    {
        public int Id { get; set; }
        public int OtherUserId { get; set; }
        public int IdTypeRelation { get; set; }

        [JsonIgnore]
        public string RelationName => DB.TypeRelations.Get(IdTypeRelation).Name;
    }
}