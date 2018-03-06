using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.Models
{
    public interface IStructureDB
    {
        string TableName { get; set; }
        string StorageTableName { get; set; }
        string Metadata { get; set; }
        string Purpose { get; set; }
        List<Fields> Fields { get; set; }
        List<Indexes> Indexes { get; set; }
    }

    public class StructureDB : IStructureDB
    {
        [Key]
        public int Code { get; set; }
        public string StorageTableName { get; set; }        // ИмяТаблицыХранения
        public string TableName { get; set; }               // ИмяТаблицы
        public string Metadata { get; set; }                // Метаданные
        public string Purpose { get; set; }                 // Назначение
        public List<Fields> Fields { get; set; }            // Поля
        public List<Indexes> Indexes { get; set; }          // Индексы

        public StructureDB()
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
