using System;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace KServerLibrary
{
    delegate void action(KClient client);
    public class KClient
    {
        private string response = "";
        private string rawText = "";
        private string rawHeaders = "";
        
        string httpHeader200 = "HTTP/1.1 200 ok" + "\r\n";
        string contentHTML = "Content-Type: text/html"+"\r\n";
        string contentJson = "Content-Type: text/json"+"\r\n";
        string contentText = "Content-Type: text/plain"+"\r\n";
        
        
        string contentLength = "Content-Length: ";
        string endHeader = "\r\n";
        string endAnswer = "\r\n\r\n";

        private action makeResponse;

        public string Response
        {
            get => response;
            set => response = value;
        }

        public string RawText
        {
            get => rawText;
            set => rawText = value;
        }


        public string RawHeaders()
        {
            return rawText.Split("\r\n\r\n")[0];
        }

        public void parseHeaders()
        {
            
            
        }

        



        public void Json(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            response = httpHeader200 + contentJson + contentLength + json.Length + endAnswer + json + endAnswer;
        }

        public void PlainText(string text)
        {
            response = httpHeader200 + contentText + contentLength + text.Length + endAnswer + text + endAnswer;
        }
        

        public void HTML(string html)
        {
            string resp = httpHeader200 + contentHTML + contentLength + html.Length + endAnswer + html + endAnswer;
            response = resp;
        }

        public KClient(Delegate del)
        {
            makeResponse = (action) del;
        }

        public KClient()
        {
            
        }


        public string answer(string rawData)
        {
            rawText = rawData;
            makeResponse(this);
            return response;
        }
        




    }
}