namespace ChatManager.Models
{
    public class FriendshipsRepository : Repository<FriendshipsView>
    {
        public void SendRequest(User senderUser,User receiverUser)
        {
            if (!FriendshipsView.DoesFriendshipExist(senderUser, receiverUser))
            {
                Add(new FriendshipsView(senderUser.Id,receiverUser.Id));
                return;
            }
            FriendshipsView mutualFriendship = FriendshipsView.GetMutualFriendship(senderUser,receiverUser);
            if (mutualFriendship.Decline && mutualFriendship.ReceiverId == senderUser.Id)
            {
                mutualFriendship.Decline = false;
                mutualFriendship.SwitchUser();
                Update(mutualFriendship);
            }
        }

        public void AcceptRequest(User senderUser,User receiverUser)
        {
            if (FriendshipsView.DoesFriendshipExist(senderUser, receiverUser))
            {
                FriendshipsView acceptedFriendShip = FriendshipsView.GetMutualFriendship(senderUser,receiverUser);
                acceptedFriendShip.Accepted = true;
                Update(acceptedFriendShip);
            }
        }

        public void RemoveRequest(User senderUser, User receiverUser)
        {
            if (FriendshipsView.DoesFriendshipExist(senderUser, receiverUser))
            {
                Delete(FriendshipsView.GetMutualFriendship(senderUser,receiverUser).Id);
            }
        }
        
        public void DeclineRequest(User senderUser,User receiverUser)
        {
            if (FriendshipsView.DoesFriendshipExist(senderUser, receiverUser))
            {
                FriendshipsView declinedFriendShip = new FriendshipsView(senderUser.Id,receiverUser.Id);
                declinedFriendShip.Decline = true;
                Update(declinedFriendShip);
            }
        }

        public void RemoveFriendships(User senderUser, User receiverUser)
        {
            if (FriendshipsView.DoesFriendshipExist(senderUser, receiverUser))
            {
                Delete(FriendshipsView.GetMutualFriendship(senderUser,receiverUser).Id);
            }
        }
        
        
        
        
    }
}