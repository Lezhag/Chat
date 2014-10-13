using System.Net;

namespace ChatProject
{
    //wrapper class for address data
    public class Connection
    {
        public IPAddress IP { get; set; }
        public ushort Port { get; set; }
        
        //default ctor
        public Connection()
        {
            IP = IPAddress.Parse("127.0.0.1");
            Port = 4000;
        }

        /*public Connection(string Address, ushort P)
        {
            IP = IPAddress.Parse(Address);
            Port = P;
        }*/
    }
  
}
