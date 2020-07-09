using HomeBudget.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Client.ViewModel
{
    public interface IPageViewModel
    {
        event EventHandler<DataToRefresh> RefreshData;
    }
}
