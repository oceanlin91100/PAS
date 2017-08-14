using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.AS400;
using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class ASSPAGROUPRepository
    {
        private string ConnectionString = string.Empty;
        public ASSPAGROUPRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        //private string ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
        public List<ASSPAGROUP> Get()
        {
            var lists = new List<ASSPAGROUP>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return lists; }
                var sqlQuery = "SELECT AGID, AGNAME, APJPCD, APUTCD, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPAGROUP";
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ASSPAGROUP item = new ASSPAGROUP();
                        item.AGID = Convert.ToInt32(reader["AGID"]);
                        item.AGNAME = Convert.ToString(reader["AGNAME"]);
                        item.APJPCD = Convert.ToString(reader["APJPCD"]);
                        item.APUTCD = Convert.ToString(reader["APUTCD"]);
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

        public ASSPAGROUP GetById(int id)
        {
            var item = new ASSPAGROUP();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return item; }
                string sqlQuery = string.Format("SELECT AGID, AGNAME, APJPCD, APUTCD, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPAGROUP WHERE AGID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        item.AGID = Convert.ToInt32(reader["AGID"]);
                        item.AGNAME = Convert.ToString(reader["AGNAME"]);
                        item.APJPCD = Convert.ToString(reader["APJPCD"]);
                        item.APUTCD = Convert.ToString(reader["APUTCD"]);
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
                string sqlQuery = string.Format("SELECT * FROM HASDLIB.ASSPAGROUP");
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    string sqlQuery1 = string.Format("SELECT MAX(AGID) FROM HASDLIB.ASSPAGROUP");
                    var command1 = new OleDbCommand(sqlQuery1, connection);
                    var lastid = command1.ExecuteScalar();
                    if (lastid == null) { return 0; }
                    return Convert.ToInt32(lastid);
                }
                else
                    return 0;
            }
        }

        public void Insert(ASSPAGROUP model)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("INSERT INTO HASDLIB.ASSPAGROUP(AGID, AGNAME, APJPCD, APUTCD, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA) VALUES ({0},'{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}')",
                    model.AGID, model.AGNAME, model.APJPCD, model.APUTCD, model.ASTATUS, model.DEF, model.CTOR, model.CTDA, model.MDOR, model.MDDA);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(ASSPAGROUP model, int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPAGROUP SET AGNAME = '{1}', APJPCD = '{6}', APUTCD = '{7}', ASTATUS = {2}, DEF = '{3}',MDOR = '{4}', MDDA = '{5}' WHERE AGID = {0}",
                    id, model.AGNAME, model.ASTATUS, model.DEF, model.MDOR, model.MDDA, model.APJPCD, model.APUTCD);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPAGROUP WHERE AGID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

    }
}

