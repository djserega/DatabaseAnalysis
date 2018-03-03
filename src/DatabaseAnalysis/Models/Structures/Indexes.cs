using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.Models
{
    public class Indexes
    {
        [Key]
        public int Code { get; set; }
        public string StorageIndexName { get; set; }        // ИмяИндексаХранения
        public List<Fields> Fields { get; set; }            // Поля
    }
}
