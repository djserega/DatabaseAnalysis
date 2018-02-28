using DatabaseAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DatabaseAnalysis
{
    internal class Json
    {
        internal List<StructureDB> ConvertStructureDB(string inputText)
        {
            var serializer = new JavaScriptSerializer();

            var objectRequest = serializer.Deserialize<List<StructureDB>>(inputText);

            return objectRequest;
        }
    }
}
