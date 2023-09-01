using DataManager.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataManager.Library.DataAccess
{
    public interface IBookDataConnector
    {
        Task<List<BookDataModel>> GetAll();
        Task<BookDataModel> GetById(string Id);
        Task<BookDataModel> GetByIsbn(string ISBN);
        Task Insert(BookDataModel model);
    }
}