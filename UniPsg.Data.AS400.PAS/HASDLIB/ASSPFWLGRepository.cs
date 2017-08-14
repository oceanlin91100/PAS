using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.AS400;
using System.Data;
using System.Data.OleDb;


namespace UniPsg.Data.AS400.PAS
{
   public  class ASSPFWLGRepository
    {
        private string ConnectionString = string.Empty;
        public ASSPFWLGRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        //private string ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
        public List<ASSPFWLG> Get()
        {
            var lists = new List<ASSPFWLG>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return lists; }
                var sqlQuery = "SELECT FLID, PRECNT, PSTCNT, CTOR, CTDA FROM HASDLIB.ASSPFWLG ";
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ASSPFWLG item = new ASSPFWLG();
                        item.FLID = Convert.ToInt32(reader["FLID"]);
                        item.PRECNT = Convert.ToString(reader["PRECNT"]);
                        item.PSTCNT = Convert.ToString(reader["PSTCNT"]);                        
                        item.CTOR = Convert.ToString(reader["CTOR"]);
                        item.CTDA = Convert.ToString(reader["CTDA"]);                        
                        lists.Add(item);
                    }
                }
            }
            return lists;
        }

        public ASSPFWLG GetById(int id)
        {
            var result = new ASSPFWLG();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return result; }
                string sqlQuery = string.Format("SELECT FLID, PRECNT, PSTCNT, CTOR, CTDA FROM HASDLIB.ASSPFWLG WHERE FLID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.FLID = Convert.ToInt32(reader["FLID"]);
                        result.PRECNT = Convert.ToString(reader["PRECNT"]);
                        result.PSTCNT = Convert.ToString(reader["PSTCNT"]);
                        result.CTOR = Convert.ToString(reader["CTOR"]);
                        result.CTDA = Convert.ToString(reader["CTDA"]);
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
                string sqlQuery = string.Format("SELECT * FROM HASDLIB.ASSPFWLG");
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    string sqlQuery1 = string.Format("SELECT MAX(FLID) FROM HASDLIB.ASSPFWLG");
                    var command1 = new OleDbCommand(sqlQuery1, connection);
                    var lastid = command1.ExecuteScalar();
                    return Convert.ToInt32(lastid);
                }
                else
                    return 0;
            }
        }

        public void Insert(ASSPFWLG model)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("INSERT INTO HASDLIB.ASSPFWLG( FLID, PRECNT, PSTCNT, CTOR, CTDA) VALUES ({0},'{1}','{2}','{3}','{4}')",
                    model.FLID, model.PRECNT, model.PSTCNT, model.CTOR, model.CTDA);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(ASSPFWLG model, int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPFWLG SET PRECNT = '{0}', PSTCNT = '{1}' WHERE FLID = {3}",
                    model.PRECNT, model.PSTCNT, id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPFWLG WHERE FLID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

    }
}

