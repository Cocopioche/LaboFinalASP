using ChatManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatManager.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        private User CurrentTarget
        {
            get
            {
                return (User)Session["CurrentTarget"];
            }
            set
            {
                Session["CurrentTarget"] = value;
            }
        }

        public ActionResult Index()
        {
            //get id of the current user in the session

            return View();

        }
        public ActionResult GetFriendList(bool forceRefresh = false)
        {
            if (forceRefresh || DB.Friendships.HasChanged)
            {
                //Send A list of all User tat is not the user in the sessions and is verified
                List<User> listUser = DB.Friendships.GetFriends(OnlineUsers.GetSessionUser());


                return PartialView(listUser);
            }
            return null;
        }
        public ActionResult GetMessages(bool forceRefresh = false)
        {
            if (forceRefresh || DB.Friendships.HasChanged || DB.Messages.HasChanged)
            {
                return PartialView();
            }
            return null;
        }
        public ActionResult SetCurrentTarget(int userId)
        {
            CurrentTarget = DB.Users.Get(userId);
            return null;
        }

    }
}