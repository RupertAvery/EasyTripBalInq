using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace EasyTripBalInq
{
    public class BalInqClass
    {
        public List<string> SendWS(string crpContent, string id)
        {
            List<string> list = new List<string>();
            string item = string.Empty;
            string empty = string.Empty;
            try
            {
                empty = JsonConvert.SerializeObject(new clsRequestBody
                {
                    Content = crpContent
                });
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.easytripcustomer.ph/ESCCB_WS/ESCCB.aspx/P01");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Timeout = 300000;
                httpWebRequest.Headers.Add("ID", id);
                httpWebRequest.Headers.Add("AppVersion", "2.0");
                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(empty);
                    streamWriter.Flush();
                }
                using (StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream()))
                {
                    item = streamReader.ReadToEnd();
                }
                list.Add("OK");
                list.Add(item);
                return list;
            }
            catch (WebException ex)
            {
                string item2 = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                list.Add("NOK");
                list.Add(item2);
                return list;
            }
        }
    }
}