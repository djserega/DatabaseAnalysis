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
    /// Логика взаимодействия для List.xaml
    /// </summary>
    public partial class List : Page
    {
        private OpenFormEvents _openFormEvents;
        private RefreshDataGridEvents _refreshDataGrid;

        private EF.IUnitOfWork _unitOfWork;

        private ICollection<Models.Base> _baseList { get; set; }

        internal List(EF.IUnitOfWork unitOfWork, OpenFormEvents openFormEvents, RefreshDataGridEvents refreshDataGrid)
        {
            InitializeComponent();

            _unitOfWork = unitOfWork;

            _openFormEvents = openFormEvents;
            _refreshDataGrid = refreshDataGrid;

            _refreshDataGrid.RefreshDataGrid += _refreshDataGrid_RefreshDataGrid;

            SetItemSourceDataGrid();
        }

        private void _refreshDataGrid_RefreshDataGrid()
        {
            SetItemSourceDataGrid();
        }

        private void SetItemSourceDataGrid()
        {
            _baseList = _unitOfWork.GetRepository<Models.Base>().GetList();

            DataGridList.ItemsSource = _baseList;
        }
    }
}
