using System;

namespace ChatProject
{
    //class for propagating data of the connected Client to Server UI
    public class ClientConnectedArgs:EventArgs
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime ConnectionTime { get; set; }
       
    }
}
