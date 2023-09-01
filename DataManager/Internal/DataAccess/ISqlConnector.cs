using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataManager.Library.Internal.DataAccess
{
    public interface ISqlConnector
    {
        Task<List<T>> LoadData<T, U>(string storedProc, U parameters, string connectionStringKey);
        Task<int> SaveData<T>(string storedProc, T parameters, string connectionStringKey);
    }
}