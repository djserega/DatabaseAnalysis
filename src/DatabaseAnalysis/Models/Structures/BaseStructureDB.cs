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

        public BaseStructures BaseStructures { get; set; }

        public string TableName { get; set; }
        public string StorageTableName { get; set; }
        public string Metadata { get; set; }
        public string Purpose { get; set; }
        public List<Fields> Fields { get; set; }
        public List<Indexes> Indexes { get; set; }

        public long SizeTable { get; set; }
        public long CountRecords { get; set; }


        public BaseStructureDB()
        {
            Fields = new List<Fields>();
            Indexes = new List<Indexes>();
        }

        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case "StorageTableName":
                        return StorageTableName;
                    case "TableName":
                        return TableName;
                    case "Metadata":
                        return Metadata;
                    case "Purpose":
                        return Purpose;
                    default:
                        throw new NotImplementedException($"Получение свойства {propertyName} не реализовано.");
                }
            }
            set
            {
                switch (propertyName)
                {
                    case "StorageTableName":
                        StorageTableName = value;
                        break;
                    case "TableName":
                        TableName = value;
                        break;
                    case "Metadata":
                        Metadata = value;
                        break;
                    case "Purpose":
                        Purpose = value;
                        break;
                    default:
                        throw new NotImplementedException($"Установка свойства {propertyName} не реализовано.");
                }
            }
        }

    }
}
