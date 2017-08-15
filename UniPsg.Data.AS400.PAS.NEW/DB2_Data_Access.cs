using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniPsg.Data.AS400.PAS.NEW
{
    class DB2_Data_Access
    {
        public String Get(string QueryString)
        {
            var ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
            string JsonResult = string.Empty;
            using (var connection = new OleDbConnection(ConnectionString))
            {


                connection.Open(); if (connection.State != ConnectionState.Open) { return "Cannot Connect!"; }
                var sqlQuery = QueryString;
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader();


                if (reader.HasRows)
                {
                    var Field_Count = reader.FieldCount;

                    object[] Row_Value = new object[Field_Count];
                    string[] Field_Name = new string[Field_Count];
                    Type[] Field_Type = new Type[Field_Count];
                    JArray Total_Result = new JArray();

                    for (int i = 0; i < Field_Count; i++)
                    {
                        Field_Type[i] = reader.GetFieldType(i);
                        Field_Name[i] = reader.GetName(i);
                    }
                    while (reader.Read())
                    {
                        JObject Row_Result = new JObject(); ;

                        reader.GetValues(Row_Value);

                        for (int i = 0; i < Field_Count; i++)
                        {
                            JProperty Current_JSON_Property;
                            var Current_Value = Row_Value[i];

                            //因為所有TABLE裡面的數字都開成DECIMAL，所以要將關鍵字以外的數字都轉換回INT
                            if (Field_Type[i] == typeof(decimal) && !(Field_Name[i].Contains("WEIGHT") || Field_Name[i].Contains("SCORE") || Field_Name[i].Contains("RATE")))
                            {
                                int Current_Number = Convert.ToInt32(Current_Value);
                                Current_JSON_Property = new JProperty(Field_Name[i], Current_Number);

                            }
                            else
                            {
                                Current_JSON_Property = new JProperty(Field_Name[i], Current_Value);

                            }
                            Row_Result.Add(Current_JSON_Property);
                        };
                        Total_Result.Add(Row_Result);


                    }

                    JProperty result = new JProperty("Result", Total_Result);
                    JsonResult = JsonConvert.SerializeObject(result);
                }

            }


            return JsonResult;
        }

        public String Select_Table(string table)
        {
            string QueryString = string.Format("SELECT * FROM {0}", table);
            string Result = Get(QueryString);
            return Result;
        }
        public String Select_Table(string table, string field, string ID)
        {
            string QueryString = string.Format("SELECT * FROM {0} Where {1} = {2}", table, field, ID);
            string Result = Get(QueryString);
            return Result;
        }

    }
}
