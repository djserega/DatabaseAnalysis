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

        private OpenFormEvents _openFormEvents = new OpenFormEvents();
        private LoadedFormEvents _loadedFormEvents = new LoadedFormEvents();
        private RefreshDataGridEvents _refreshDataGrid = new RefreshDataGridEvents();

        #region Private fields

        private int _countMouseLeftBtnDown = 0;

        private EF.IUnitOfWork _unitOfWork;
        private Dictionary<string, Page> _listPage = new Dictionary<string, Page>();
        private List<string> _listSheets = new List<string>();

        private bool _VisibilityElement_formBaseList;

        #endregion

        #region Window events

        public MainWindow()
        {
            InitializeComponent();

            SetVisibleElementsForm();

            _unitOfWork = new EF.UnitOfWork<EF.Context>(new EF.Context());
        }

        #endregion

        #region Windows style button

        private void MainButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MainButtonRestore_Click(object sender, RoutedEventArgs e)
        {
            ExpandWindowMainMenu();
        }

        private void ExpandWindowMainMenu()
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void MainButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region Main menu

        private void ButtonBase_Click(object sender, RoutedEventArgs e)
        {
            OpenForm("ListBase");
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            OpenForm("ObjectBase");
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            EditCurrentRowObjectBase();
        }

        private void TextBlockTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();

            if (e.ChangedButton == MouseButton.Left)
                _countMouseLeftBtnDown++;

            if (_countMouseLeftBtnDown > 1)
            {
                ExpandWindowMainMenu();
                _countMouseLeftBtnDown = 0;
            }
            else
                ResetCountMouseLeftBtnDownAsync();
        }

        #endregion

        #region Open forms

        #region Sheets

        private void AddSheetOpenForm(string nameForm, string caption)
        {
            if (!String.IsNullOrWhiteSpace(_listSheets.Find(f => f == nameForm)))
                return;


            Style styleSeparatorBetwenElementHorizontal5;
            try
            {
                styleSeparatorBetwenElementHorizontal5 = (Style)FindResource("SeparatorBetwenElementHorizontal5");
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
                Width = 150
            };
            buttonOpenForm.Click += PressedButtonOpenForm;

            stackPanel.Children.Add(buttonOpenForm);


            Button buttonClose = new Button()
            {
                Name = nameForm + "Close",
                Content = "x",
                Width = 15
            };
            buttonClose.Click += PressetButtonCloseForm;

            stackPanel.Children.Add(buttonClose);


            Separator separatorBetweenClose = new Separator()
            {
                Name = nameForm + "Separator",
                Style = styleSeparatorBetwenElementHorizontal5
            };

            stackPanel.Children.Add(separatorBetweenClose);


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

            CloseFormByName(formName);
        }

        #endregion

        #region Change frame

        private void OpenForm(string formName, int? code = null)
        {
            string pageName = formName;
            if (code != null)
                pageName += $"_{code}";

            Page form = FindPageName(pageName);

            if (form == null)
            {
                _openFormEvents.OpenForm += OpenOtherForm;
                _openFormEvents.CloseCurrentForm += OpenFormEvents_CloseCurrentForm;

                if (formName.EndsWith("Base"))
                {
                    switch (formName)
                    {
                        case "ListBase":
                            form = new Forms.Base.List(_unitOfWork, _openFormEvents, _refreshDataGrid);
                            break;
                        case "ObjectBase":
                            form = new Forms.Base.Object(_unitOfWork, _openFormEvents, _refreshDataGrid , code);
                            break;
                        default:
                            return;
                    }
                }
                else
                    return;

                form.Loaded += OpenForm_Loaded;
                AddPageInListPages(pageName, form);
            }

            if (form != null)
            {
                FrameMain.Content = form;
                FrameMain.DataContext = form;
            }
        }

        private void OpenFormEvents_CloseCurrentForm()
        {
            if (FrameMain.Content is Page currentPage)
                CloseFormByName(currentPage.Name);
        }

        private void OpenForm_Loaded(object sender, RoutedEventArgs e)
        {
            Page page = (Page)e.Source;
            if (FindPageName(page.Name) != null)
                AddSheetOpenForm(page.Name, page.Title);
        }

        internal void OpenOtherForm()
        {
            if (String.IsNullOrWhiteSpace(_openFormEvents.PageName))
                return;

            OpenForm(_openFormEvents.PageName);
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
                if (FrameMain.Content is Page contentPage)
                    if (contentPage.Name == formName)
                        FrameMain.Content = null;

                _listPage.Remove(formName);
                _listSheets.Remove(_listSheets.Find(f => f == formName));

                string formNameClose = formName + "StackPanel";
                var childrenElement = StackPanelOpened.Children;
                for (int i = 0; i < childrenElement.Count; i++)
                {
                    if (childrenElement[i] is StackPanel)
                    {
                        if (((StackPanel)childrenElement[i]).Name == formNameClose)
                        {
                            childrenElement.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }

        private void OpenLastPage()
        {
            if (_listPage.Count > 0)
                OpenForm(_listPage.Last().Key);
            else
                FrameMain.Content = null;

            _VisibilityElement_formBaseList = FrameMain.Content is Forms.Base.List;

            SetVisibleElementsForm();
        }

        #endregion

        #endregion

        #region Frame

        private void FrameMain_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _VisibilityElement_formBaseList = e.NewValue is Forms.Base.List;

            SetVisibleElementsForm();
        }

        #endregion

        #region Private methods

        private async void ResetCountMouseLeftBtnDownAsync()
        {
            await OtherMethods.StartTimerPause(0.5);
            _countMouseLeftBtnDown = 0;
        }

        private void EditCurrentRowObjectBase()
        {
            if (FrameMain.Content is Forms.Base.List formList)
            {
                if (formList.DataGridList.SelectedItem is Base objectBase)
                {
                    OpenForm("ObjectBase", objectBase.Code);
                }
            }
        }

        private void CloseFormByName(string formName)
        {
            DeletePageInListPages(formName);

            OpenLastPage();
        }


        #endregion

        #region Visibility

        private void SetVisibleElementsForm()
        {
            ButtonCreate.Visibility = _VisibilityElement_formBaseList ? Visibility.Visible : Visibility.Hidden;
            ButtonEdit.Visibility = _VisibilityElement_formBaseList ? Visibility.Visible : Visibility.Hidden;
        }


        #endregion

    }
}
