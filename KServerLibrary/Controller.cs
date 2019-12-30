using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace KServerLibrary
{
    public class Controller
    {
        
        List<TcpClient> clients = new List<TcpClient>();

        public static Dictionary<string,KClient> roadMap = new Dictionary<string, KClient>();
        
        public void Get(string road, Delegate del)
        {
            roadMap.Add("GET "+road+" HTTP/1.1",new KClient(del));
        }


        public void serve(string ip="0.0.0.0",int port=3000)
        {
            IPAddress address = IPAddress.Parse(ip);
            TcpListener listener = new TcpListener(address,port);
            
            listener.Start();
            
            Console.WriteLine("server started on: "+ip+" and port "+port);
            
            Test resper = new Test();
            
            
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                resper.Resp(client);
            }
        }

        
        
        
        
    }
}