using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.Models
{
    public class Fields
    {
        [Key]
        public int Code { get; set; }
        public string StorageFieldName { get; set; }        // ИмяПоляХранения
        public string FieldName { get; set; }               // ИмяПоля
        public string Metadata { get; set; }                // Метаданные
    }
}
