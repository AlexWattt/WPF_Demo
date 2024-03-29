﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
//using Finisar.SQLite;
using System.Data.SQLite;

namespace ModernUINavigationApp1.Pages


{
    //////SQLite Edition
    class DataAccess
    {

        // Connection String for  SQlite Edition
        //static string _ConnectionString = @"Data Source=SqliteDBtest;Version=3;New=False;Compress=True";
        //Data Source=DemoT.db;Version=3;New=False;Compress=True;

        // Use for ..exe.config file 
        //   static string _ConnectionString = Sqtlie_project_tutorial.Properties.Settings.Default.SqliteDBtestConnectionString1;




        static SQLiteConnection _Connection = null;
        public static SQLiteConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new SQLiteConnection();
                    _Connection.ConnectionString = "Data Source=database.sqlite;Version=3;New=False;Compress=True";
                    _Connection.Open();

                    return _Connection;
                }
                else if (_Connection.State != System.Data.ConnectionState.Open)
                {
                    _Connection.Open();

                    return _Connection;
                }
                else
                {
                    return _Connection;
                }
            }
        }

        public static DataSet GetDataSet(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds);
            Connection.Close();

            return ds;
        }

        public static DataTable GetDataTable(string sql)
        {
            Console.WriteLine(sql);
            DataSet ds = GetDataSet(sql);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public static int ExecuteSQL(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            return cmd.ExecuteNonQuery();
        }
    }


}