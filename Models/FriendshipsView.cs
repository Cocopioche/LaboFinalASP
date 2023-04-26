using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChatManager.Models
{
    public class FriendshipsView
    {
        public int Id { get; set; } //Id of the user
        public int IdRelation { get; set; }

        [JsonIgnore]
        public List<Relation> Relations
        {
            get
            {
                List<Relation> relationList = new List<Relation>();
                var relations = DB.Relations.Get(IdRelation);
                foreach (var relation in relationList)
                {
                    relationList.Add(relation);
                }

                return relationList;
            }
        }
    }
}