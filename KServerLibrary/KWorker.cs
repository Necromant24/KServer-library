using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace KServerLibrary
{
    public class KWorker
    {
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