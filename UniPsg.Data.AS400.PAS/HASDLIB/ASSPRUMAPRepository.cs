using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.AS400;
using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class ASSPRUMAPRepository
    {
        private string ConnectionString = string.Empty;
        public ASSPRUMAPRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        //private string ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
        public List<ASSPRUMAP> Get()
        {
            var lists = new List<ASSPRUMAP>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return lists; }
                var sqlQuery = "SELECT MAPID, ROLEID, MENUID, EMPNO, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPRUMAP ";
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ASSPRUMAP item = new ASSPRUMAP();
                        item.MAPID = Convert.ToInt32(reader["MAPID"]);
                        item.ROLEID = Convert.ToInt32(reader["ROLEID"]);
                        item.MENUID = Convert.ToInt32(reader["MENUID"]);
                        item.EMPNO = Convert.ToString(reader["EMPNO"]);
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

        public ASSPRUMAP GetById(int id)
        {
            var result = new ASSPRUMAP();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return result; }
                string sqlQuery = string.Format("SELECT MAPID, ROLEID, MENUID, EMPNO, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPRUMAP WHERE MAPID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.MAPID = Convert.ToInt32(reader["MAPID"]);
                        result.ROLEID = Convert.ToInt32(reader["ROLEID"]);
                        result.MENUID = Convert.ToInt32(reader["MENUID"]);
                        result.EMPNO = Convert.ToString(reader["EMPNO"]);
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
                string sqlQuery = string.Format("SELECT * FROM HASDLIB.ASSPRUMAP");
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    string sqlQuery1 = string.Format("SELECT MAX(MAPID) FROM HASDLIB.ASSPRUMAP");
                    var command1 = new OleDbCommand(sqlQuery1, connection);
                    var lastid = command1.ExecuteScalar();
                    return Convert.ToInt32(lastid);
                }
                else
                    return 0;
            }
        }

        public void Insert(ASSPRUMAP model)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("INSERT INTO HASDLIB.ASSPRUMAP(MAPID, ROLEID, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA, MENUID, EMPNO) VALUES ({0},{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},'{9}')",
                    model.MAPID, model.ROLEID, model.ASTATUS, model.DEF, model.CTOR, model.CTDA, model.MDOR, model.MDDA, model.MENUID, model.EMPNO);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(ASSPRUMAP model, int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPRUMAP SET ROLEID = {0}, ASTATUS = {1}, DEF = '{2}',MDOR = '{3}', MDDA = '{4}', MENUID = {6}, EMPNO = {7}   WHERE MAPID = {5}",
                    model.ROLEID, model.ASTATUS, model.DEF, model.MDOR, model.MDDA, id, model.MENUID, model.EMPNO);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPRUMAP WHERE MAPID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

    }
}


