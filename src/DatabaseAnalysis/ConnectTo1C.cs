using DatabaseAnalysis.Models;
using System;
using System.Collections.Generic;
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
#warning temp

            WebRequest webRequest = WebRequest.Create(_uri);
            webRequest.Credentials = new NetworkCredential(_base.User, _base.Password);

            webRequest.Method = "get";
            SetParameterWebRequest(ref webRequest, timeout);

            using (WebResponse response = webRequest.GetResponse())
            {
                Stream stream = response.GetResponseStream();

                if (stream.CanRead)
                {
                    StreamReader streamReader = new StreamReader(stream);
                    textResponse = streamReader.ReadToEnd();
                }
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

    }
}
