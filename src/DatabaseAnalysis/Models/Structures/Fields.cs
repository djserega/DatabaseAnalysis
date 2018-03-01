using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.Models
{
    public class Fields
    {
        public string StorageFieldName { get; set; }        // ИмяПоляХранения
        public string FieldName { get; set; }               // ИмяПоля
        public string Metadata { get; set; }                // Метаданные
    }
}
