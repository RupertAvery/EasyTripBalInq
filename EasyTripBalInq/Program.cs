using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyTripBalInq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your account number...");
			string acctNo = Console.ReadLine();
            Console.WriteLine("Sending request to EasyTrip service...");
            DoWSRequest(acctNo);
            Console.ReadLine();
        }

        private static void DoWSRequest(string strAcct)
        {
            try
            {
                SerializeBalance serializeBalance = new SerializeBalance();
                BalInqClass balInqClass = new BalInqClass();
                string deviceId = "123456789012345"; // Uses IMEI but any value will do
                string passPhrase =
                    "OAUNEW5EHNAU25Z9Z1T0A3R67YS0ZH2JGV89HSD2MDNN0HN6U90KJMDVOLO37O0ZMV4BKA9M5I5SISA0XZMYGEWE6CJAKYFQG1WC6KCOTA737E02LVHAPOX28KMD1UKD";
                string passPhrase2 =
                    "C2TH1KWTLLK3ICLUCTYGLO3WILVOPYYRADUYCQSTT58WQBZIZBXEW6FVJ9OODMZJ36KUD3T6Y25HQOMOO0OLOONGVU1KN9IOA8XHWRWAFNOM7H6517BFZPGYRFYBQCEZ";
                string crpContent = StringCipher.Encrypt(serializeBalance.ConvertToContent(strAcct), passPhrase2);
                string id = StringCipher.Encrypt("02:00:00:00:00:00" + deviceId.ToString(), passPhrase);
                List<string> list = balInqClass.SendWS(crpContent, id);

                if (list[0] == "OK")
                {
                    string value =
                        StringCipher.Decrypt(JsonConvert.DeserializeObject<ReturnResponse>(list[1]).Return.Result,
                            passPhrase2);
                    ResponseContent responseCont = JsonConvert.DeserializeObject<ResponseContent>(value);
                    //Console.WriteLine(responseCont.CustomerInfo[0].OBUID);
                    //Console.WriteLine(responseCont.CustomerInfo[0].CustAccountID.ToString());
                    Console.WriteLine(responseCont.CustomerInfo[0].Name);
                    Console.WriteLine(responseCont.CustomerInfo[0].Balance.ToString());
                    Console.WriteLine(responseCont.ReloadInfo[0].DateCreated + " " +
                                      responseCont.ReloadInfo[0].Description);
                    Console.WriteLine(responseCont.CustomerInfo[0].status);
                }
            }
            catch (Exception ex2)
            {
                Console.WriteLine(ex2.Message);
            }
        }
    }
}
