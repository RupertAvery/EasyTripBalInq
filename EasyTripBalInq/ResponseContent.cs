using System.Collections.Generic;

namespace EasyTripBalInq
{
    public class ResponseContent
    {
        public List<CustomerInfo> CustomerInfo
        {
            get;
            set;
        }

        public List<ReloadInfo> ReloadInfo
        {
            get;
            set;
        }
    }
}