using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.AS400;
using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class ASSPSCORERepository
    {
        private string ConnectionString = string.Empty;
        public ASSPSCORERepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        //private string ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
        public List<ASSPSCORE> Get()
        {
            var lists = new List<ASSPSCORE>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return lists; }
                var sqlQuery = "SELECT PSID, APRID, EMPNO, RVNO, KPISCORE, CORESCORE, MAGSCORE, BPSCORE, TOTSCORE, ARID, DEVCOMM, " +
                    " COMM, ORDER, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPSCORE ";
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ASSPSCORE item = new ASSPSCORE();
                        item.PSID = Convert.ToString(reader["PSID"]);
                        item.APRID = Convert.ToInt32(reader["APRID"]);
                        item.EMPNO = Convert.ToString(reader["EMPNO"]);
                        item.RVNO = Convert.ToString(reader["RVNO"]);
                        item.KPISCORE = Convert.ToDecimal(reader["KPISCORE"]);
                        item.CORESCORE = Convert.ToDecimal(reader["CORESCORE"]);
                        item.MAGSCORE = Convert.ToDecimal(reader["MAGSCORE"]);
                        item.BPSCORE = Convert.ToDecimal(reader["BPSCORE"]);
                        item.TOTSCORE = Convert.ToDecimal(reader["TOTSCORE"]);
                        item.ARID = Convert.ToInt32(reader["ARID"]);
                        item.DEVCOMM = Convert.ToString(reader["DEVCOMM"]);
                        item.COMM = Convert.ToString(reader["COMM"]);
                        item.ORDER = Convert.ToInt32(reader["ORDER"]);
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

        public ASSPSCORE GetById(string id)
        {
            var result = new ASSPSCORE();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return result; }
                string sqlQuery = string.Format("SELECT PSID, APRID, EMPNO, RVNO, KPISCORE, CORESCORE, MAGSCORE, BPSCORE, TOTSCORE, ARID, DEVCOMM, " +
                    " COMM, ORDER, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPSCORE WHERE PSID = '{0}'", id);
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.PSID = Convert.ToString(reader["PSID"]);
                        result.APRID = Convert.ToInt32(reader["APRID"]);
                        result.EMPNO = Convert.ToString(reader["EMPNO"]);
                        result.RVNO = Convert.ToString(reader["RVNO"]);
                        result.KPISCORE = Convert.ToDecimal(reader["KPISCORE"]);
                        result.CORESCORE = Convert.ToDecimal(reader["CORESCORE"]);
                        result.MAGSCORE = Convert.ToDecimal(reader["MAGSCORE"]);
                        result.BPSCORE = Convert.ToDecimal(reader["BPSCORE"]);
                        result.TOTSCORE = Convert.ToDecimal(reader["TOTSCORE"]);
                        result.ARID = Convert.ToInt32(reader["ARID"]);
                        result.DEVCOMM = Convert.ToString(reader["DEVCOMM"]);                        
                        result.COMM = Convert.ToString(reader["COMM"]);
                        result.ORDER = Convert.ToInt32(reader["ORDER"]);
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

        public void Insert(ASSPSCORE model)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("INSERT INTO HASDLIB.ASSPSCORE(PSID, APRID, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA, EMPNO, RVNO, KPISCORE, CORESCORE, MAGSCORE, " +
                    " BPSCORE, TOTSCORE, ARID, DEVCOMM, COMM, ORDER) VALUES ('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},{12},{13},{14},{15},'{16}','{17}',{18})",
                    model.PSID, model.APRID, model.ASTATUS, model.DEF, model.CTOR, model.CTDA, model.MDOR, model.MDDA, model.EMPNO, model.RVNO, model.KPISCORE, model.CORESCORE, model.MAGSCORE, model.BPSCORE,
                    model.TOTSCORE, model.ARID, model.DEVCOMM, model.COMM, model.ORDER);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(ASSPSCORE model, string id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPSCORE SET APRID = {0}, ASTATUS = {1}, DEF = '{2}',MDOR = '{3}', MDDA = '{4}'" +
                    ", EMPNO = '{6}', RVNO = '{7}', KPISCORE = {8}, CORESCORE = {9}, MAGSCORE = {10}, BPSCORE = {11}, TOTSCORE = {12}, ARID = {13}, DEVCOMM = '{14}', COMM = '{15}' " +
                    ", ORDER = {16} WHERE PSID = '{5}'",
                    model.APRID, model.ASTATUS, model.DEF, model.MDOR, model.MDDA, id, model.EMPNO, model.RVNO, model.KPISCORE, model.CORESCORE, model.MAGSCORE, model.BPSCORE, model.TOTSCORE,
                    model.ARID, model.DEVCOMM, model.COMM, model.ORDER);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(string id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPSCORE WHERE PSID = '{0}'", id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

    }
}


