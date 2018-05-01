using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using IPDTracker.Models;

namespace IPDTracker.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<Billable> Billables { get; set; }

        public MainPageViewModel()
        {
            Billables = new ObservableCollection<Billable>();

            Billables.Add(new Billable
            {
                BillableId = Guid.NewGuid(),
                ClientName = "Jimbo Jones",
                BillableDate = DateTime.Now,
                BillableTime = new TimeSpan(1, 45, 0),
                Notes = "Loves Beanies and breaking things"
            });
            Billables.Add(new Billable
            {
                BillableId = Guid.NewGuid(),
                ClientName = "Nelson Muntz",
                BillableDate = DateTime.Now,
                BillableTime = new TimeSpan(2, 30, 0),
                Notes = "Little slow .. but loves a good laugh"
            });
            Billables.Add(new Billable
            {
                BillableId = Guid.NewGuid(),
                ClientName = "Kearney Zzyzwicz",
                BillableDate = DateTime.Now,
                BillableTime = new TimeSpan(2, 30, 0),
                Notes = "Way to old to still be in school"
            });
            Billables.Add(new Billable
            {
                BillableId = Guid.NewGuid(),
                ClientName = "Dolph Starbeam",
                BillableDate = DateTime.Now,
                BillableTime = new TimeSpan(1, 10, 0),
                Notes = "Hippie parents"
            });
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                
            }
            if (parameter != null)
            {
                Billable billable = (Billable)parameter;
                Billables.Add(billable);
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        //public void GotoDetailsPage() => NavigationService.Navigate(typeof(Views.DetailPage), Value);
        public void GotoAddNew() =>
            NavigationService.Navigate(typeof(Views.NewBillablePage));
        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}
