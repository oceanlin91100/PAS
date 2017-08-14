using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.AS400;
using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class ASSPITEMRepository
    {
        private string ConnectionString = string.Empty;
        public ASSPITEMRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        //private string ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
        public List<ASSPITEM> Get()
        {
            var lists = new List<ASSPITEM>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return lists; }
                var sqlQuery = "SELECT ITEMID, ITEMNAME, WEIGHT, ASID, KPICID, GROUPS, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPITEM ";
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ASSPITEM item = new ASSPITEM();
                        item.ITEMID = Convert.ToInt32(reader["ITEMID"]);
                        item.ITEMNAME = Convert.ToString(reader["ITEMNAME"]);
                        item.WEIGHT = Convert.ToInt32(reader["WEIGHT"]);
                        item.ASID = Convert.ToInt32(reader["ASID"]);
                        item.KPICID = Convert.ToInt32(reader["KPICID"]);
                        item.GROUPS = Convert.ToString(reader["GROUPS"]);
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

        public ASSPITEM GetById(int id)
        {
            var result = new ASSPITEM();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return result; }
                string sqlQuery = string.Format("SELECT ITEMID, ITEMNAME, WEIGHT, ASID, KPICID, GROUPS, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPITEM WHERE ITEMID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.ITEMID = Convert.ToInt32(reader["ITEMID"]);
                        result.ITEMNAME = Convert.ToString(reader["ITEMNAME"]);
                        result.WEIGHT = Convert.ToInt32(reader["WEIGHT"]);
                        result.ASID = Convert.ToInt32(reader["ASID"]);
                        result.KPICID = Convert.ToInt32(reader["KPICID"]);
                        result.GROUPS = Convert.ToString(reader["GROUPS"]);
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
                string sqlQuery = string.Format("SELECT * FROM HASDLIB.ASSPITEM");
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    string sqlQuery1 = string.Format("SELECT MAX(ITEMID) FROM HASDLIB.ASSPITEM");
                    var command1 = new OleDbCommand(sqlQuery1, connection);
                    var lastid = command1.ExecuteScalar();
                    return Convert.ToInt32(lastid);
                }
                else
                    return 0;
            }
        }

        public void Insert(ASSPITEM model)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("INSERT INTO HASDLIB.ASSPITEM(ITEMID, ITEMNAME, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA, WEIGHT, ASID, KPICID, GROUPS) " +
                    " VALUES ({0},'{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},'{11}')",
                    model.ITEMID, model.ITEMNAME, model.ASTATUS, model.DEF, model.CTOR, model.CTDA, model.MDOR, model.MDDA, model.WEIGHT, model.ASID, model.KPICID, model.GROUPS);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(ASSPITEM model, int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPITEM SET ITEMNAME = '{0}', ASTATUS = {1}, DEF = '{2}',MDOR = '{3}', MDDA = '{4}', " + 
                    " WEIGHT = {6}, ASID = {7}, KPICID = {8}, GROUPS = '{9}' WHERE ITEMID = {5}",
                    model.ITEMNAME, model.ASTATUS, model.DEF, model.MDOR, model.MDDA, id, model.WEIGHT, model.ASID, model.KPICID, model.GROUPS);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPITEM WHERE ITEMID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

    }
}

