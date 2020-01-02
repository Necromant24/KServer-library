using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;

namespace KServerLibrary
{
    class Program
    {

        struct MyStruct
        {
            public string data;
            public int count;

            public MyStruct(string text, int cnt)
            {
                data = text;
                count = cnt;
            }
        }
        static void Main(string[] args)
        {
            
            var tst = new KWorker();
            tst.jfn();
            //return;
            
            
            

            Controller controller = new Controller();
            
            action dl = delegate(KClient client)
            {
                client.HTML("<h3>works!</h3><h1>Yra!!!</h1>");
            };
            
            controller.Get("/tost",dl);
            
            action dl2 = delegate(KClient client) 
            { 
                Console.WriteLine(client.RawText);
                client.HTML("<h5>some txt</h5>" +
                            "<div> werui</div>" +
                            "<button>click</button>");
                Console.WriteLine("ended test response create");
            };
            
            controller.Get("/test2",dl2);
            
            
            action dl3 = delegate(KClient client) 
            {
                client.Json(new MyStruct("mnogo texta",5));
            };
            
            controller.Get("/json",dl3);
            
            
            
            
            action dlwait = delegate(KClient client)
            {
                client.HTML("waited some time");
                Thread.Sleep(3000);
                Console.WriteLine("has waited");
            };
            
            controller.Get("/wait",dlwait);
            
            action postdel = delegate(KClient client)
            {
                Console.WriteLine(client.GetHeader("Content-Type"));
                
                client.PlainText("posted");
            };
            
            controller.Post("/tpost",postdel);
            

            Thread th = new Thread(test);
            //th.Start();
            
            controller.serve();
            
        }

        static void test()
        {
            Thread.Sleep(5000);
//            HttpClient client = new HttpClient();
//            Dictionary<string, string> data = new Dictionary<string, string>()
//            {
//                {"name1","data1"},
//                {"name2","data2"}
//            };
//            
//            var content = new FormUrlEncodedContent(data);
//            
//            client.PostAsync("http://localhost:3000/tpost",content);

            var request = (HttpWebRequest) WebRequest.Create("http://localhost:3000/tpost");
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWr = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{\"tkey\": \"tval\"}";
                streamWr.Write(json);
            }

            var resp = request.GetResponse();

        }
        
        
        
    }
}
