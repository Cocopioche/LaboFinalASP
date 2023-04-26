namespace ChatManager.Models
{
    public class Relation
    {
        public int Id;
        public int OtherUserId { get; set; }
        public int IdTypeRelation { get; set; }
    }
}