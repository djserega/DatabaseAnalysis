using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.Models
{
    public class BaseStructures
    {
        [Key]
        public int Code { get; set; }

        public Base Base { get; set; }

        public DateTime Date { get; set; }

        public ICollection<BaseStructureDB> StructureDB { get; set; }

    }
}
