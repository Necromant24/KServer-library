﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

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

        //TODO: ДОДЕЛАТЬ И ПРОТЕСТИРОВАТЬ POST ЗАПРОСЫ
        public void Post(string road, Delegate del)
        {
            roadMap.Add("POST "+road+" HTTP/1.1",new KClient(del));
        }
        
        
        


        public async void serve(string ip="0.0.0.0",int port=3000)
        {
            IPAddress address = IPAddress.Parse(ip);
            TcpListener listener = new TcpListener(address,port);
            
            listener.Start();
            
            Console.WriteLine("server started on: "+ip+" and port "+port);
            Console.WriteLine(Environment.ProcessorCount +" is processsors count");

            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.SetMaxThreads(3, 3);

            KWorker resper = new KWorker();
            
            while (true)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(resper.Resp),listener.AcceptTcpClient());
            }
        }

        
        

        
        
    }
}