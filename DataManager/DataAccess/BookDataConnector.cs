using DataManager.Library.Internal.DataAccess;
using DataManager.Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataManager.Library.DataAccess
{
    public class BookDataConnector : IBookDataConnector
    {
        private readonly ISqlConnector sql;

        public BookDataConnector(ISqlConnector sql)
        {
            this.sql = sql;
        }

        public async Task<List<BookDataModel>> GetAll()
        {
            List<BookDataModel> result = await sql.LoadData<BookDataModel, dynamic>("spBook_GetAll", null, "BookcaseData");
            return result;
        }

        public async Task<BookDataModel> GetByIsbn(string ISBN)
        {
            List<BookDataModel> result = await sql.LoadData<BookDataModel, dynamic>("spBook_GetByIsbn", new { ISBN }, "BookcaseData");
            return result.FirstOrDefault();
        }

        public async Task<BookDataModel> GetById(string Id)
        {
            List<BookDataModel> result = await sql.LoadData<BookDataModel, dynamic>("spBook_GetById", new { Id }, "BookcaseData");
            return result.FirstOrDefault();
        }

        public async Task Insert(BookDataModel model)
        {
            await sql.SaveData("spBook_Insert", model, "BookcaseData");
        }
    }
}
