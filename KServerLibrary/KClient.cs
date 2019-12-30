using System;

namespace KServerLibrary
{
    delegate void action(KClient client);
    public class KClient
    {
        private string response = "";
        
        string httpHeader200 = "HTTP/1.1 200 ok" + "\r\n";
        string contentHTML = "Content-Type: text/html"+"\r\n";
        string contentLength = "Content-Length: ";
        string endHeader = "\r\n";
        string endAnswer = "\r\n\r\n";

        private action makeResponse;

        public string Response
        {
            get => response;
            set => response = value;
        }

        public void HTML(string html)
        {
            string resp = httpHeader200 + contentHTML + contentLength + html.Length.ToString() + endAnswer + html + endAnswer;
            response = resp;
        }

        public KClient(Delegate del)
        {
            makeResponse = (action) del;
        }

        public KClient()
        {
            
        }


        public string answer()
        {
            makeResponse(this);
            return response;
        }
        




    }
}