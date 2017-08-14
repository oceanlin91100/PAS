using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.AS400;
using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class ASSPACATRepository
    {
        private string ConnectionString = string.Empty;
        public ASSPACATRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        //private string ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
        public List<ASSPACAT> Get()
        {
            var lists = new List<ASSPACAT>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return lists; }
                var sqlQuery = "SELECT ACID, ACNAME, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPACAT ";
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ASSPACAT item = new ASSPACAT();
                        item.ACID = Convert.ToInt32(reader["ACID"]);
                        item.ACNAME = Convert.ToString(reader["ACNAME"]);
                        item.ASTATUS = Convert.ToInt32(reader["ASTATUS"]);
                        item.DEF = Convert.ToString(reader["DEF"]);
                        item.CTOR = Convert.ToString(reader["CTOR"]);
                        item.CTDA = Convert.ToString(reader["CTDA"]);
                        item.MDOR = Convert.ToString(reader["MDOR"]);
                        item.MDDA = Convert.ToString(reader["MDDA"]);
                        lists.Add(item);
                    }
                }
            }
            return lists;
        }

        public ASSPACAT GetById(int id)
        {
            var result = new ASSPACAT();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return result; }
                string sqlQuery = string.Format("SELECT ACID, ACNAME, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPACAT WHERE ACID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.ACID = Convert.ToInt32(reader["ACID"]);
                        result.ACNAME = Convert.ToString(reader["ACNAME"]);
                        result.ASTATUS = Convert.ToInt32(reader["ASTATUS"]);
                        result.DEF = Convert.ToString(reader["DEF"]);
                        result.CTOR = Convert.ToString(reader["CTOR"]);
                        result.CTDA = Convert.ToString(reader["CTDA"]);
                        result.MDOR = Convert.ToString(reader["MDOR"]);
                        result.MDDA = Convert.ToString(reader["MDDA"]);
                    }
                }
            }
            return result;
        }

        public int GetLastId()
        {

            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return 0; }
                string sqlQuery = string.Format("SELECT * FROM HASDLIB.ASSPACAT");
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    string sqlQuery1 = string.Format("SELECT MAX(ACID) FROM HASDLIB.ASSPACAT");
                    var command1 = new OleDbCommand(sqlQuery1, connection);
                    var lastid = command1.ExecuteScalar();
                    return Convert.ToInt32(lastid);
                }
                else
                    return 0;
            }
        }

        public void Insert(ASSPACAT model)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("INSERT INTO HASDLIB.ASSPACAT(ACID, ACNAME, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA) VALUES ({0},'{1}',{2},'{3}','{4}','{5}','{6}','{7}')",
                    model.ACID, model.ACNAME, model.ASTATUS, model.DEF, model.CTOR, model.CTDA, model.MDOR, model.MDDA);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(ASSPACAT model, int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPACAT SET ACNAME = '{0}', ASTATUS = {1}, DEF = '{2}',MDOR = '{3}', MDDA = '{4}' WHERE ACID = {5}",
                    model.ACNAME, model.ASTATUS, model.DEF, model.MDOR, model.MDDA, id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPACAT WHERE ACID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

    }
}
