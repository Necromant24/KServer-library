using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace KServerLibrary
{
    public class Controller
    {
        
        Queue<TcpClient> clients = new Queue<TcpClient>();
        
        

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
            
            KWorker resper = new KWorker();
            
            
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                resper.Resp(client);
            }
        }

        
        
        
        
    }
}