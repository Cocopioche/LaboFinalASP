using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatManager.Models;
using Login = System.Web.UI.WebControls.Login;

namespace ChatManager.Controllers
{
    public class FriendshipsController : Controller
    {
        // GET: Friendships
        [OnlineUsers.UserAccess]
        public ActionResult Index()
        {
            //get id of the current user in the session
            int id = (int)Session["currentLoginId"];
            FriendshipsView model = DB.Friendships.Get(id);
            return View(model);
        }

        [OnlineUsers.UserAccess(false /* do not reset timeout*/)]
        public ActionResult Friendships(bool forceRefresh = false, string search = "")
        {
            if (forceRefresh || DB.Friendships.HasChanged)
            {
                List<User> listUser = new List<User>();
                listUser.Add(OnlineUsers.GetSessionUser());

                if (!string.IsNullOrEmpty(search))
                {
                    listUser.AddRange(DB.Users.ToList().Where(x => x.Id != OnlineUsers.GetSessionUser().Id &&
                                                                   x.Verified &&
                                                                   (x.FirstName.ToLower().Contains(search.ToLower()) ||
                                                                    x.LastName.ToLower().Contains(search.ToLower()))).ToList());
                }
                else
                {
                    listUser.AddRange(DB.Users.ToList().Where(x => x.Id != OnlineUsers.GetSessionUser().Id &&
                                                                   x.Verified).ToList());
                }

                return PartialView(listUser);
            }

            return null;
        }


        public void SendFriendRequest(User otherUser)
        {
            User mainUser = OnlineUsers.GetSessionUser();
            DB.Friendships.SendRequest(mainUser, otherUser);
        }


        public void AcceptFriendRequest(User otherUser)
        {
            User mainUser = OnlineUsers.GetSessionUser();
            DB.Friendships.AcceptRequest(mainUser, otherUser);
        }


        public void DeclineFriendRequest(User otherUser)
        {
            User mainUser = OnlineUsers.GetSessionUser();
            DB.Friendships.DeclineRequest(mainUser, otherUser);
        }


        public void RemoveFriend(User otherUser)
        {
            User mainUser = OnlineUsers.GetSessionUser();
            DB.Friendships.RemoveFriendships(mainUser, otherUser);
        }


        public void RemoveRequest(User otherUser)
        {
            User mainUser = OnlineUsers.GetSessionUser();
            DB.Friendships.RemoveRequest(mainUser, otherUser);
        }
    }
}