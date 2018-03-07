using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DatabaseAnalysis.Models
{
    public class Base
    {
        [Key]
        [DisplayName("Код")]
        public int Code { get; set; }
        [DisplayName("Наименование")]
        public string Name { get; set; }
        [DisplayName("Комментарий")]
        public string Comment { get; set; }

        [DisplayName("Сервер")]
        public string Server { get; set; }
        [DisplayName("Имя базы")]
        public string BaseName { get; set; }
        [DisplayName("Имя пользователя БД")]
        public string UserDB { get; set; }
        [DisplayName("Пароль пользователя БД")]
        public string PasswordDB { get; set; }


        [DisplayName("URI")]
        public string URI { get; set; }
        [DisplayName("Имя пользователя")]
        public string User { get; set; }
        [DisplayName("Пароль")]
        public string Password { get; set; }

        public ICollection<BaseStructures> Structures { get; set; }

        public Base()
        {
            Structures = new List<BaseStructures>();
        }

        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case "Name":
                        return Name;
                    case "Comment":
                        return Comment;
                    case "Server":
                        return Server;
                    case "BaseName":
                        return BaseName;
                    case "UserDB":
                        return UserDB;
                    case "PasswordDB":
                        return PasswordDB;
                    case "URI":
                        return URI;
                    case "User":
                        return User;
                    case "Password":
                        return Password;
                    default:
                        throw new NotImplementedException($"Получение свойства {propertyName} не реализовано.");
                }
            }
            set
            {
                switch (propertyName)
                {
                    case "Name":
                        Name = value;
                        break;
                    case "Comment":
                        Comment = value;
                        break;
                    case "Server":
                        Server = value;
                        break;
                    case "BaseName":
                        BaseName = value;
                        break;
                    case "UserDB":
                        UserDB = value;
                        break;
                    case "PasswordDB":
                        PasswordDB = value;
                        break;
                    case "URI":
                        URI = value;
                        break;
                    case "User":
                        User = value;
                        break;
                    case "Password":
                        Password = value;
                        break;
                    default:
                        throw new NotImplementedException($"Установка свойства {propertyName} не реализовано.");
                }
            }
        }


        internal void StructureDBToStructures(List<StructureDB> list)
        {
            Structures.Clear();

            BaseStructures baseStructures = new BaseStructures
            {
                Base = this,
                Date = DateTime.Now
            };

            List<BaseStructureDB> listBaseStructureDB = new List<BaseStructureDB>();

            Mapping<StructureDB, BaseStructureDB> mapping = new Mapping<StructureDB, BaseStructureDB>();

            foreach (StructureDB itemStructureDB in list)
            {

                BaseStructureDB baseStructureDB = new BaseStructureDB();

                mapping.MappingObject(itemStructureDB, baseStructureDB);

                listBaseStructureDB.Add(baseStructureDB);
            }
            baseStructures.StructureDB = listBaseStructureDB;
            Structures.Add(baseStructures);
        }

    }
}
