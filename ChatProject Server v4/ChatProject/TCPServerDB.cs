using System;
using System.Collections.Generic;
using System.Linq;
using ChatDB;
using PostOffice;

namespace ChatProject
{
    //class for handling data to and from DB
    public static class TCPServerDB
    {
        #region Variables and Declarations
        private static ChatEntities _entities;
        #endregion

        #region Initialization

        public static void Initialize()
        {
            _entities = new ChatEntities();
        }

        #endregion

        # region Working with Messages in DB

        //adds the parcel to DB
        internal static void AddNewMessage(Parcel parcelToAdd)
        {
            StoredMessage message = new StoredMessage
            {
                MessageText = parcelToAdd.Msg,
                UserId = FindIdFromName(parcelToAdd.UserName),
                SentDate = parcelToAdd.TimeStamp.Date
            };

            _entities.StoredMessages.Add(message);
            _entities.SaveChanges();
        }

        public static List<StoredMessage> LookUpStoredMessages(DateTime date)
        {
            var messagesQ = from m in _entities.StoredMessages where m.SentDate == date.Date select m;
            return messagesQ.ToList();
        }

        public static List<StoredMessage> LookUpStoredMessages(string keyword)
        {
            var messagesQ = from m in _entities.StoredMessages where m.MessageText.Contains(keyword) select m;
            return messagesQ.ToList();
        }

        #endregion

        #region Working with Users in DB

        public static int? FindIdFromName(string userName)
        {
            var userQr = from u in _entities.ExistingUsers where u.UserName == userName select u;
            var singleOrDefault = userQr.SingleOrDefault();
            if (singleOrDefault != null)
            {
                return singleOrDefault.UserId;
            }
            return null;
        }

        public static ExistingUser FindByUserName(string userName)
        {
            var userQr = from u in _entities.ExistingUsers where u.UserName == userName select u;
            return userQr.SingleOrDefault();
        }

        public static List<ExistingUser> FindByUserNameList(string userName)
        {
            var userQr = from u in _entities.ExistingUsers where u.UserName == userName select u;
            return new List<ExistingUser> { userQr.SingleOrDefault() };
        }

        public static ExistingUser FindById(string iddb)
        {
            return _entities.ExistingUsers.Find(iddb);
        }

        public static List<ExistingUser> FindByIdList(string iddb)
        {
            return new List<ExistingUser> { _entities.ExistingUsers.Find(iddb) };
        }

        internal static void AddNewUser(string userName, string name)
        {
            ExistingUser user = new ExistingUser
            {
                UserName = userName,
                IsConnected = true,
                LastConnection = DateTime.Now,
                Name = name
            };
            _entities.ExistingUsers.Add(user);
            _entities.SaveChanges();
        }

        public static void RemoveUserbyUserName(string userName)
        {
            ExistingUser userToRemove = FindByUserName(userName);
            if (userToRemove == null) return; 
            if (userToRemove.IsConnected != null && (bool)userToRemove.IsConnected) return;//user is connected
                //MessageBox.Show("User is still connected!");
                _entities.ExistingUsers.Remove(userToRemove);
            _entities.SaveChanges();
        }

        public static void RemoveUserbyID(string iddb)
        {
            ExistingUser userToRemove = FindById(iddb);
            if (userToRemove == null) return;
            if (userToRemove.IsConnected != null && (bool) userToRemove.IsConnected) return;//user is connected
                _entities.ExistingUsers.Remove(userToRemove);
            _entities.SaveChanges();
        }

        internal static void UpdateUserConnectionStatus(string userName, bool connected)
        {
            ExistingUser userToUpdate = FindByUserName(userName);
            if (userToUpdate.IsConnected == null) return;
            userToUpdate.IsConnected = connected;
            _entities.SaveChanges();
        }

        #endregion
    }
}
