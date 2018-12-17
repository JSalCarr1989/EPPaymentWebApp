using System.Data;

namespace EPPaymentWebApp.Interfaces
{
    public interface IDbConnectionRepository
    {
        IDbConnection CreateDbConnection();
        string GetEpPaymentConnectionString();
    }
}
