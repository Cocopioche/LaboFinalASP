using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChatManager.Models
{
    public enum FriendshipStatus
    {
        Neutral,RequestReceive,OtherDecline,IDecline,RequestSend,Friend,Blocked
    }
    public class FriendshipsView
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public bool Accepted { get; set; }
        public bool Decline { get; set; }
        

        [JsonIgnore]
        public User Sender => DB.Users.Get(SenderId);
        [JsonIgnore]
        public User Receiver => DB.Users.Get(ReceiverId);

        //Empty Constructor for Repository do not use
        public FriendshipsView()
        {
        }
        public FriendshipsView(int senderId,int receiverId)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Accepted = false;
            Decline = false;
        }

        public bool IsUserIn(User user)
        {
            int id = user.Id;
            return  id == SenderId || id == ReceiverId;
        }

        public void SwitchUser()
        {
            (SenderId, ReceiverId) = (ReceiverId, SenderId);
        }

        

        public static bool DoesFriendshipExist(User user1, User user2)
        {
            foreach (FriendshipsView friendship in user1.Friendships)
            {
                if (friendship.IsUserIn(user2))
                {
                    return true;
                }
            }
            return false;
        }
        
        public static FriendshipsView GetMutualFriendship(User user1,User  user2)
        {
            foreach (FriendshipsView friendship in user1.Friendships)
            {
                if (friendship.IsUserIn(user2))
                {
                    return friendship;
                }
            }
            return null;
        }
        
        
        
        
    }
}