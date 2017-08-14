using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class WFPPERMRepository
    {
        private string ConnectionString = string.Empty;
        public WFPPERMRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        public DataTable GetEmployees(string group, string brachCode, string unitCode, string jobCode)
        {
            DataTable dt = new DataTable();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                string sqlQuery = string.Empty;
                connection.Open(); if (connection.State != ConnectionState.Open) { return dt; }
                switch (group)
                {
                    case "1": //-- 督導 SELECT* FROM AS400DB..WFPPERM WITH(NOLOCK) WHERE COMPCD = '585' AND JOBPOS IN('A25') AND EMPTYP = 'A' AND POSCON = 0
                    case "2": //-- 分公司經理人 SELECT* FROM AS400DB..WFPPERM WITH(NOLOCK) WHERE COMPCD = '585' AND JOBPOS IN('A30', 'A31', 'A36') AND EMPTYP = 'A' AND POSCON = 0                        
                        sqlQuery = "SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = '585' AND JOBPOS IN('" + jobCode + "') AND EMPTYP = 'A' AND POSCON = 0 " +
                            "UNION SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = 'PIP' AND JOBPOS IN('" + jobCode + "') AND EMPTYP = 'A' AND POSCON = 0 " +
                            "UNION SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = 'PCM' AND JOBPOS IN('" + jobCode + "') AND EMPTYP = 'A' AND POSCON = 0 " +
                            "UNION SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = 'PVC' AND JOBPOS IN('" + jobCode + "') AND EMPTYP = 'A' AND POSCON = 0 ";
                        break;
                    case "3": //-- 部門主管:業務單位 SELECT* FROM AS400DB..WFPPERM WITH(NOLOCK) WHERE COMPCD = '585' AND JOBPOS IN('A20') AND RTRIM(BRHCOD) + RTRIM(DEPTCD) IN('5850130', '5850170', '5850510', '5850530', '5850600', '5850620', '5850630', '5850720') AND EMPTYP = 'A' AND POSCON = 0
                        sqlQuery = "SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = '585' AND JOBPOS IN ('" + jobCode + "') AND RTRIM(BRHCOD) + RTRIM(DEPTCD) IN ('" + unitCode + "') AND EMPTYP = 'A' AND POSCON = 0 " +
                            "UNION SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = 'PIP' AND JOBPOS IN ('A10') AND EMPTYP = 'A' AND POSCON = 0 " +
                            "UNION SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = 'PCM' AND JOBPOS IN ('A10') AND EMPTYP = 'A' AND POSCON = 0 " +
                            "UNION SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = 'PVC' AND JOBPOS IN ('A10') AND EMPTYP = 'A' AND POSCON = 0 ";
                        break;
                    case "5": //-- 部門主管:後勤支援單位 SELECT* FROM AS400DB..WFPPERM WITH(NOLOCK) WHERE COMPCD = '585' AND JOBPOS IN('A20') AND RTRIM(BRHCOD) + RTRIM(DEPTCD) IN('5850100', '5850110', '5850120', '5850200', '5850310', '5850400', '5850800') AND EMPTYP = 'A' AND POSCON = 0                         
                        sqlQuery = "SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = '585' AND JOBPOS IN ('" + jobCode + "') AND RTRIM(BRHCOD) + RTRIM(DEPTCD) IN ('" + unitCode + "') AND EMPTYP = 'A' AND POSCON = 0 ";
                        break;
                    case "4": //-- 營業主管 SELECT * FROM AS400DB..WFPPERM WITH (NOLOCK) WHERE COMPCD = '585' AND JOBPOS IN ('B10') AND EMPTYP = 'C' AND POSCON = 0
                        sqlQuery = "SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = '585' AND JOBPOS IN ('" + jobCode + "') AND EMPTYP = 'C' AND POSCON = 0";
                        break;
                    case "6": //--總公司管理職同仁(不含部級主管) SELECT* FROM AS400DB..WFPPERM WITH(NOLOCK) WHERE COMPCD = '585' AND BRHCOD = '5850' AND EMPTYP = 'B' AND POSCON = 0                        
                        sqlQuery = "SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = '585' AND BRHCOD = '5850' AND EMPTYP = 'B' AND POSCON = 0 AND DEPTCD <>'330' " +
                            "UNION SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = 'PCM' AND BRHCOD = 'PCM0' AND EMPTYP = 'B' AND POSCON = 0 " +
                            "UNION SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = 'PVC' AND BRHCOD = 'PVC0' AND EMPTYP = 'B' AND POSCON = 0 " +
                            "UNION SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = 'PIP' AND BRHCOD = 'PIAP0' AND EMPTYP = 'B' AND POSCON = 0 ";
                        break;
                    case "7":
                        //if (brachCode == "5850") //--總公司非管理職同仁 SELECT * FROM AS400DB..WFPPERM WITH(NOLOCK) WHERE EMPTYP NOT IN ('H', 'A', 'B', 'C', 'E') AND BRHCOD = '5850'
                        //    cmd = new SqlCommand("SELECT * FROM AS400DB..WFPPERM WITH(NOLOCK) WHERE COMPCD = '585' AND EMPTYP NOT IN ('H', 'A', 'B', 'C', 'E') AND BRHCOD = '5850' AND POSCON = 0 AND DEPTCD <>'330'", conn);
                        //else //--分公司稽核 SELECT * FROM AS400DB..WFPPERM WITH(NOLOCK)WHERE COMPCD = '585' AND BRHCOD <> '5850' AND JOBPOS = 'D02'
                        //    cmd = new SqlCommand("SELECT * FROM AS400DB..WFPPERM WITH(NOLOCK) WHERE COMPCD = '585' AND BRHCOD <> '5850' AND JOBPOS = 'D02' AND POSCON = 0", conn);                        
                        sqlQuery = "SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = '585' AND JOBPOS IN('D02', 'D09', 'G09', 'H01', 'H02') AND POSCON = 0  UNION SELECT * FROM WFDLIB.WFPPERMN WHERE JOBPOS IN('D01', 'D04') AND DEPTCD = '328' AND POSCON = 0  " +
                            "UNION SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = 'PCM' AND JOBPOS IN('D02', 'D09', 'G09', 'H01', 'H02') AND POSCON = 0 " +
                            "UNION SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = 'PVC' AND JOBPOS IN('D02', 'D09', 'G09', 'H01', 'H02') AND POSCON = 0 " +
                            "UNION SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = 'PIP' AND JOBPOS IN('D02', 'D09', 'G09', 'H01', 'H02') AND POSCON = 0 ";
                        break;
                }
                var command = new OleDbCommand(sqlQuery, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
            }
            return dt;
        }


        public DataTable GetEmployees(string jobCode)
        {
            DataTable dt = new DataTable();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                string sqlQuery = string.Empty;
                connection.Open(); if (connection.State != ConnectionState.Open) { return dt; }                
                sqlQuery = "SELECT * FROM WFDLIB.WFPPERMN WHERE COMPCD = '585' AND JOBPOS IN('" + jobCode + "')";               
                var command = new OleDbCommand(sqlQuery, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
