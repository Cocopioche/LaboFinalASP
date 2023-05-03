﻿using System;
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
                return PartialView(DB.Friendships.ToList().OrderBy(c => c.User.LastName));
            }
            return null;
        }
    }
}