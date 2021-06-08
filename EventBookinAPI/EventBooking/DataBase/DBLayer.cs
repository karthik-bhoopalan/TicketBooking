using EventBooking.Data;
using EventBooking.Modal;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.DataBase
{
    public class DBLayer : IDBLayer
    {
        private readonly AuthenticationContext _Context;

        public DBLayer(AuthenticationContext Context)
        {
            _Context = Context ?? throw new ArgumentNullException(nameof(Context));
        }

        public async Task<string> ExecuteAsyncSql(SqlParameter[] param, string queryString, int outparameterValue)
        {
            int affectedRows = await _Context.Database.ExecuteSqlRawAsync(queryString, param);
            string ResponseMessage = Convert.ToString(param[outparameterValue].Value);
            return ResponseMessage;
        }

        public async Task<List<T>> ExecuteAsyncTableSql<T>(string procedureName) where T : class
        {
            var events = _Context.Set<T>().FromSqlRaw(procedureName).ToList();
            return events;
        }

        public async Task<List<T>> ExecuteAsyncTableProcedure<T>(SqlParameter[] parameters, string Query) where T : class
        {
            var events = _Context.Set<T>().FromSqlRaw(Query, parameters).ToList();
            return events;
        }
    }
}
