using System;
using System.Drawing;

namespace PostOffice
{
    // class holding the data to send accross the chat
    [Serializable]
    public class Parcel : EventArgs
    {
        #region Public properties

        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Msg { get; set; }
        public Color Colour { get; set; }
        public string SenderID { get; set; }

        #endregion

        #region Ctors

        //for diagnostic Debug.WriteLine calls
        public override string ToString()
        {
            return TimeStamp.ToShortTimeString() + "-" + UserName + ": " + Msg;
        }

        //default ctor
        public Parcel()
        {

        }
        //ctor with empty senderId
        public Parcel(string name, DateTime dt, string m, Color c)
        {
            UserName = name;
            TimeStamp = dt;
            Msg = m;
            Colour = c;
            SenderID = "empty";
        }
        //ctor with SenderId
        public Parcel(string name, DateTime dt, string m, Color c, string id)
        {
            UserName = name;
            TimeStamp = dt;
            Msg = m;
            Colour = c;
            SenderID = id;
        }

        #endregion
    }
}