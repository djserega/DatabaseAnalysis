using DatabaseAnalysis.Models;
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

namespace DatabaseAnalysis
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetStructureDB_Click(object sender, RoutedEventArgs e)
        {

            Base db = new Base()
            {
                Server = GetValueTextBox(TextBoxServer),
                BaseName = GetValueTextBox(TextBoxBaseName),
                UserDB = GetValueTextBox(TextBoxUserDB),
                PasswordDB = GetValueTextBox(TextBoxPasswordDB),
                URI = GetValueTextBox(TextBoxUri),
                User = GetValueTextBox(TextBoxUser),
                Password = GetValueTextBox(TextBoxPassword),
            };

            List<StructureDB> structureDB = new ConnectTo1C(db, "1235679").GetStructureDB();

        }

        private string GetValueTextBox(TextBox element)
            => element.ToString();
    }
}
