using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.AS400;
using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class ASSPAPOSNRepository
    {
        private string ConnectionString = string.Empty;
        public ASSPAPOSNRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        //private string ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
        public List<ASSPAPOSN> Get()
        {
            var lists = new List<ASSPAPOSN>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return lists; }
                var sqlQuery = "SELECT APRID, AGID, AFID, EMPNO, RVNO, EMPNAME, STADA, TRYDA, BRCD, BRNAME, DEPCD, DEPNAME, APJCCD, " +
                    " APJCNAME, ASJPCD, ASJPNAME, APED, EMPTP, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPAPOSN ";
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ASSPAPOSN item = new ASSPAPOSN();
                        item.APRID = Convert.ToInt32(reader["APRID"]);
                        item.AGID = Convert.ToInt32(reader["AGID"]);
                        item.AFID = Convert.ToInt32(reader["AFID"]);
                        item.EMPNO = Convert.ToString(reader["EMPNO"]);
                        item.RVNO = Convert.ToString(reader["RVNO"]);
                        item.EMPNAME = Convert.ToString(reader["EMPNAME"]);
                        item.STADA = Convert.ToInt32(reader["STADA"]);
                        item.TRYDA = Convert.ToInt32(reader["TRYDA"]);
                        item.BRCD = Convert.ToString(reader["BRCD"]);
                        item.BRNAME = Convert.ToString(reader["BRNAME"]);
                        item.DEPCD = Convert.ToString(reader["DEPCD"]);
                        item.DEPNAME = Convert.ToString(reader["DEPNAME"]);
                        item.APJCCD = Convert.ToString(reader["APJCCD"]);
                        item.APJCNAME = Convert.ToString(reader["APJCNAME"]);
                        item.ASJPCD = Convert.ToString(reader["ASJPCD"]);
                        item.ASJPNAME = Convert.ToString(reader["ASJPNAME"]);
                        item.APED = Convert.ToString(reader["APED"]);
                        item.EMPTP = Convert.ToString(reader["EMPTP"]);
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

        public ASSPAPOSN GetById(int id, string empNO)
        {
            var result = new ASSPAPOSN();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return result; }
                string sqlQuery = string.Format("SELECT APRID, AGID, AFID, EMPNO, RVNO, EMPNAME, STADA, TRYDA, BRCD, BRNAME, DEPCD, DEPNAME, APJCCD, " +
                    " APJCNAME, ASJPCD, ASJPNAME, APED, EMPTP, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPAPOSN WHERE APRID = {0} AND EMPNO = '{1}'", id, empNO);
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.APRID = Convert.ToInt32(reader["APRID"]);
                        result.AGID = Convert.ToInt32(reader["AGID"]);
                        result.AFID = Convert.ToInt32(reader["AFID"]);
                        result.EMPNO = Convert.ToString(reader["EMPNO"]);
                        result.RVNO = Convert.ToString(reader["RVNO"]);
                        result.EMPNAME = Convert.ToString(reader["EMPNAME"]);
                        result.STADA = Convert.ToInt32(reader["STADA"]);
                        result.TRYDA = Convert.ToInt32(reader["TRYDA"]);
                        result.BRCD = Convert.ToString(reader["BRCD"]);
                        result.BRNAME = Convert.ToString(reader["BRNAME"]);
                        result.DEPCD = Convert.ToString(reader["DEPCD"]);
                        result.DEPNAME = Convert.ToString(reader["DEPNAME"]);
                        result.APJCCD = Convert.ToString(reader["APJCCD"]);
                        result.APJCNAME = Convert.ToString(reader["APJCNAME"]);
                        result.ASJPCD = Convert.ToString(reader["ASJPCD"]);
                        result.ASJPNAME = Convert.ToString(reader["ASJPNAME"]);
                        result.APED = Convert.ToString(reader["APED"]);
                        result.EMPTP = Convert.ToString(reader["EMPTP"]);
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

        public void Insert(ASSPAPOSN model)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("INSERT INTO HASDLIB.ASSPAPOSN( APRID, EMPNO, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA, AGID, AFID, RVNO, EMPNAME, STADA, TRYDA, BRCD, " +
                    " BRNAME, DEPCD, DEPNAME, APJCCD, APJCNAME, ASJPCD, ASJPNAME, APED, EMPTP) " +
                    " VALUES ({0},'{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},'{10}','{11}',{12},{13},'{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}')",
                    model.APRID, model.EMPNO, model.ASTATUS, model.DEF, model.CTOR, model.CTDA, model.MDOR, model.MDDA, model.AGID, model.AFID, model.RVNO, model.EMPNAME, model.STADA,
                    model.TRYDA, model.BRCD, model.BRNAME, model.DEPCD, model.DEPNAME, model.APJCCD, model.APJCNAME, model.ASJPCD, model.ASJPNAME, model.APED, model.EMPTP);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(ASSPAPOSN model, int id, string empNo)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPAPOSN SET EMPNAME = '{0}', ASTATUS = {1}, DEF = '{2}', MDOR = '{3}', MDDA = '{4}', " +
                    " AGID = {7}, AFID = {8}, RVNO = '{9}', STADA = {10}, TRYDA = {11}, BRCD = '{12}', BRNAME = '{13}'," +
                    " DEPCD = '{14}', DEPNAME = '{15}', APJCCD = '{16}', APJCNAME = '{17}', ASJPCD = '{18}', ASJPNAME = '{19}', APED = '{20}', EMPTP = '{21}' WHERE APRID = {5} AND EMPNO = '{6}'",
                    model.EMPNAME, model.ASTATUS, model.DEF, model.MDOR, model.MDDA, id, empNo, model.AGID, model.AFID, model.RVNO, model.STADA,
                    model.TRYDA, model.BRCD, model.BRNAME, model.DEPCD, model.DEPNAME, model.APJCCD, model.APJCNAME, model.ASJPCD, model.ASJPNAME, model.APED, model.EMPTP);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(int id, string editer, string edittime)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPAPOSN SET ASTATUS = 2, MDOR = '{1}', MDDA = '{2}' WHERE APRID = {0} ", id, editer, edittime);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id, string empNo)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPAPOSN WHERE APRID = {0} AND EMPNO = '{1}'", id, empNo);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPAPOSN WHERE APRID = {0} ", id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public DataTable GetPersonFlow()
        {
            DataTable dt = new DataTable();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return dt; }
                var sqlQuery = "SELECT A.APRID, A.AGID, A.AFID, A.EMPNO, A.RVNO, A.EMPNAME, A.STADA, A.TRYDA, A.BRCD, A.BRNAME, A.DEPCD, " +
                            "A.DEPNAME, A.APJCCD, A.APJCNAME, A.ASJPCD, A.ASJPNAME, A.APED, A.EMPTP, A.ASTATUS, A.DEF, A.CTOR, " +
                            " A.CTDA, A.MDOR, A.MDDA, A.APRNAME, A.AFNAME, A.AGNAME, A.FLOWPATH, A.NAMEPATH, " +
                            " B.EMPNAME AS ReName " +
                            " FROM (SELECT HASDLIB.ASSPAPOSN.APRID, HASDLIB.ASSPAPOSN.AGID, HASDLIB.ASSPAPOSN.AFID, " +
                                                        "HASDLIB.ASSPAPOSN.EMPNO, HASDLIB.ASSPAPOSN.RVNO, HASDLIB.ASSPAPOSN.EMPNAME, " +
                                                        "HASDLIB.ASSPAPOSN.STADA, HASDLIB.ASSPAPOSN.TRYDA, HASDLIB.ASSPAPOSN.BRCD, " +
                                                        "HASDLIB.ASSPAPOSN.BRNAME, HASDLIB.ASSPAPOSN.DEPCD, HASDLIB.ASSPAPOSN.DEPNAME, " +
                                                        "HASDLIB.ASSPAPOSN.APJCCD, HASDLIB.ASSPAPOSN.APJCNAME, HASDLIB.ASSPAPOSN.ASJPCD, " +
                                                       " HASDLIB.ASSPAPOSN.ASJPNAME, HASDLIB.ASSPAPOSN.APED, HASDLIB.ASSPAPOSN.EMPTP, " +
                                                        "HASDLIB.ASSPAPOSN.ASTATUS, HASDLIB.ASSPAPOSN.DEF, HASDLIB.ASSPAPOSN.CTOR, " +
                                                       " HASDLIB.ASSPAPOSN.CTDA, HASDLIB.ASSPAPOSN.MDOR, HASDLIB.ASSPAPOSN.MDDA, " +
                                                        "HASDLIB.ASSPAPRJ.APRNAME, HASDLIB.ASSPAFORM.AFNAME, HASDLIB.ASSPAGROUP.AGNAME, " +
                                                        "HASDLIB.ASSPAFORG.FLOWPATH, HASDLIB.ASSPAFORG.NAMEPATH " +
                           " FROM HASDLIB.ASSPAPOSN, HASDLIB.ASSPAPRJ, HASDLIB.ASSPAFORM, HASDLIB.ASSPAGROUP, " +
                                                        "HASDLIB.ASSPAFORG " +
                           " WHERE HASDLIB.ASSPAPOSN.APRID = HASDLIB.ASSPAPRJ.APRID AND " +
                                                       " HASDLIB.ASSPAPOSN.AFID = HASDLIB.ASSPAFORM.AFID AND " +
                                                        "HASDLIB.ASSPAPOSN.AGID = HASDLIB.ASSPAGROUP.AGID AND " +
                                                       " HASDLIB.ASSPAPOSN.APRID = HASDLIB.ASSPAFORG.APRID AND " +
                                                        "HASDLIB.ASSPAPOSN.EMPNO = HASDLIB.ASSPAFORG.EMPNO) A, HASDLIB.ASSPAFORG B " +
                            "WHERE A.RVNO = B.EMPNO AND A.APRID = B.APRID " +
                            " UNION " +
                            "SELECT APRID, AGID, AFID, EMPNO, RVNO, EMPNAME, STADA, TRYDA, BRCD, BRNAME, DEPCD, DEPNAME, APJCCD, " +
                            "APJCNAME, ASJPCD, ASJPNAME, APED, EMPTP, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA, APRNAME, AFNAME, " +
                            "AGNAME, FLOWPATH, NAMEPATH, '' AS ReName " +
                            "FROM (SELECT ASSPAPOSN_1.APRID, ASSPAPOSN_1.AGID, ASSPAPOSN_1.AFID, ASSPAPOSN_1.EMPNO," +
                                                        "ASSPAPOSN_1.RVNO, ASSPAPOSN_1.EMPNAME, ASSPAPOSN_1.STADA, ASSPAPOSN_1.TRYDA," +
                                                        "ASSPAPOSN_1.BRCD, ASSPAPOSN_1.BRNAME, ASSPAPOSN_1.DEPCD, ASSPAPOSN_1.DEPNAME," +
                                                        "ASSPAPOSN_1.APJCCD, ASSPAPOSN_1.APJCNAME, ASSPAPOSN_1.ASJPCD," +
                                                        "ASSPAPOSN_1.ASJPNAME, ASSPAPOSN_1.APED, ASSPAPOSN_1.EMPTP, ASSPAPOSN_1.ASTATUS," +
                                                        "ASSPAPOSN_1.DEF, ASSPAPOSN_1.CTOR, ASSPAPOSN_1.CTDA, ASSPAPOSN_1.MDOR, " +
                                                        "ASSPAPOSN_1.MDDA, ASSPAPRJ_1.APRNAME, ASSPAFORM_1.AFNAME, ASSPAGROUP_1.AGNAME," +
                                                        "ASSPAFORG_1.FLOWPATH, ASSPAFORG_1.NAMEPATH " +
                            "FROM HASDLIB.ASSPAPOSN ASSPAPOSN_1, HASDLIB.ASSPAPRJ ASSPAPRJ_1," +
                                                        "HASDLIB.ASSPAFORM ASSPAFORM_1, HASDLIB.ASSPAGROUP ASSPAGROUP_1," +
                                                        "HASDLIB.ASSPAFORG ASSPAFORG_1 " +
                            "WHERE ASSPAPOSN_1.APRID = ASSPAPRJ_1.APRID AND ASSPAPOSN_1.AFID = ASSPAFORM_1.AFID AND " +
                                                        "ASSPAPOSN_1.AGID = ASSPAGROUP_1.AGID AND ASSPAPOSN_1.APRID = ASSPAFORG_1.APRID AND " +
                                                        " ASSPAPOSN_1.EMPNO = ASSPAFORG_1.EMPNO) A_1 WHERE(RVNO = '00000') ";
                var command = new OleDbCommand(sqlQuery, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetPersonFlow(int projectId)
        {
            DataTable dt = new DataTable();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return dt; }
                var sqlQuery = "SELECT A.APRID, A.AGID, A.AFID, A.EMPNO, A.RVNO, A.EMPNAME, A.STADA, A.TRYDA, A.BRCD, A.BRNAME, A.DEPCD, " +
                             "A.DEPNAME, A.APJCCD, A.APJCNAME, A.ASJPCD, A.ASJPNAME, A.APED, A.EMPTP, A.ASTATUS, A.DEF, A.CTOR, " +
                             " A.CTDA, A.MDOR, A.MDDA, A.APRNAME, A.AFNAME, A.AGNAME, A.FLOWPATH, A.NAMEPATH, " +
                             " B.EMPNAME AS ReName " +
                             " FROM (SELECT HASDLIB.ASSPAPOSN.APRID, HASDLIB.ASSPAPOSN.AGID, HASDLIB.ASSPAPOSN.AFID, " +
                                                         "HASDLIB.ASSPAPOSN.EMPNO, HASDLIB.ASSPAPOSN.RVNO, HASDLIB.ASSPAPOSN.EMPNAME, " +
                                                         "HASDLIB.ASSPAPOSN.STADA, HASDLIB.ASSPAPOSN.TRYDA, HASDLIB.ASSPAPOSN.BRCD, " +
                                                         "HASDLIB.ASSPAPOSN.BRNAME, HASDLIB.ASSPAPOSN.DEPCD, HASDLIB.ASSPAPOSN.DEPNAME, " +
                                                         "HASDLIB.ASSPAPOSN.APJCCD, HASDLIB.ASSPAPOSN.APJCNAME, HASDLIB.ASSPAPOSN.ASJPCD, " +
                                                        " HASDLIB.ASSPAPOSN.ASJPNAME, HASDLIB.ASSPAPOSN.APED, HASDLIB.ASSPAPOSN.EMPTP, " +
                                                         "HASDLIB.ASSPAPOSN.ASTATUS, HASDLIB.ASSPAPOSN.DEF, HASDLIB.ASSPAPOSN.CTOR, " +
                                                        " HASDLIB.ASSPAPOSN.CTDA, HASDLIB.ASSPAPOSN.MDOR, HASDLIB.ASSPAPOSN.MDDA, " +
                                                         "HASDLIB.ASSPAPRJ.APRNAME, HASDLIB.ASSPAFORM.AFNAME, HASDLIB.ASSPAGROUP.AGNAME, " +
                                                         "HASDLIB.ASSPAFORG.FLOWPATH, HASDLIB.ASSPAFORG.NAMEPATH " +
                            " FROM HASDLIB.ASSPAPOSN, HASDLIB.ASSPAPRJ, HASDLIB.ASSPAFORM, HASDLIB.ASSPAGROUP, " +
                                                         "HASDLIB.ASSPAFORG " +
                            " WHERE HASDLIB.ASSPAPOSN.APRID = HASDLIB.ASSPAPRJ.APRID AND " +
                                                        " HASDLIB.ASSPAPOSN.AFID = HASDLIB.ASSPAFORM.AFID AND " +
                                                         "HASDLIB.ASSPAPOSN.AGID = HASDLIB.ASSPAGROUP.AGID AND " +
                                                        " HASDLIB.ASSPAPOSN.APRID = HASDLIB.ASSPAFORG.APRID AND " +
                                                         "HASDLIB.ASSPAPOSN.EMPNO = HASDLIB.ASSPAFORG.EMPNO) A, HASDLIB.ASSPAFORG B " +
                            "WHERE A.RVNO = B.EMPNO AND A.APRID = B.APRID AND A.APRID =" + projectId +
                           " UNION " +
                            "SELECT APRID, AGID, AFID, EMPNO, RVNO, EMPNAME, STADA, TRYDA, BRCD, BRNAME, DEPCD, DEPNAME, APJCCD, " +
                            "APJCNAME, ASJPCD, ASJPNAME, APED, EMPTP, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA, APRNAME, AFNAME, " +
                            "AGNAME, FLOWPATH, NAMEPATH, '' AS ReName " +
                            "FROM (SELECT ASSPAPOSN_1.APRID, ASSPAPOSN_1.AGID, ASSPAPOSN_1.AFID, ASSPAPOSN_1.EMPNO," +
                                                        "ASSPAPOSN_1.RVNO, ASSPAPOSN_1.EMPNAME, ASSPAPOSN_1.STADA, ASSPAPOSN_1.TRYDA," +
                                                        "ASSPAPOSN_1.BRCD, ASSPAPOSN_1.BRNAME, ASSPAPOSN_1.DEPCD, ASSPAPOSN_1.DEPNAME," +
                                                        "ASSPAPOSN_1.APJCCD, ASSPAPOSN_1.APJCNAME, ASSPAPOSN_1.ASJPCD," +
                                                        "ASSPAPOSN_1.ASJPNAME, ASSPAPOSN_1.APED, ASSPAPOSN_1.EMPTP, ASSPAPOSN_1.ASTATUS," +
                                                        "ASSPAPOSN_1.DEF, ASSPAPOSN_1.CTOR, ASSPAPOSN_1.CTDA, ASSPAPOSN_1.MDOR, " +
                                                        "ASSPAPOSN_1.MDDA, ASSPAPRJ_1.APRNAME, ASSPAFORM_1.AFNAME, ASSPAGROUP_1.AGNAME," +
                                                        "ASSPAFORG_1.FLOWPATH, ASSPAFORG_1.NAMEPATH " +
                            "FROM HASDLIB.ASSPAPOSN ASSPAPOSN_1, HASDLIB.ASSPAPRJ ASSPAPRJ_1," +
                                                        "HASDLIB.ASSPAFORM ASSPAFORM_1, HASDLIB.ASSPAGROUP ASSPAGROUP_1," +
                                                        "HASDLIB.ASSPAFORG ASSPAFORG_1 " +
                            "WHERE ASSPAPOSN_1.APRID = ASSPAPRJ_1.APRID AND ASSPAPOSN_1.AFID = ASSPAFORM_1.AFID AND " +
                                                        "ASSPAPOSN_1.AGID = ASSPAGROUP_1.AGID AND ASSPAPOSN_1.APRID = ASSPAFORG_1.APRID AND " +
                                                        " ASSPAPOSN_1.EMPNO = ASSPAFORG_1.EMPNO) A_1 WHERE RVNO = '00000' AND APRID =" + projectId;
                var command = new OleDbCommand(sqlQuery, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetPersonFlow(int projectId, string employeeNo)
        {
            DataTable dt = new DataTable();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return dt; }
                var sqlQuery = "SELECT A.APRID, A.AGID, A.AFID, A.EMPNO, A.RVNO, A.EMPNAME, A.STADA, A.TRYDA, A.BRCD, A.BRNAME, A.DEPCD, " +
                             "A.DEPNAME, A.APJCCD, A.APJCNAME, A.ASJPCD, A.ASJPNAME, A.APED, A.EMPTP, A.ASTATUS, A.DEF, A.CTOR, " +
                             " A.CTDA, A.MDOR, A.MDDA, A.APRNAME, A.AFNAME, A.AGNAME, A.FLOWPATH, A.NAMEPATH, " +
                             " B.EMPNAME AS ReName " +
                             " FROM (SELECT HASDLIB.ASSPAPOSN.APRID, HASDLIB.ASSPAPOSN.AGID, HASDLIB.ASSPAPOSN.AFID, " +
                                                         "HASDLIB.ASSPAPOSN.EMPNO, HASDLIB.ASSPAPOSN.RVNO, HASDLIB.ASSPAPOSN.EMPNAME, " +
                                                         "HASDLIB.ASSPAPOSN.STADA, HASDLIB.ASSPAPOSN.TRYDA, HASDLIB.ASSPAPOSN.BRCD, " +
                                                         "HASDLIB.ASSPAPOSN.BRNAME, HASDLIB.ASSPAPOSN.DEPCD, HASDLIB.ASSPAPOSN.DEPNAME, " +
                                                         "HASDLIB.ASSPAPOSN.APJCCD, HASDLIB.ASSPAPOSN.APJCNAME, HASDLIB.ASSPAPOSN.ASJPCD, " +
                                                        " HASDLIB.ASSPAPOSN.ASJPNAME, HASDLIB.ASSPAPOSN.APED, HASDLIB.ASSPAPOSN.EMPTP, " +
                                                         "HASDLIB.ASSPAPOSN.ASTATUS, HASDLIB.ASSPAPOSN.DEF, HASDLIB.ASSPAPOSN.CTOR, " +
                                                        " HASDLIB.ASSPAPOSN.CTDA, HASDLIB.ASSPAPOSN.MDOR, HASDLIB.ASSPAPOSN.MDDA, " +
                                                         "HASDLIB.ASSPAPRJ.APRNAME, HASDLIB.ASSPAFORM.AFNAME, HASDLIB.ASSPAGROUP.AGNAME, " +
                                                         "HASDLIB.ASSPAFORG.FLOWPATH, HASDLIB.ASSPAFORG.NAMEPATH " +
                            " FROM HASDLIB.ASSPAPOSN, HASDLIB.ASSPAPRJ, HASDLIB.ASSPAFORM, HASDLIB.ASSPAGROUP, " +
                                                         "HASDLIB.ASSPAFORG " +
                            " WHERE HASDLIB.ASSPAPOSN.APRID = HASDLIB.ASSPAPRJ.APRID AND " +
                                                        " HASDLIB.ASSPAPOSN.AFID = HASDLIB.ASSPAFORM.AFID AND " +
                                                         "HASDLIB.ASSPAPOSN.AGID = HASDLIB.ASSPAGROUP.AGID AND " +
                                                        " HASDLIB.ASSPAPOSN.APRID = HASDLIB.ASSPAFORG.APRID AND " +
                                                         "HASDLIB.ASSPAPOSN.EMPNO = HASDLIB.ASSPAFORG.EMPNO) A, HASDLIB.ASSPAFORG B " +
                             "WHERE A.RVNO = B.EMPNO AND A.APRID = B.APRID AND A.APRID =" + projectId + " AND A.EMPNO = '" + employeeNo + "'" +
                             " UNION " +
                            "SELECT APRID, AGID, AFID, EMPNO, RVNO, EMPNAME, STADA, TRYDA, BRCD, BRNAME, DEPCD, DEPNAME, APJCCD, " +
                            "APJCNAME, ASJPCD, ASJPNAME, APED, EMPTP, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA, APRNAME, AFNAME, " +
                            "AGNAME, FLOWPATH, NAMEPATH, '' AS ReName " +
                            "FROM (SELECT ASSPAPOSN_1.APRID, ASSPAPOSN_1.AGID, ASSPAPOSN_1.AFID, ASSPAPOSN_1.EMPNO," +
                                                        "ASSPAPOSN_1.RVNO, ASSPAPOSN_1.EMPNAME, ASSPAPOSN_1.STADA, ASSPAPOSN_1.TRYDA," +
                                                        "ASSPAPOSN_1.BRCD, ASSPAPOSN_1.BRNAME, ASSPAPOSN_1.DEPCD, ASSPAPOSN_1.DEPNAME," +
                                                        "ASSPAPOSN_1.APJCCD, ASSPAPOSN_1.APJCNAME, ASSPAPOSN_1.ASJPCD," +
                                                        "ASSPAPOSN_1.ASJPNAME, ASSPAPOSN_1.APED, ASSPAPOSN_1.EMPTP, ASSPAPOSN_1.ASTATUS," +
                                                        "ASSPAPOSN_1.DEF, ASSPAPOSN_1.CTOR, ASSPAPOSN_1.CTDA, ASSPAPOSN_1.MDOR, " +
                                                        "ASSPAPOSN_1.MDDA, ASSPAPRJ_1.APRNAME, ASSPAFORM_1.AFNAME, ASSPAGROUP_1.AGNAME," +
                                                        "ASSPAFORG_1.FLOWPATH, ASSPAFORG_1.NAMEPATH " +
                            "FROM HASDLIB.ASSPAPOSN ASSPAPOSN_1, HASDLIB.ASSPAPRJ ASSPAPRJ_1," +
                                                        "HASDLIB.ASSPAFORM ASSPAFORM_1, HASDLIB.ASSPAGROUP ASSPAGROUP_1," +
                                                        "HASDLIB.ASSPAFORG ASSPAFORG_1 " +
                            "WHERE ASSPAPOSN_1.APRID = ASSPAPRJ_1.APRID AND ASSPAPOSN_1.AFID = ASSPAFORM_1.AFID AND " +
                                                        "ASSPAPOSN_1.AGID = ASSPAGROUP_1.AGID AND ASSPAPOSN_1.APRID = ASSPAFORG_1.APRID AND " +
                                                        " ASSPAPOSN_1.EMPNO = ASSPAFORG_1.EMPNO) A_1 WHERE RVNO = '00000' AND APRID =" + projectId + " AND EMPNO = '" + employeeNo + "'";
                var command = new OleDbCommand(sqlQuery, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
            }
            return dt;
        }

    }
}

