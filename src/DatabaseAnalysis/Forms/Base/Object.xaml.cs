using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Models = DatabaseAnalysis.Models;

namespace DatabaseAnalysis.Forms.Base
{
    /// <summary>
    /// Логика взаимодействия для Object.xaml
    /// </summary>
    public partial class Object : Page
    {

        private GeneralMethods gm = new GeneralMethods();

        private Models.Base _ref;
        private int _id;

        internal Models.Base Ref
        {
            get
            {
                return _ref;
            }
            set
            {
                if (_ref != value)
                {
                    _ref = value;
                }
            }
        }

        internal int? Id
        {
            get
            {
                if (_id == 0)
                    return null;
                else
                    return _id;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Установить id типа null недопустимо.");
                };

                int valueId = (int)value;

                if (_id != valueId)
                {
                    _id = valueId;
                }
            }
        }


        public Object()
        {
            InitializeComponent();

            this.DataContext = _ref;
        }

        private void GetStructureDB_Click(object sender, RoutedEventArgs e)
        {

            Models.Base db = new Models.Base()
            {
                Server = gm.GetValue(TextBoxServer),
                BaseName = gm.GetValue(TextBoxBaseName),
                UserDB = gm.GetValue(TextBoxUserDB),
                PasswordDB = gm.GetValue(PasswordBoxPasswordDB),
                URI = gm.GetValue(TextBoxURI),
                User = gm.GetValue(TextBoxUser),
                Password = gm.GetValue(PasswordBoxPassword),
            };

            List<Models.StructureDB> structureDB = new ConnectTo1C(db, "1235679").GetStructureDB();

        }

    }
}
