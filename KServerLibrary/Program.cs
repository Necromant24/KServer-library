using System;
using System.Net;
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
                Thread.Sleep(2000);
                Console.WriteLine("has waited");
            };
            
            controller.Get("/wait",dlwait);
            
            action postdel = delegate(KClient client)
            {
                client.PlainText("posted");
            };
            
            controller.Post("/tpost",postdel);
            
            
            
            
            
            
            
            
            
            controller.serve();
            
            

        }
    }
}
