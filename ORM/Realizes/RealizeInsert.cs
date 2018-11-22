using ORM.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Realizes
{
    public class RealizeInsert<T> : RealizeToSql<T>, IInsert<T>
    {
        public int Insert(T models)
        {
            throw new System.NotImplementedException();
        }

        public int InsertBatch(IEnumerator<T> models)
        {
            throw new System.NotImplementedException();
        }

        public int InsertBatch(params T[] models)
        {
            throw new System.NotImplementedException();
        }

        private void ToInsert()
        {
            var typeT = typeof(T);
            if (typeT.IsArray)
            {
                throw new NotImplementedException();
            }
            var properties = typeT.GetProperties();
            //typeT.

            var sql = new StringBuilder($"INSERT INTO {1}");
            sql.Append($"\r\n  ");


            //            var sql = @"INSERT INTO rules
            //(
            //  id
            // ,created_at
            // ,updated_at
            // ,deleted_at 
            // ,schedule_id
            // ,type
            // ,rule_date
            //)
            //VALUES
            //(
            //  0 
            // ,NOW() 
            // ,NOW() 
            // ,NOW() 
            // ,0 
            // ,0 
            // ,NOW() 
            //);";
        }
    }
}
