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
    }
}
