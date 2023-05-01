namespace ChatManager.Models
{
    public class Relation
    {
        public int Id { get; set; }
        public int OtherUserId { get; set; }
        public int IdTypeRelation { get; set; }
    }
}