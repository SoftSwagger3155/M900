using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Data.SQLite;
using System.Data;

namespace MF900
{
    public static class SQLiteHelper
    {
        public static string ConStr ="";

        /// <summary>
        /// �������ݿ�
        /// </summary>
        public static bool NewDbFile(string dbPath)
        {
            try
            {
                SQLiteConnection.CreateFile(dbPath);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("�½����ݿ��ļ�" + dbPath + "ʧ�ܣ�\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="dbPath">ָ�����ݿ��ļ�</param>
        /// <param name="tableName">������</param>
        public static void NewTable(string dbPath, string tableName)
        {
            SQLiteConnection sqliteConn = new SQLiteConnection("Data Source=" + dbPath);
            if (sqliteConn.State != System.Data.ConnectionState.Open)
            {
                sqliteConn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = sqliteConn;
                cmd.CommandText = "CREATE TABLE " + tableName + "(Role varchar,Rold varchar,Password varchar)";
                cmd.ExecuteNonQuery();
            }  //Alter table tabname add primary key(col) 
            sqliteConn.Close();
        }

        /// <summary>
        /// ִ����ɾ��
        /// </summary>
        /// <param name="sql"><
        /// ram>
        /// <returns></returns>
        public static int Update(string sql)
        {
            SQLiteConnection DBConnection = new SQLiteConnection(ConStr);
            SQLiteCommand cmd = new SQLiteCommand(sql, DBConnection);
            try
            {
                DBConnection.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DBConnection.Close();
            }
        }
        public static int Updates(string sql, SQLiteParameter[] param)
        {
            SQLiteConnection DBConnection = new SQLiteConnection(ConStr);
            SQLiteCommand cmd = new SQLiteCommand(sql, DBConnection);
            try
            {
                DBConnection.Open();
                cmd.Parameters.AddRange(param);//��Ӳ���
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                WriteLog("ִ��Update(string sql)�����������󣬴�����־��" + ex.Message);
                throw;
            }
            finally
            {
                DBConnection.Close();
            }
        }
        /// <summary>
        /// ��ȡ��һ���
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            SQLiteConnection DBConnection = new SQLiteConnection(ConStr);
            SQLiteCommand cmd = new SQLiteCommand(sql, DBConnection);
            try
            {
                DBConnection.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                DBConnection.Close();
            }
        }
        /// <summary>
        /// ���ؽ����
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SQLiteDataReader GetReader(string sql)
        {
            SQLiteConnection DBConnection = new SQLiteConnection(ConStr);
            SQLiteCommand cmd = new SQLiteCommand(sql, DBConnection);
            try
            {
                DBConnection.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                DBConnection.Close();
                throw ex;
            }


        }
        /// <summary>
        /// �������ݼ�
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql)
        {
            SQLiteConnection DBConnection = new SQLiteConnection(ConStr);
            SQLiteCommand cmd = new SQLiteCommand(sql, DBConnection);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
             DataSet ds = new DataSet();
            try
            {
                DBConnection.Open();
                da.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                DBConnection.Close();
            }
        }


        #region ��������ִ�ж���SQL���
        /// <summary>
        /// ��������ִ�ж���SQL���
        /// </summary>
        /// <param name="sqlList"></param>
        /// <returns></returns>
        public static bool UpdateByTran(List<string> sqlList)
        {
            SQLiteConnection DBConnection = new SQLiteConnection(ConStr);
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = DBConnection;
            try
            {
                DBConnection.Open();
                cmd.Transaction = DBConnection.BeginTransaction();//��������
                foreach (string itemsql in sqlList)
                {
                    cmd.CommandText = itemsql;
                    cmd.ExecuteNonQuery();
                }
                cmd.Transaction.Commit();//�ύ����
                return true;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                    cmd.Transaction.Rollback();//�ع�����
                throw new Exception("�������񷽷�ʱ���ִ���" + ex.Message);
            }
            finally
            {
                if (cmd.Transaction != null)
                    cmd.Transaction = null;//�������
                DBConnection.Close();

            }
        }
        #endregion

        #region ������Ϣд����־
        /// <summary>
        /// ��������Ϣд����־�ļ�
        /// </summary>
        /// <param name="msg"></param>
        private static void WriteLog(string msg)
        {
            FileStream fs = new FileStream("Log.text", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("[{0}]  ������Ϣ��{1}", DateTime.Now.ToString(), msg);
            sw.Close();
            fs.Close();
        }
        #endregion

        #region ִ�д�������SQL���
        /// <summary>
        /// ִ����ɾ��
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Update(string sql, SQLiteParameter[] param)
        {
            SQLiteConnection DBConnection = new SQLiteConnection(ConStr);
            SQLiteCommand cmd = new SQLiteCommand(sql, DBConnection);
            try
            {
                DBConnection.Open();
                cmd.Parameters.AddRange(param);//��Ӳ���
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                WriteLog("ִ��Update(string sql)�����������󣬴�����־��" + ex.Message);
                throw;
            }
            finally
            {
                DBConnection.Close();
            }
        }
        /// <summary>
        /// ���ص�һ���
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql, SQLiteParameter[] param)
        {
            SQLiteConnection DBConnection = new SQLiteConnection(ConStr);
            SQLiteCommand cmd = new SQLiteCommand(sql, DBConnection);
            try
            {
                DBConnection.Open();
                cmd.Parameters.AddRange(param);//��Ӳ���
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                WriteLog("ִ��GetSingleResult(string sql)�����������󣬴�����־��" + ex.Message);
                throw;
            }
            finally
            {
                DBConnection.Close();
            }
        }
        /// <summary>
        /// �������ݼ�
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SQLiteDataReader GetReader(string sql, SQLiteParameter[] param)
        {
            SQLiteConnection DBConnection = new SQLiteConnection(ConStr);
            SQLiteCommand cmd = new SQLiteCommand(sql, DBConnection);
            try
            {
                DBConnection.Open();
                cmd.Parameters.AddRange(param);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                WriteLog("ִ��GetReader(string sql)�����������󣬴�����־��" + ex.Message);
                DBConnection.Close();
                throw ex;
            }

        }
        #endregion
    }
}
