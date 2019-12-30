using System;
using System.Net;
using System.Net.Sockets;

namespace KServerLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            
            action dl = delegate(KClient client)
            {
                client.HTML("<h3>works!</h3>");
            };
            
            Controller.roadMap.Add("GET /tost HTTP/1.1",new KClient(dl));
            
            string httpHeader200 = "HTTP/1.1 200 ok" + "\r\n";
            string contentHTML = "Content-Type: text/html"+"\r\n";
            
            
            string contentLength = "Content-Length: ";
            string endHeader = "\r\n";
            string endAnswer = endHeader+endHeader;
            
            Controller controller = new Controller();
            controller.serve();
            
            

        }
    }
}
