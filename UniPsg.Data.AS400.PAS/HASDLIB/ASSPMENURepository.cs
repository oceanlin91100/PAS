using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.AS400;
using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class ASSPMENURepository
    {
        private string ConnectionString = string.Empty;
        public ASSPMENURepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        //private string ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
        public List<ASSPMENU> Get()
        {
            var lists = new List<ASSPMENU>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return lists; }
                var sqlQuery = "SELECT MENUID, MENUNAME, CONTR, ACTIOM, URL, ODER, PARID, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPMENU ";
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ASSPMENU item = new ASSPMENU();
                        item.MENUID = Convert.ToInt32(reader["MENUID"]);
                        item.MENUNAME = Convert.ToString(reader["MENUNAME"]);
                        item.CONTR = Convert.ToString(reader["CONTR"]);
                        item.ACTIOM = Convert.ToString(reader["ACTIOM"]);
                        item.URL = Convert.ToString(reader["URL"]);
                        item.ODER = Convert.ToInt32(reader["ODER"]);
                        item.PARID = Convert.ToInt32(reader["PARID"]);
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

        public ASSPMENU GetById(int id)
        {
            var result = new ASSPMENU();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return result; }
                string sqlQuery = string.Format("SELECT MENUID, MENUNAME, CONTR, ACTIOM, URL, ODER, PARID, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPMENU WHERE MENUID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.MENUID = Convert.ToInt32(reader["MENUID"]);
                        result.MENUNAME = Convert.ToString(reader["MENUNAME"]);
                        result.CONTR = Convert.ToString(reader["CONTR"]);
                        result.ACTIOM = Convert.ToString(reader["ACTIOM"]);
                        result.URL = Convert.ToString(reader["URL"]);
                        result.ODER = Convert.ToInt32(reader["ODER"]);
                        result.PARID = Convert.ToInt32(reader["PARID"]);
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
                string sqlQuery = string.Format("SELECT * FROM HASDLIB.ASSPMENU");
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    string sqlQuery1 = string.Format("SELECT MAX(MENUID) FROM HASDLIB.ASSPMENU");
                    var command1 = new OleDbCommand(sqlQuery1, connection);
                    var lastid = command1.ExecuteScalar();
                    return Convert.ToInt32(lastid);
                }
                else
                    return 0;
            }
        }

        public void Insert(ASSPMENU model)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("INSERT INTO HASDLIB.ASSPMENU(MENUID, MENUNAME, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA, CONTR, ACTIOM, URL, ODER, PARID) VALUES ({0},'{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},{12})",
                    model.MENUID, model.MENUNAME, model.ASTATUS, model.DEF, model.CTOR, model.CTDA, model.MDOR, model.MDDA, model.CONTR, model.ACTIOM, model.URL, model.ODER, model.PARID);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(ASSPMENU model, int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPMENU SET MENUNAME = '{0}', ASTATUS = {1}, DEF = '{2}',MDOR = '{3}', MDDA = '{4}', CONTR = '{6}', ACTIOM = '{7}', URL = '{8}', ODER = {9}, PARID = {10}  WHERE MENUID = {5}",
                    model.MENUNAME, model.ASTATUS, model.DEF, model.MDOR, model.MDDA, id, model.CONTR, model.ACTIOM, model.URL, model.ODER, model.PARID);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPMENU WHERE MENUID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

    }
}

