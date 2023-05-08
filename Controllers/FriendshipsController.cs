using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatManager.Models;

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
        [OnlineUsers.UserAccess(false/* do not reset timeout*/)]
        public ActionResult Friendships(bool forceRefresh = false)
        {
            if (forceRefresh || DB.Friendships.HasChanged)
            {
                //Send A list of all User tat is not the user in the sessions and is verified
                var listUser = DB.Users.ToList().Where(u => u.Id == (int)Session["currentLoginId"]).ToList();
                listUser.AddRange(DB.Users.ToList().Where(x => x.Id != (int)Session["currentLoginId"] && x.Verified).ToList());
                return PartialView(listUser);
            }
            return null;
        }

        public ActionResult SendFriendDemand(User otherUser)
        {
            int id = (int)Session["currentLoginId"];
            DB.Friendships.Add(new FriendshipsView(id, otherUser.Id));
            return RedirectToAction("Index");
        }
    }
}