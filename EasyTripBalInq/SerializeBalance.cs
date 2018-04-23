using Newtonsoft.Json;

namespace EasyTripBalInq
{
    public class SerializeBalance
    {
        public string ConvertToContent(string accInfo)
        {
            string empty = string.Empty;
            return JsonConvert.SerializeObject(new AccountObuIDInfo
            {
                AccountObuID = accInfo
            });
        }
    }
}