using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis
{
    internal delegate void OpenFormName();
    internal delegate void CloseCurrentFormEvent();

    internal class OpenFormEvents : EventArgs
    {
        internal event OpenFormName OpenForm;
        internal event CloseCurrentFormEvent CloseCurrentForm;

        internal string PageName { get; set; }
        internal int Id { get; set; }

        internal void ToOpenForm()
        {
            if (OpenForm == null)
                return;

            OpenForm();
        }

        internal void ClosingCurrentForm()
        {
            if (CloseCurrentForm == null)
                return;

            CloseCurrentForm();
        }

    }

    internal delegate void LoadingForms();

    internal class LoadedFormEvents : EventArgs
    {
        internal event LoadingForms LoadingForms;

        internal string FormName { get; set; }
        internal string FormCaption { get; set; }

        internal void FormIsLoaded()
        {
            if (LoadingForms == null)
                return;

            LoadingForms();
        }

    }
}
