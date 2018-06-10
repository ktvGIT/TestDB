using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Branch
{
    public class CReadBase : IDisposable
    {
        private SqlConnection mConn;
        protected SqlCommand mCmd;
        private bool mDisposed;

        public CReadBase(string SQLstr)
        {
            try
            {
                string connstr = GetConnectionString();
                mConn = new SqlConnection(connstr);
                mConn.Open();
                mCmd = new SqlCommand(SQLstr, mConn);
                mDisposed = false;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка обращения к БД, проверьте файл настоек " + "(" + ex + ")");
            }
        }

        public void ExecSql()
        {
            try
            {
                mCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка изменения БД " + "(" + ex + ")");
            }
        }

        private string GetConnectionString()
        {
            try
            {
                string returnValue = null;
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["ConnStr"];
                if (settings != null)
                    returnValue = settings.ConnectionString;
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка обращения к БД " + "(" + ex + ")");
            }
        }

        public void Dispose()
        {
            if (mDisposed)
                return;
            mConn.Close();
            mDisposed = true;
        }



    }
}

