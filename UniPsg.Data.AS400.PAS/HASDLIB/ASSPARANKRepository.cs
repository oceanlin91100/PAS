using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.AS400;
using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class ASSPARANKRepository
    {
        private string ConnectionString = string.Empty;
        public ASSPARANKRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        //private string ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
        public List<ASSPARANK> Get()
        {
            var lists = new List<ASSPARANK>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return lists; }
                var sqlQuery = "SELECT ARID, ARNAME, WEIGHT, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPARANK";
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ASSPARANK item = new ASSPARANK();
                        item.ARID = Convert.ToInt32(reader["ARID"]);
                        item.ARNAME = Convert.ToString(reader["ARNAME"]);
                        item.WEIGHT = Convert.ToInt32(reader["WEIGHT"]);
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

        public ASSPARANK GetById(int id)
        {
            var item = new ASSPARANK();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return item; }
                string sqlQuery = string.Format("SELECT ARID, ARNAME, WEIGHT, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPARANK WHERE ARID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        item.ARID = Convert.ToInt32(reader["ARID"]);
                        item.ARNAME = Convert.ToString(reader["ARNAME"]);
                        item.WEIGHT = Convert.ToInt32(reader["WEIGHT"]);
                        item.ASTATUS = Convert.ToInt32(reader["ASTATUS"]);
                        item.DEF = Convert.ToString(reader["DEF"]);
                        item.CTOR = Convert.ToString(reader["CTOR"]);
                        item.CTDA = Convert.ToString(reader["CTDA"]);
                        item.MDOR = Convert.ToString(reader["MDOR"]);
                        item.MDDA = Convert.ToString(reader["MDDA"]);
                    }
                }
            }
            return item;
        }

        public int GetLastId()
        {

            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return 0; }
                string sqlQuery = string.Format("SELECT * FROM HASDLIB.ASSPARANK");
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    string sqlQuery1 = string.Format("SELECT MAX(ARID) FROM HASDLIB.ASSPARANK");
                    var command1 = new OleDbCommand(sqlQuery1, connection);
                    var lastid = command1.ExecuteScalar();
                    if (lastid == null) { return 0; }
                    return Convert.ToInt32(lastid);
                }
                else
                    return 0;
            }
        }

        public void Insert(ASSPARANK model)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("INSERT INTO HASDLIB.ASSPARANK(ARID, ARNAME, WEIGHT, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA) VALUES ({0},'{1}',{2},{3},'{4}','{5}','{6}','{7}','{8}')",
                    model.ARID, model.ARNAME, model.WEIGHT, model.ASTATUS, model.DEF, model.CTOR, model.CTDA, model.MDOR, model.MDDA);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(ASSPARANK model, int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPARANK SET ARNAME = '{1}', ASTATUS = {2}, DEF = '{3}',MDOR = '{4}', MDDA = '{5}', WEIGHT= {6} WHERE ARID = {0}",
                    id, model.ARNAME, model.ASTATUS, model.DEF, model.MDOR, model.MDDA, model.WEIGHT);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPARANK WHERE ARID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

    }
}

