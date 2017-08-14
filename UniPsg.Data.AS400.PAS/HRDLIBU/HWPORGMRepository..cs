using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class HWPORGMRepository
    {
        private string ConnectionString = string.Empty;
        public HWPORGMRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        public DataTable GetWFPORGM(string employeeno)
        {
            DataTable dt = new DataTable();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return dt; }
                var sqlQuery = "SELECT * FROM HRDLIBU.HWPORGM WHERE EMPNO ='" + employeeno + "'";
                var command = new OleDbCommand(sqlQuery, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetVP(string employeeno)
        {
            DataTable dt = new DataTable();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return dt; }
                var sqlQuery = "SELECT EMPNO, EMPNAME, EMPACCT, BRHCOD, BRHNAMB, DEPTCD, DEPTNAM, TEAMCD, MANGER, EMPNAME01, MNGACCT, ORGPWD, ORGNAME, 1 as level, EMPNO AS sort_path FROM HRDLIBU.HWPORGM WHERE EMPNO ='" + employeeno + "'";
                var command = new OleDbCommand(sqlQuery, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetUnits(string unitCode)
        {
            DataTable dt = new DataTable();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return dt; }
                var sqlQuery = "SELECT BRHCOD,DEPTCD, DEPTNAM, EMPNO FROM HRDLIBU.HWPORGM WHERE SUBSTR(BRHCOD, 1, 3) = '585' AND DEPTCD IN ('" + unitCode + "')  AND MANGER = '86760' GROUP BY BRHCOD, DEPTCD, DEPTNAM, EMPNO";
                var command = new OleDbCommand(sqlQuery, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetUnits()
        {

            DataTable dt = new DataTable();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return dt; }
                var sqlQuery = "SELECT BRHCOD,DEPTCD, DEPTNAM, EMPNO FROM HRDLIBU.HWPORGM WHERE SUBSTR(BRHCOD, 1, 3) = '585' AND MANGER = '86760' AND EMPNO <>'86760'  GROUP BY BRHCOD, DEPTCD, DEPTNAM, EMPNO";
                var command = new OleDbCommand(sqlQuery, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetFlow(string employeeNo)
        {
            DataTable dt = new DataTable();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return dt; }
                var sqlQuery = "WITH n (EMPNO,EMPNAME,EMPACCT,BRHCOD,BRHNAMB,DEPTCD,DEPTNAM,TEAMCD,MANGER,EMPNAME01,MNGACCT,ORGPWD,ORGNAME, level, sort_path ,name_path )  AS  (" +
                "SELECT EMPNO, EMPNAME, EMPACCT, BRHCOD, BRHNAMB, DEPTCD, DEPTNAM, TEAMCD, MANGER, EMPNAME01, MNGACCT, ORGPWD, ORGNAME, 1 as level," +
                " EMPNO, RTRIM(EMPNAME) || '(' || EMPNO || ')'  " +
                " FROM HRDLIBU.HWPORGM  " +
                "WHERE EMPNO = '" + employeeNo + "' " +
                "UNION ALL " +
                "SELECT child.EMPNO,child.EMPNAME,child.EMPACCT,child.BRHCOD,child.BRHNAMB,child.DEPTCD,child.DEPTNAM,child.TEAMCD,child.MANGER,child.EMPNAME01,child.MNGACCT,child.ORGPWD,child.ORGNAME, n.level + 1," +
                "child.EMPNO || ',' || n.sort_path  , RTRIM(child.EMPNAME) || '(' || child.EMPNO || ')' || ',' || n.name_path  " +
                " FROM HRDLIBU.HWPORGM as child ,n WHERE n.EMPNO = child.MANGER) " +
                "SELECT * FROM n ";
                var command = new OleDbCommand(sqlQuery, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
            }
            return dt;
        }
    }
}

