﻿using DatabaseAnalysis.Models;
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

namespace DatabaseAnalysis
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        OpenFormEvents openFormEvents = new OpenFormEvents();
        LoadedFormEvents loadedFormEvents = new LoadedFormEvents();
        private int _countMouseLeftBtnDown = 0;

        #region Private fields

        private EF.IUnitOfWork _unitOfWork;
        private Dictionary<string, Page> _listPage = new Dictionary<string, Page>();
        private List<string> _listSheets = new List<string>();

        #endregion


        public MainWindow()
        {
            InitializeComponent();

            _unitOfWork = new EF.UnitOfWork<EF.Context>(new EF.Context());
        }

        private void MainButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonBase_Click(object sender, RoutedEventArgs e)
        {
            Forms.Base.List form = new Forms.Base.List(_unitOfWork, openFormEvents);

            FrameMain.Content = form;
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {

        }


        #region Open forms

        #region Sheets

        private void AddSheetOpenForm(string nameForm, string caption)
        {
            if (!String.IsNullOrWhiteSpace(_listSheets.Find(f => f == nameForm)))
                return;

            Style styleOpenedForm;
            Style styleCloseForm;
            try
            {
                styleOpenedForm = (Style)FindResource("ButtonMenuOpened");
                styleCloseForm = (Style)FindResource("ButtonMenuOpenedClose");
            }
            catch (ResourceReferenceKeyNotFoundException)
            {
                Dialog.ShowMessage("Не удалось найти стиль оформления страницы открытого окна.");
                return;
            }

            StackPanel stackPanel = new StackPanel()
            {
                Name = nameForm + "StackPanel",
                Orientation = Orientation.Horizontal
            };

            Button buttonOpenForm = new Button()
            {
                Name = nameForm,
                Content = caption,
                Style = styleOpenedForm,
            };
            buttonOpenForm.Click += PressedButtonOpenForm;

            stackPanel.Children.Add(buttonOpenForm);

            Button buttonClose = new Button()
            {
                Name = nameForm + "Close",
                Content = "x",
                Style = styleCloseForm
            };
            buttonClose.Click += PressetButtonCloseForm;

            stackPanel.Children.Add(buttonClose);

            StackPanelOpened.Children.Add(stackPanel);

            _listSheets.Add(nameForm);
        }

        private void PressedButtonOpenForm(object sender, RoutedEventArgs e)
        {
            OpenForm(((Button)e.Source).Name);
        }

        private void PressetButtonCloseForm(object sender, RoutedEventArgs e)
        {
            string formName = ((Button)e.Source).Name;

            formName = formName.Substring(0, formName.Length - 5);

            DeletePageInListPages(formName);
        }

        #endregion

        #region Change frame

        private void OpenForm(string formName)
        {
            if (FindPageName(formName) == null)
                openFormEvents.OpenForm += OpenOtherForm;

            Page form = FindPageName(formName);

            if (form == null)
            {
                if (formName.EndsWith("Base"))
                {
                    switch (formName)
                    {
                        //case "ListRequest":
                        //    form = new Forms.Base.List(openFormEvents);
                        //    break;
                        case "ObjectRequest":
                            form = new Forms.Base.Object(_unitOfWork, openFormEvents);
                            break;
                    }
                }
                else
                    return;

                form.Loaded += OpenForm_Loaded;
                AddPageInListPages(formName, form);
            }
            //else
            //    form.Loaded -= OpenForm_Loaded;

            if (form != null)
                FrameMain.Content = form;
        }

        private void OpenForm_Loaded(object sender, RoutedEventArgs e)
        {
            Page page = (Page)e.Source;
            if (FindPageName(page.Name) != null)
                AddSheetOpenForm(page.Name, page.Title);
        }

        internal void OpenOtherForm()
        {
            if (String.IsNullOrWhiteSpace(openFormEvents.PageName))
                return;

            OpenForm(openFormEvents.PageName);
        }

        #endregion

        #region List pages

        private Page FindPageName(string pageName)
        {
            Page form = null;

            var findedPage = _listPage.FirstOrDefault(f => f.Key == pageName);
            if (!String.IsNullOrWhiteSpace(findedPage.Key)
                && findedPage.Value != null)
                form = findedPage.Value;

            return form;
        }

        private void AddPageInListPages(string formName, Page form)
        {
            if (FindPageName(formName) == null)
                _listPage.Add(formName, form);
        }

        private void DeletePageInListPages(string formName)
        {
            if (String.IsNullOrWhiteSpace(formName))
                return;

            Page page = FindPageName(formName);

            if (page != null)
            {
                if (FrameMain.Content != null
                    & FrameMain.Content is Page
                    & ((Page)FrameMain.Content).Name == formName)
                    FrameMain.Content = null;

                _listPage.Remove(formName);
                _listSheets.Remove(_listSheets.Find(f => f == formName));

                string formNameClose = formName + "StackPanel";
                var childrenElement = StackPanelOpened.Children;
                for (int i = 0; i < childrenElement.Count; i++)
                {
                    if (((StackPanel)childrenElement[i]).Name == formNameClose)
                    {
                        childrenElement.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        #endregion

        #endregion


    }
}
