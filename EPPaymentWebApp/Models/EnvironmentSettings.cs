using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPPaymentWebApp.Models
{
    public class EnvironmentSettings
    {
        public string MpSk { get; set; }
        public string ConnectionString { get; set; }
        public string UrlSuccess { get; set; }
        public string UrlFailure { get; set; }
    }
}
