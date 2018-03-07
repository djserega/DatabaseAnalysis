using DatabaseAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis
{
    internal class ConnectTo1C
    {

        private Base _base;
        private string _uri;
        private string _token;

        internal Base Base
        {
            get
            {
                return _base;
            }
            set
            {
                if (_base != value)
                    _base = value;
            }
        }
        internal string Token
        {
            get
            {
                return _token;
            }
            set
            {
                if (_token != value)
                    _token = value;
            }
        }

        internal ConnectTo1C(Base db, string token)
        {
            Base = db;
            _uri = Base.URI;
            Token = token;
        }

        #region Base

        internal List<StructureDB> GetStructureDB()
            => new Json().ConvertStructureDB(CallGet());

        private string CallGet(int timeout = 10)
        {
            string textResponse = String.Empty;

#warning temp

            if (String.IsNullOrWhiteSpace(_uri))
            {
                string pathResult = Path.Combine(Environment.CurrentDirectory, "stucturedb.json");

                using (StreamReader reader = new StreamReader(pathResult, Encoding.Default))
                {
                    textResponse = reader.ReadToEnd();
                }

                return textResponse;
            }

            WebRequest webRequest = WebRequest.Create(_uri);
            webRequest.Credentials = new NetworkCredential(_base.User, _base.Password);

            webRequest.Method = "get";
            SetParameterWebRequest(ref webRequest, timeout);

            try
            {
                using (WebResponse response = webRequest.GetResponse())
                {
                    Stream stream = response.GetResponseStream();

                    if (stream.CanRead)
                    {
                        StreamReader streamReader = new StreamReader(stream);
                        textResponse = streamReader.ReadToEnd();
                    }
                }

            }
            catch (Exception ex)
            {
                Dialog.ShowMessage($"Не удалось получить структуру БД.\n{ex.Message}");
            }
            return textResponse;
        }

        #region other http methods

        private void AddHeaderToken(ref WebRequest webRequest)
        {
            if (!string.IsNullOrWhiteSpace(_token))
                webRequest.Headers.Add("token", _token);
        }

        private void SetParameterWebRequest(ref WebRequest webRequest, int timeout)
        {
            webRequest.Timeout = timeout * 1000;
            webRequest.ContentType = "application/json";
        }

        #endregion

        #endregion

        #region Server

        internal void GetSizeTable()
        {

#warning text query
            //string queryString = "DECLARE @pagesizeKB int " +
            //    "SELECT @pagesizeKB = low / 1024 FROM master.dbo.spt_values " +
            //    "WHERE number = 1 AND type = 'E' " +
            //    "SELECT table_name = OBJECT_NAME(o.id), " +
            //    "rows = i1.rowcnt, " +
            //    "reservedKB = (ISNULL(SUM(i1.reserved), 0) + ISNULL(SUM(i2.reserved), 0)) * @pagesizeKB, " +
            //    "dataKB = (ISNULL(SUM(i1.dpages), 0) + ISNULL(SUM(i2.used), 0)) * @pagesizeKB, " +
            //    "index_sizeKB = ((ISNULL(SUM(i1.used), 0) + ISNULL(SUM(i2.used), 0)) - (ISNULL(SUM(i1.dpages), 0) + ISNULL(SUM(i2.used), 0))) * @pagesizeKB, " +
            //    "unusedKB = ((ISNULL(SUM(i1.reserved), 0) + ISNULL(SUM(i2.reserved), 0 - (ISNULL(SUM(i1.used), 0) + ISNULL(SUM(i2.used), 0))) * @pagesizeKB " +
            //    "FROM sysobjects o " +
            //    "LEFT OUTER JOIN sysindexes i1 ON i1.id = o.id AND i1.indid < 2 " +
            //    "LEFT OUTER JOIN sysindexes i2 ON i2.id = o.id AND i2.indid = 255 " +
            //    "WHERE OBJECTPROPERTY(o.id, N'IsUserTable') = 1--same as: o.xtype = 'IsView' " +
            //    "OR(OBJECTPROPERTY(o.id, N'IsView') = 1 AND OBJECTPROPERTY(o.id, N'IsIndexed') = 1) " +
            //    "GROUP BY o.id, i1.rowcnt " +
            //    "ORDER BY 3 DESC";


            //using (SqlConnection connection = new SqlConnection(_connectionPath))
            //{
            //    SqlCommand command = new SqlCommand(queryString, connection);
            //    connection.Open();
            //    SqlDataReader reader = command.ExecuteReader();

            //    try
            //    {

            //    }
            //    catch
            //    {
            //    }
            //    finally
            //    {
            //        reader.Close();
            //    }
            //}

        }

        #endregion

    }
}
