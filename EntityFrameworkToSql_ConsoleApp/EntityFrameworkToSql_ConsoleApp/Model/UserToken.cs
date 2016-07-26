using System;

namespace EntityFrameworkToSql_ConsoleApp.Model
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public string LoginId { get; set; }

        public string DeviceId { get; set; }

        public DateTime ExpiryTime { get; set; }

    }
}