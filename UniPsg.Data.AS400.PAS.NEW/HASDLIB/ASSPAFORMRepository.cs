using System;
using System.Collections.Generic;
using UniPsg.Model.PAS.AS400;
using System.Data;
using System.Data.OleDb;

namespace UniPsg.Data.AS400.PAS
{
    public class ASSPAFORMRepository
    {
        private string ConnectionString = string.Empty;
        public ASSPAFORMRepository()
        {
            ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSCTST"].ConnectionString;
        }
        //private string ConnectionString = "Provider=IBMDA400;User ID=HASCMN;Password=HAS2017598;Data Source=PSCTST;Transport Product=Client Access;SSL=DEFAULT;";
        public List<ASSPAFORM> Get()
        {
            var lists = new List<ASSPAFORM>();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return lists; }
                var sqlQuery = "SELECT AFID, AFNAME, ACID, AFGRP, AFITEM, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPAFORM ";
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ASSPAFORM item = new ASSPAFORM();
                        item.AFID = Convert.ToInt32(reader["AFID"]);
                        item.AFNAME = Convert.ToString(reader["AFNAME"]);
                        item.ACID = Convert.ToInt32(reader["ACID"]);
                        item.AFGRP = Convert.ToString(reader["AFGRP"]);
                        item.AFITEM = Convert.ToString(reader["AFITEM"]);
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

        public ASSPAFORM GetById(int id)
        {
            var result = new ASSPAFORM();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return result; }
                string sqlQuery = string.Format("SELECT AFID, AFNAME, ACID, AFGRP, AFITEM, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA FROM HASDLIB.ASSPAFORM WHERE AFID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.AFID = Convert.ToInt32(reader["AFID"]);
                        result.AFNAME = Convert.ToString(reader["AFNAME"]);
                        result.ACID = Convert.ToInt32(reader["ACID"]);
                        result.AFGRP = Convert.ToString(reader["AFGRP"]);
                        result.AFITEM = Convert.ToString(reader["AFITEM"]);
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

        public int GetLastId()
        {

            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return 0; }
                string sqlQuery = string.Format("SELECT * FROM HASDLIB.ASSPAFORM");
                var command = new OleDbCommand(sqlQuery, connection);
                var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.HasRows)
                {
                    string sqlQuery1 = string.Format("SELECT MAX(AFID) FROM HASDLIB.ASSPAFORM");
                    var command1 = new OleDbCommand(sqlQuery1, connection);
                    var lastid = command1.ExecuteScalar();
                    return Convert.ToInt32(lastid);
                }
                else
                    return 0;
            }
        }

        public void Insert(ASSPAFORM model)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("INSERT INTO HASDLIB.ASSPAFORM(AFID, AFNAME, ASTATUS, DEF, CTOR, CTDA, MDOR, MDDA, ACID, AFGRP, AFITEM) VALUES ({0},'{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8},'{9}','{10}')",
                    model.AFID, model.AFNAME, model.ASTATUS, model.DEF, model.CTOR, model.CTDA, model.MDOR, model.MDDA, model.ACID, model.AFGRP, model.AFITEM);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Update(ASSPAFORM model, int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("UPDATE HASDLIB.ASSPAFORM SET AFNAME = '{0}', ASTATUS = {1}, DEF = '{2}',MDOR = '{3}', MDDA = '{4}', ACID = {6}, AFGRP = '{7}', AFITEM = '{8}' WHERE AFID = {5}",
                    model.AFNAME, model.ASTATUS, model.DEF, model.MDOR, model.MDDA, id, model.ACID, model.AFGRP, model.AFITEM);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OleDbConnection(ConnectionString))
            {
                connection.Open(); if (connection.State != ConnectionState.Open) { return; }
                string sqlQuery = string.Format("DELETE FROM HASDLIB.ASSPAFORM WHERE AFID = {0}", id);
                var command = new OleDbCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

    }
}

