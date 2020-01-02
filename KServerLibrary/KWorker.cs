using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KServerLibrary
{
    public class KWorker
    {

        struct Road_Delegate
        {
            public Delegate del;
            public string road;
            public Road_Delegate(string road, Delegate del)
            {
                this.del = del;
                this.road = road;
            }
        }
        
        List<Road_Delegate> rd = new List<Road_Delegate>();

        public void jfn()
        {
            if (new Regex(@"^/test\w*").IsMatch("/test2"))
            {
                Console.WriteLine("matched");
            }
            foreach (var rdi in rd)
            {
                if (new Regex(@"^/test\w*").IsMatch("/test2"))
                {
                    Console.WriteLine("matched");
                }
                
            }
            
        }


        public async void Resp2(object obj)
        {
            TcpClient client = (TcpClient) obj;
            string httpHeader200 = "HTTP/1.1 200 ok" + "\r\n";
            string contentHTML = "Content-Type: text/html" + "\r\n";
            string contentLength = "Content-Length: ";
            string endHeader = "\r\n";
            string endAnswer = endHeader + endHeader;

            NetworkStream stream = client.GetStream();
            byte[] bytes = new byte[2048];
            int i;

            while ((i = stream.Read(bytes, 0, bytes.Length)) > 0)
            {
                string clientData = System.Text.Encoding.ASCII.GetString(bytes);
                string road = clientData.Split("\r\n")[0].Split()[1];

                Console.WriteLine("----------------------");
                Console.WriteLine(road);
                Console.WriteLine("----------------------");


                string resp = "";

                bool matched = false;
                Delegate del = null;
                
                Console.WriteLine(Controller.roadMap.Keys.Count);

                foreach (var road2 in Controller.roadMap.Keys)
                {
                    var regex  =new Regex(@"^"+@road2+@"\w*");
                    if (regex.IsMatch(road))
                    {
                        Console.WriteLine(road2);
                        resp = Controller.roadMap[road2].answer(clientData);
                        Console.WriteLine(resp);
                        matched = true;
                        break;
                    }
                    
                }
                

//                if (Controller.roadMap.ContainsKey(road))
//                {
//                    Console.WriteLine("ENTERED ROAD");
//                    resp = Controller.roadMap[road].answer(clientData);
//                }
//                else
//                {
//                    resp = httpHeader200 + contentHTML + contentLength + "3" + endAnswer + "404" + endAnswer;
//                    Console.WriteLine(road);
//                    Console.WriteLine(Controller.roadMap.Keys.ToList()[0]);
//                }

                byte[] byteResp = System.Text.Encoding.ASCII.GetBytes(resp);

                stream.Write(byteResp);
                stream.Flush();
            }
        }

        public async void Resp(object obj)
        {
            TcpClient client = (TcpClient) obj;
            string httpHeader200 = "HTTP/1.1 200 ok" + "\r\n";
            string contentHTML = "Content-Type: text/html"+"\r\n";
            string contentLength = "Content-Length: ";
            string endHeader = "\r\n";
            string endAnswer = endHeader+endHeader;
            
            NetworkStream stream = client.GetStream();
            byte[] bytes = new byte[2048];
            int i;

            while ((i=stream.Read(bytes,0,bytes.Length))>0)
            {
                string clientData = System.Text.Encoding.ASCII.GetString(bytes);
                string road = clientData.Split("\r\n")[0];
                
                Console.WriteLine("----------------------");
                Console.WriteLine(clientData);
                Console.WriteLine("----------------------");

                
                string resp = "";

                if (Controller.roadMap.ContainsKey(road))
                {
                    Console.WriteLine("ENTERED ROAD");
                    resp = Controller.roadMap[road].answer(clientData);
                }
                else
                {
                    resp = httpHeader200 + contentHTML + contentLength + "3" + endAnswer + "404" + endAnswer;
                    Console.WriteLine(road);
                    Console.WriteLine(Controller.roadMap.Keys.ToList()[0]);
                }
                
                byte[] byteResp = System.Text.Encoding.ASCII.GetBytes(resp);
                
                stream.Write(byteResp);
                stream.Flush();
            }
        }
    }
}