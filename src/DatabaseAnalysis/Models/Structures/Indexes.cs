using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.Models
{
    public class Indexes
    {
        public string StorageIndexName { get; set; }        // ИмяИндексаХранения
        public List<Fields> Fields { get; set; }            // Поля
    }
}
