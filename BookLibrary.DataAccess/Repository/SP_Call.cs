using BookLibrary.DataAccess.Repository.IRepository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrary.DataAccess.Repository
{
    public class SP_Call : ISP_Call
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Execute(string procedureName, DynamicParameters param = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null)
        {
            throw new NotImplementedException();
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null)
        {
            throw new NotImplementedException();
        }

        public T OneRecord<T>(string procedureName, DynamicParameters param = null)
        {
            throw new NotImplementedException();
        }

        public T Single<T>(string procedureName, DynamicParameters param = null)
        {
            throw new NotImplementedException();
        }
    }
}
