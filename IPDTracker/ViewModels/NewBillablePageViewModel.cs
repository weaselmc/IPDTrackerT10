using IPDTracker.IPDTrackerServiceReference;
using IPDTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace IPDTracker.ViewModels
{
    public class NewBillablePageViewModel : ViewModelBase
    {
        public NewBillablePageViewModel()
        {
            billable = new BillingEntry();
            billable.BillableId = Guid.NewGuid();
            billable.BillableDate = DateTime.Now;
        }

        public BillingEntry billable  { get; set; }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            //Value = (suspensionState.ContainsKey(nameof(Value))) ? suspensionState[nameof(Value)]?.ToString() : parameter?.ToString();
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                //suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void GotoMainPage()
        {
            NavigationService.Navigate(typeof(Views.MainPage), billable);
        }
    }
}
