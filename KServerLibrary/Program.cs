using System;
using System.Net;
using System.Net.Sockets;

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
                client.HTML("<h5>some txt</h5>" +
                            "<div> werui</div>" +
                            "<button>click</button>");
            };
            
            controller.Get("/test2",dl2);
            
            
            
            action dl3 = delegate(KClient client) 
            {
                
                client.Json(new MyStruct("mnogo texta",5));
            };
            
            controller.Get("/json",dl3);
            
            
            
            
            
            
            controller.serve();
            
            

        }
    }
}
