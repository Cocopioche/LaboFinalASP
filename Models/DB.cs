using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Hosting;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace ChatManager.Models
{
    public class DB
    {
        #region singleton setup
        private static readonly DB instance = new DB();
        public static DB Instance
        {
            get { return instance; }
        }
        #endregion
        #region Repositories
        public static Repository<Gender> Genders { get; set; }
        public static Repository<UserType> UserTypes { get; set; }
        public static Repository<UnverifiedEmail> UnverifiedEmails { get; set; }
        public static Repository<ResetPasswordCommand> ResetPasswordCommands { get; set; }
        public static Repository<Login> Logins { get; set; }
        public static UsersRepository Users { get; set; }
        public static Repository<TypeRelation> TypeRelations { get; set; }
        public static Repository<Message> Messages { get; set; }
        public static Repository<FriendshipsView> Friendships { get; set; }
        #endregion
        #region initialization
        public DB()
        {
            Genders = new Repository<Gender>();
            UserTypes = new Repository<UserType>();
            UnverifiedEmails = new Repository<UnverifiedEmail>();
            ResetPasswordCommands = new Repository<ResetPasswordCommand>();
            Logins = new Repository<Login>();
            Users = new UsersRepository();
            TypeRelations = new Repository<TypeRelation>();
            Messages = new Repository<Message>();
            Friendships = new Repository<FriendshipsView>();
            InitRepositories(this);
            UpdateAllRelation();
        }

        private static void InitRepositories(DB db)
        {
            string serverPath = HostingEnvironment.MapPath(@"~/App_Data/");
            PropertyInfo[] myPropertyInfo = db.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in myPropertyInfo)
            {
                if (propertyInfo.PropertyType.Name.Contains("Repository"))
                {
                    MethodInfo method = propertyInfo.PropertyType.GetMethod("Init");
                    method.Invoke(propertyInfo.GetValue(db), new object[] { serverPath + propertyInfo.Name + ".json" });
                }
            }
        }

        #endregion

        public void UpdateAllRelation()
        {
            List<User> users = Users.ToList();
            foreach (User user in users)
            {
                FriendshipsView friendshipsView = Friendships.Get(user.Id);
                if (friendshipsView == null)
                {
                    Friendships.Add(new FriendshipsView(user.Id, new List<Relation>()));
                }

                List<Relation> relations = friendshipsView.Relations;
                if (relations == null)
                {
                    relations = new List<Relation>();
                    Friendships.Add(new FriendshipsView(user.Id, relations));
                }
                foreach (User otherUser in users)
                {
                    if (user != otherUser)
                    {
                        bool isPresent = false;
                        foreach (Relation relation in relations)
                        {
                            if (relation.OtherUserId == otherUser.Id)
                            {
                                isPresent = true;
                                break;
                            }
                        }

                        if (!isPresent)
                        {
                            Relation relation = new Relation(otherUser.Id);
                            relations.Add(relation);
                            
                        }
                    }
                }
                Friendships.Update(new FriendshipsView(user.Id, relations));
            }
        }


    }
}