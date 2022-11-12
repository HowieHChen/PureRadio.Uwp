using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Constants
{
    public static class ServiceConstants
    {
        public const string DefaultAcceptString = "*/*";
        public const string DefaultUserAgentString = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36 Edg/92.0.902.62";


        public static class Messages
        {
            public const string OverallTimeoutCannotBeSet = "全局超时未能在第一次请求后设置";
        }

        public static class Params
        {
            public const string AccessToken = "access_token";
            public const string QingTingId = "qingting_id";
            public const string DeviceId = "device_id";
            public const string TimeStamp = "ts";
            public const string Time = "t";
            public const string Sign = "sign";
            public const string CurPage = "curpage";
            public const string PageSize = "pagesize";
            public const string Order = "order";

        }

        public static class Query
        {
            public const string GrantType = "grant_type";
            public const string RefreshToken = "refresh_token";
            public const string QingTingId = "qtId";
            public const string Order = "order";
            public const string AccountType = "account_type";
            public const string DeviceId = "device_id";
            public const string Password = "password";
            public const string AreaCode = "area_code";
            public const string UserId = "user_id";
        }

        public static class Order
        {
            public const string Asc = "asc";
            public const string Desc = "desc";
        }
    }
}
