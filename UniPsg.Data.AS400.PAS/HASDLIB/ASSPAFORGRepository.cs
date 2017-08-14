using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.AS400;
using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class ASSPAFORGRepository
    {
        private string ConnectionString = string.Empty;
        public ASSPAFORGRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        //private string ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
        public List<ASSPAFORG> Get()
        {
            var lists = new List<ASSPAFORG>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return lists; }
                var sqlQuery = "SELECT APRID, EMPNO, EMPNAME, EMPACTNO, BRCD, BRNAME, DEPCD, DEPNAME, TMCD, TMNAME, MAGNO, MAGNAME, " +
                    " MAGACTNO, FLOWPATH, NAMEPATH, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPAFORG ";
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ASSPAFORG item = new ASSPAFORG();
                        item.APRID = Convert.ToInt32(reader["APRID"]);
                        item.EMPNO = Convert.ToString(reader["EMPNO"]);
                        item.EMPNAME = Convert.ToString(reader["EMPNAME"]);
                        item.EMPACTNO = Convert.ToString(reader["EMPACTNO"]);
                        item.BRCD = Convert.ToString(reader["BRCD"]);
                        item.BRNAME = Convert.ToString(reader["BRNAME"]);
                        item.DEPCD = Convert.ToString(reader["DEPCD"]);
                        item.DEPNAME = Convert.ToString(reader["DEPNAME"]);
                        item.TMCD = Convert.ToString(reader["TMCD"]);
                        item.TMNAME = Convert.ToString(reader["TMNAME"]);
                        item.MAGNO = Convert.ToString(reader["MAGNO"]);
                        item.MAGNAME = Convert.ToString(reader["MAGNAME"]);
                        item.MAGACTNO = Convert.ToString(reader["MAGACTNO"]);
                        item.FLOWPATH = Convert.ToString(reader["FLOWPATH"]);
                        item.NAMEPATH = Convert.ToString(reader["NAMEPATH"]);
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

        public ASSPAFORG GetById(int id, string empNO)
        {
            var result = new ASSPAFORG();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return result; }
                string sqlQuery = string.Format("SELECT APRID, EMPNO, EMPNAME, EMPACTNO, BRCD, BRNAME, DEPCD, DEPNAME, TMCD, TMNAME, MAGNO, MAGNAME, " +
                    " MAGACTNO, FLOWPATH, NAMEPATH, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPAFORG WHERE APRID = {0} AND EMPNO = '{1}'", id, empNO);
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.APRID = Convert.ToInt32(reader["APRID"]);
                        result.EMPNO = Convert.ToString(reader["EMPNO"]);
                        result.EMPNAME = Convert.ToString(reader["EMPNAME"]);
                        result.EMPACTNO = Convert.ToString(reader["EMPACTNO"]);
                        result.BRCD = Convert.ToString(reader["BRCD"]);
                        result.BRNAME = Convert.ToString(reader["BRNAME"]);
                        result.DEPCD = Convert.ToString(reader["DEPCD"]);
                        result.DEPNAME = Convert.ToString(reader["DEPNAME"]);
                        result.TMCD = Convert.ToString(reader["TMCD"]);
                        result.TMNAME = Convert.ToString(reader["TMNAME"]);
                        result.MAGNO = Convert.ToString(reader["MAGNO"]);
                        result.MAGNAME = Convert.ToString(reader["MAGNAME"]);
                        result.MAGACTNO = Convert.ToString(reader["MAGACTNO"]);
                        result.FLOWPATH = Convert.ToString(reader["FLOWPATH"]);
                        result.NAMEPATH = Convert.ToString(reader["NAMEPATH"]);
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

        public void Insert(ASSPAFORG model)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("INSERT INTO HASDLIB.ASSPAFORG(APRID, EMPNO, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA," + 
                    " EMPNAME, EMPACTNO, BRCD, BRNAME, DEPCD, DEPNAME, TMCD, TMNAME, MAGNO, MAGNAME, MAGACTNO, FLOWPATH, NAMEPATH) " + 
                    " VALUES ({0},'{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}')",
                    model.APRID, model.EMPNO, model.ASTATUS, model.DEF, model.CTOR, model.CTDA, model.MDOR, model.MDDA, model.EMPNAME, model.EMPACTNO, model.BRCD, model.BRNAME, model.DEPCD,
                    model.DEPNAME, model.TMCD, model.TMNAME, model.MAGNO, model.MAGNAME, model.MAGACTNO, model.FLOWPATH, model.NAMEPATH);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(ASSPAFORG model, int id, string empNo)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPAFORG SET EMPNAME = '{0}', ASTATUS = {1}, DEF = '{2}', MDOR = '{3}', MDDA = '{4}', " +
                    " EMPACTNO = '{7}', BRCD = '{8}', BRNAME = '{9}', DEPCD = '{10}', DEPNAME = '{11}', TMCD = '{12}', TMNAME = '{13}'," +
                    " MAGNO = '{14}', MAGNAME = '{15}', MAGACTNO = '{16}', FLOWPATH = '{17}', NAMEPATH = '{18}' WHERE APRID = {5} AND EMPNO = '{6}'",
                    model.EMPNAME, model.ASTATUS, model.DEF, model.MDOR, model.MDDA, id, empNo, model.EMPACTNO, model.BRCD, model.BRNAME, model.DEPCD,
                    model.DEPNAME, model.TMCD, model.TMNAME, model.MAGNO, model.MAGNAME, model.MAGACTNO, model.FLOWPATH, model.NAMEPATH);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id, string empNo)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPAFORG WHERE APRID = {0} AND EMPNO = '{1}'", id, empNo);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPAFORG WHERE APRID = {0} ", id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

    }
}
