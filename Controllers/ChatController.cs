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
            
            //return View(model);
            

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
            if (CurrentTarget != null)
            {
                if (forceRefresh || DB.Friendships.HasChanged || DB.Messages.HasChanged)
                {
                    List<Message> listMessage = DB.Messages.ToList();
                    foreach (var message in listMessage)
                    {
                        if (message.IdUserOther != CurrentTarget.Id)
                        {
                            listMessage.Remove(message);
                        }
                    }

                    return PartialView(listMessage);
                }
            }

            return null;
        }
        public ActionResult SetCurrentTarget(int userId)
        {
            CurrentTarget = DB.Users.Get(userId);
            return null;
        }
        
        public ActionResult Send(string message)
        {
            // Utilisez la valeur du message ici

            // ...
            
            //DB.Messages.Add()
            User currentUser = OnlineUsers.GetSessionUser();
            User envoieUser = CurrentTarget;
            Console.WriteLine(currentUser);
            Console.WriteLine(envoieUser);
            Console.WriteLine(message);

            Message message2 = new Message(currentUser.Id, envoieUser.Id, message);
            DB.Messages.Add(message2);

            return null;
        }

    }
}