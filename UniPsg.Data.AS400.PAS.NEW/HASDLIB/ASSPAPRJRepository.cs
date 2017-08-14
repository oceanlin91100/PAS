using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.AS400;
using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class ASSPAPRJRepository
    {
        private string ConnectionString = string.Empty;
        public ASSPAPRJRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        //private string ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
        public List<ASSPAPRJ> Get()
        {
            var lists = new List<ASSPAPRJ>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return lists; }
                var sqlQuery = "SELECT APRID, APRNAME, VSDA, VEDA, FRDA, DLDA, AYEAR, TRYDA, GROUPS, FROMS, ACID, ASTATUS, DEF, CTOR, CTDA, " +
                    " MDOR, MDDA FROM HASDLIB.ASSPAPRJ ";
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ASSPAPRJ item = new ASSPAPRJ();
                        item.APRID = Convert.ToInt32(reader["APRID"]);
                        item.APRNAME = Convert.ToString(reader["APRNAME"]);
                        item.VSDA = Convert.ToInt32(reader["VSDA"]);
                        item.VEDA = Convert.ToInt32(reader["VEDA"]);
                        item.FRDA = Convert.ToInt32(reader["FRDA"]);
                        item.DLDA = Convert.ToInt32(reader["DLDA"]);
                        item.AYEAR = Convert.ToInt32(reader["AYEAR"]);
                        item.TRYDA = Convert.ToInt32(reader["TRYDA"]);
                        item.GROUPS = Convert.ToString(reader["GROUPS"]);
                        item.FROMS = Convert.ToString(reader["FROMS"]);
                        item.ACID = Convert.ToInt32(reader["ACID"]);
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

        public ASSPAPRJ GetById(int id)
        {
            var item = new ASSPAPRJ();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return item; }
                string sqlQuery = string.Format("SELECT APRID, APRNAME, VSDA, VEDA, FRDA, DLDA, AYEAR, TRYDA, GROUPS, FROMS, ACID, ASTATUS, DEF, CTOR, CTDA, " +
                    " MDOR, MDDA FROM HASDLIB.ASSPAPRJ WHERE APRID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        item.APRID = Convert.ToInt32(reader["APRID"]);
                        item.APRNAME = Convert.ToString(reader["APRNAME"]);
                        item.VSDA = Convert.ToInt32(reader["VSDA"]);
                        item.VEDA = Convert.ToInt32(reader["VEDA"]);
                        item.FRDA = Convert.ToInt32(reader["FRDA"]);
                        item.DLDA = Convert.ToInt32(reader["DLDA"]);
                        item.AYEAR = Convert.ToInt32(reader["AYEAR"]);
                        item.TRYDA = Convert.ToInt32(reader["TRYDA"]);
                        item.GROUPS = Convert.ToString(reader["GROUPS"]);
                        item.FROMS = Convert.ToString(reader["FROMS"]);
                        item.ACID = Convert.ToInt32(reader["ACID"]);
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
                string sqlQuery = string.Format("SELECT * FROM HASDLIB.ASSPAPRJ");
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    string sqlQuery1 = string.Format("SELECT MAX(APRID) FROM HASDLIB.ASSPAPRJ");
                    var command1 = new OleDbCommand(sqlQuery1, connection);
                    var lastid = command1.ExecuteScalar();
                    return Convert.ToInt32(lastid);
                }
                else
                    return 0;
            }
        }

        public void Insert(ASSPAPRJ model)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("INSERT INTO HASDLIB.ASSPAPRJ(APRID, APRNAME, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA, VSDA, VEDA, FRDA, DLDA, AYEAR, TRYDA, GROUPS, FROMS, ACID) VALUES ({0},'{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},{13},'{14}','{15}',{16})",
                    model.APRID, model.APRNAME, model.ASTATUS, model.DEF, model.CTOR, model.CTDA, model.MDOR, model.MDDA, model.VSDA, model.VEDA, model.FRDA, model.DLDA, model.AYEAR, model.TRYDA, model.GROUPS, model.FROMS, model.ACID);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(ASSPAPRJ model, int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPAPRJ SET APRNAME = '{0}', ASTATUS = {1}, DEF = '{2}', MDOR = '{3}', MDDA = '{4}', VSDA = {6}, VEDA = {7}, FRDA = {8}, DLDA = {9}, AYEAR = {10}, TRYDA = {11}, GROUPS = '{12}', FROMS = '{13}', ACID = {14}  WHERE APRID = {5}",
                    model.APRNAME, model.ASTATUS, model.DEF, model.MDOR, model.MDDA, id, model.VSDA, model.VEDA, model.FRDA, model.DLDA, model.AYEAR, model.TRYDA, model.GROUPS, model.FROMS, model.ACID);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPAPRJ WHERE APRID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

    }
}

