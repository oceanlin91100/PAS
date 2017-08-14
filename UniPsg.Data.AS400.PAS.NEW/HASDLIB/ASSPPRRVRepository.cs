using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.AS400;
using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class ASSPPRRVRepository
    {
        private string ConnectionString = string.Empty;
        public ASSPPRRVRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        //private string ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
        public List<ASSPPRRV> Get()
        {
            var lists = new List<ASSPPRRV>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return lists; }
                var sqlQuery = "SELECT PVID, APRID, EMPNO, RVNO, ASID, ITEMID, ITEMNAME, KPICID, WEIGHT, RATE, SCORE, SCORE1, COMM, COMM1, " +
                    " COMM2, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPPRRV ";
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ASSPPRRV item = new ASSPPRRV();
                        item.PVID = Convert.ToString(reader["PVID"]);
                        item.APRID = Convert.ToInt32(reader["APRID"]);
                        item.EMPNO = Convert.ToString(reader["EMPNO"]);
                        item.RVNO = Convert.ToString(reader["RVNO"]);
                        item.ASID = Convert.ToInt32(reader["ASID"]);
                        item.ITEMID = Convert.ToInt32(reader["ITEMID"]);
                        item.ITEMNAME = Convert.ToString(reader["ITEMNAME"]);
                        item.KPICID = Convert.ToInt32(reader["KPICID"]);
                        item.WEIGHT = Convert.ToDecimal(reader["WEIGHT"]);
                        item.RATE = Convert.ToDecimal(reader["RATE"]);
                        item.SCORE = Convert.ToDecimal(reader["SCORE"]);
                        item.SCORE1 = Convert.ToDecimal(reader["SCORE1"]);
                        item.COMM = Convert.ToString(reader["COMM"]);
                        item.COMM1 = Convert.ToString(reader["COMM1"]);
                        item.COMM2 = Convert.ToString(reader["COMM2"]);
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

        public ASSPPRRV GetById(string id)
        {
            var result = new ASSPPRRV();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return result; }
                string sqlQuery = string.Format("SELECT PVID, APRID, EMPNO, RVNO, ASID, ITEMID, ITEMNAME, KPICID, WEIGHT, RATE, SCORE, SCORE1, COMM, COMM1, " +
                    " COMM2, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPPRRV WHERE PVID = '{0}'", id);
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.PVID = Convert.ToString(reader["PVID"]);
                        result.APRID = Convert.ToInt32(reader["APRID"]);
                        result.EMPNO = Convert.ToString(reader["EMPNO"]);
                        result.RVNO = Convert.ToString(reader["RVNO"]);
                        result.ASID = Convert.ToInt32(reader["ASID"]);
                        result.ITEMID = Convert.ToInt32(reader["ITEMID"]);
                        result.ITEMNAME = Convert.ToString(reader["ITEMNAME"]);
                        result.KPICID = Convert.ToInt32(reader["KPICID"]);
                        result.WEIGHT = Convert.ToDecimal(reader["WEIGHT"]);
                        result.RATE = Convert.ToDecimal(reader["RATE"]);
                        result.SCORE = Convert.ToDecimal(reader["SCORE"]);
                        result.SCORE1 = Convert.ToDecimal(reader["SCORE1"]);
                        result.COMM = Convert.ToString(reader["COMM"]);
                        result.COMM1 = Convert.ToString(reader["COMM1"]);
                        result.COMM2 = Convert.ToString(reader["COMM2"]);
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

        public void Insert(ASSPPRRV model)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("INSERT INTO HASDLIB.ASSPPRRV(PVID, APRID, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA, EMPNO, RVNO, ASID, ITEMID, ITEMNAME, KPICID, " +
                    " WEIGHT, RATE, SCORE, SCORE1, COMM, COMM1, COMM2) VALUES ('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}',{13},{14},{15},{16},{17},'{18}','{19}','{20}')",
                    model.PVID, model.APRID, model.ASTATUS, model.DEF, model.CTOR, model.CTDA, model.MDOR, model.MDDA, model.EMPNO, model.RVNO, model.ASID, model.ITEMID, model.ITEMNAME, model.KPICID,
                    model.WEIGHT, model.RATE, model.SCORE, model.SCORE1, model.COMM, model.COMM1, model.COMM2);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(ASSPPRRV model, string id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPPRRV SET APRID = {0}, ASTATUS = {1}, DEF = '{2}',MDOR = '{3}', MDDA = '{4}'" +
                    ", EMPNO = '{6}', RVNO = '{7}', ASID = {8}, ITEMID = {9}, ITEMNAME = '{10}', KPICID = {11}, WEIGHT = {12}, RATE = {13}, SCORE = {14}, SCORE1 = {15} " +
                    ", COMM = '{16}', COMM1 = '{17}', COMM2 = '{18}' WHERE PVID = '{5}'",
                    model.APRID, model.ASTATUS, model.DEF, model.MDOR, model.MDDA, id, model.EMPNO, model.RVNO, model.ASID, model.ITEMID, model.ITEMNAME, model.KPICID, model.WEIGHT,
                    model.RATE, model.SCORE, model.SCORE1, model.COMM, model.COMM1, model.COMM2);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(string id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPPRRV WHERE PVID = '{0}'", id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

    }
}

