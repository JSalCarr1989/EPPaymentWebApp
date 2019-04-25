using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPPaymentWebApp.Models;


namespace EPPaymentWebApp.Interfaces
{
    public interface IEnvironmentSettingsRepository
    {
        string GetConnectionStringSetting();
        string GetMpSK();
        string GetUrlSuccess();
        string GetUrlFailure();
    }
}
