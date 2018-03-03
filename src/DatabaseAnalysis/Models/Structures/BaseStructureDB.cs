using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.Models
{
    public class BaseStructureDB : IStructureDB
    {

        [Key]
        public int Code { get; set; }

        public ICollection<BaseStructures> BaseStructures { get; set; }

        public string TableName { get; set; }
        public string StorageTableName { get; set; }
        public string Metadata { get; set; }
        public string Purpose { get; set; }
        public List<Fields> Fields { get; set; }
        public List<Indexes> Indexes { get; set; }

        public long SizeTable { get; set; }
        public long CountRecords { get; set; }
    }
}
