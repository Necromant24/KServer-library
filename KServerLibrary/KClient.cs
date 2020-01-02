using System;
using System.Collections.Generic;
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
        private string rawBody = "";
        
        
        // all client headers data structure
        Dictionary<string,string> clientHeaders = new Dictionary<string, string>();
        
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
        
        public string RawBody => rawBody;

        public string GetHeader(string name)
        {
            return clientHeaders[name];
        }
        

        private void parseRequest()
        {
            string[] data = rawText.Split("\r\n\r\n");
            rawBody = data[1];
            string rawHeaders = data[0];
            string[] masHeaders = rawHeaders.Split("\r\n");
            for (int i = 1; i < masHeaders.Length; i++)
            {
                string[] name_Header = masHeaders[i].Split(":");
                clientHeaders[name_Header[0]] = name_Header[1].Trim();
            }
            
        }


        public string RawHeaders()
        {
            string[] data = rawText.Split("\r\n\r\n");
            rawBody = data[1];
            return data[0];
        }

        private void parseHeaders()
        {
            string rawHeaders = RawHeaders();
            string[] masHeaders = rawHeaders.Split("\r\n");
            for (int i = 1; i < masHeaders.Length; i++)
            {
                string[] name_Header = masHeaders[i].Split(":");
                clientHeaders[name_Header[0]] = name_Header[1].Trim();
            }
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
        


        public string answer(string rawData)
        {
            rawText = rawData;
            parseRequest();
            makeResponse(this);
            return response;
        }
        




    }
}