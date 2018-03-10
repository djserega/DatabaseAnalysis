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
using DatabaseAnalysis;

namespace DatabaseAnalysis.Forms.Base
{
    /// <summary>
    /// Логика взаимодействия для Object.xaml
    /// </summary>
    public partial class Object : Page
    {

        private OpenFormEvents _openFormEvents;
        private GeneralMethods gm = new GeneralMethods();

        private string _formNameOriginal = "ObjectBase";
        private string _titleOriginal = "Базы данных";

        private Models.Base _ref;
        private ICollection<Models.BaseStructures> _structures;
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
            _structures = new List<Models.BaseStructures>();
            _openFormEvents = openFormEvents;

            var _repoBase = _unitOfWork.GetRepository<Models.Base>();
            var _repoStructures = _unitOfWork.GetRepository<Models.BaseStructures>();

            if (id is int _id)
            {
                _ref = _repoBase.GetFirstOrDefault(f => f.Code == _id);
                _id = _ref.Code;

                ICollection<Models.BaseStructures> structures = _repoStructures.GetList(f => f.Base.Code == _ref.Code);

                _structures = _ref.Structures;

                DataGridBaseStructures.ItemsSource = _structures;

                Title += $" {_ref.Name}";
            }
            else
            {
                _ref = new Models.Base();
                Title += " (новый)";
            }

            SetFormSettings();

            DataContext = _ref;
            _unitOfWork = unitOfWork;
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
            if (SaveObject())
                _openFormEvents.ClosingCurrentForm();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveObject();
        }

        private bool SaveObject()
        {
            try
            {
                var repoBase = _unitOfWork.GetRepository<Models.Base>();
                var repoStructure = _unitOfWork.GetRepository<Models.BaseStructures>();

                _ref.LastModified = DateTime.Now;

                if (_ref.Code == 0)
                {
                    repoBase.Insert(_ref);
                    repoStructure.Insert(_ref.Structures);
                }
                else
                {
                    repoBase.Update(_ref);
                    repoStructure.Update(_ref.Structures);
                }

                _unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Dialog.ShowMessage(ex.Message, 0);
                return false;
            }
        }

        private void SetFormSettings()
        {
            if (String.IsNullOrWhiteSpace(_ref.Name))
                Title = $"{_titleOriginal} {_ref.Name}";
            else if (_ref.Code != 0)
                Title = $"{_titleOriginal} {_ref.Code}";
            else
                Title = $"{_titleOriginal}";

            if (_ref.Code == 0)
                Name = $"{_formNameOriginal}";
            else
                Name = $"{_formNameOriginal}_{_ref.Code}";
        }
    }
}
