using EventBooking.Modal;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.DataBase
{
    public interface IDBLayer
    {
        Task<string> ExecuteAsyncSql(SqlParameter[] parameters, string queryString, int outparameterValue);
        Task<List<T>> ExecuteAsyncTableSql<T>(string procedureName) where T : class;

        Task<List<T>> ExecuteAsyncTableProcedure<T>(SqlParameter[] parameters, string Query) where T : class;
    }
}
