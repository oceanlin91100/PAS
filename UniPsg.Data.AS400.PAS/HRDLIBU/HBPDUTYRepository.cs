using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class HBPDUTYRepository
    {
        private string ConnectionString = string.Empty;
        public HBPDUTYRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        public DataTable GetHBPDUTY(string employeeno)
        {
            DataTable dt = new DataTable();

            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return dt; }
                //var sqlQuery = "SELECT BRHCOD,DEPTCD,DEPTNAM,EMPNO FROM HRDLIBU.HBPDUTY WHERE EMPNO ='" + employeeno + "' AND SUBSTR(BRHCOD,1,3)='585'";
                var sqlQuery = "SELECT BRHCOD,DEPTCD,DEPTNAM,EMPNO FROM HRDLIBU.HBPDUTY WHERE EMPNO ='" + employeeno + "' ";
                var command = new OleDbCommand(sqlQuery, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
