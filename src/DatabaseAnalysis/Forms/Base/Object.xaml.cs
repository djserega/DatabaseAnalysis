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
using EF = DatabaseAnalysis.EntityFramework;

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

        private EF.IUnitOfWork _unitOfWork;

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

        private Object() { }

        internal Object(EF.IUnitOfWork unitOfWork, OpenFormEvents openFormEvents, int? id = null)
        {
            InitializeComponent();

            _unitOfWork = unitOfWork;

            var _repo = _unitOfWork.GetRepository<Models.Base>();

            if (id is int _id)
            {
                _ref = _repo.GetFirstOrDefault(f => f.Code == _id);
            }
            else
                _ref = new Models.Base();

            DataContext = _ref;
            _unitOfWork = unitOfWork;

            _ref.Name = "test";
        }

        private void GetStructureDB_Click(object sender, RoutedEventArgs e)
        {
            List<Models.StructureDB> structureDB = new ConnectTo1C(_ref, "1235679").GetStructureDB();
            _ref.StructureDBToStructures(structureDB);
        }

        private void GetCountRecords_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GetSizeTable_Click(object sender, RoutedEventArgs e)
        {
            new ConnectTo1C(_ref, "1235679").GetSizeTable();
        }

        private void ButtonSaveAndClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
