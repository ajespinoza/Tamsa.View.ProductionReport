using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tenaris.View.ExitApplication.ViewModel
{
    public class ExitHistoricViewModel : BaseViewModel
    {
        public ExitHistoricViewModel(string header)
            : base(header)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;
        }
    }
}
