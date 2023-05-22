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
        [OnlineUsers.UserAccess]
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
        [OnlineUsers.UserAccess(false)]
        public ActionResult GetMessages(bool forceRefresh = false)
        {
            if (forceRefresh || DB.Friendships.HasChanged || DB.Messages.HasChanged)
            {

                if (CurrentTarget != null)
                {
                  
                    List<Message> listMessageMain = DB.Messages.ToList();
                    List<Message> listMessage = new List<Message>();
                    foreach (var message in listMessageMain)
                    {
                      if (message.IdUserOther == CurrentTarget.Id)
                      {
                            listMessage.Add(message);
                       }
                    }
                    return PartialView(listMessage);
                }
                else
                    return PartialView(null);

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
          
            User currentUser = OnlineUsers.GetSessionUser();
            User envoieUser = CurrentTarget;
            Console.WriteLine(currentUser);
            Console.WriteLine(envoieUser);
            Console.WriteLine(message);

            OnlineUsers.AddNotification(currentUser.Id, $" nouveau message");

            Message message2 = new Message(currentUser.Id, envoieUser.Id, message);
            DB.Messages.Add(message2);


            return null;
        }
        public ActionResult Delete(int id)
        {
            DB.Messages.Delete(id);
            return PartialView(null); 
        }
        
        public ActionResult Update(int id, string message)
        {
            Message message2 = new Message(id,  OnlineUsers.GetSessionUser().Id, CurrentTarget.Id, message);

            DB.Messages.Update(message2);
            return PartialView(null);
        }


    }
}