using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
