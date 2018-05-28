using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using IPDTracker;
using IPDTracker.Models;
using IPDTracker.Views;
using Windows.UI.Xaml.Controls;
using IPDTracker.Services.DataServices;
using IPDTracker.IPDTrackerServiceReference;

namespace IPDTracker.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<BillingEntry> Billables { get; set; }
        public BillingEntry selectedBillable;
        public string DisplayTotal { get; set; }
        public MainPageViewModel()
        {            
            Billables = new ObservableCollection<BillingEntry>();                       
        }
        void SetDisplayTotal()
        {
            var total = Billables.Sum(p => p.BillableTime.Ticks);
            DisplayTotal = "Total Hours: " + new DateTime(total).ToString("H:mm");
            RaisePropertyChanged("DisplayTotal");
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                
            }
            if(mode == NavigationMode.New && Billables.Count==0)
            {

                Billables = await DataHelper.GetBillablesAsync(); // FileHelper.GetBillablesAsync();

                //Billables.Add(new Billable
                //{
                //    BillableId = Guid.NewGuid(),
                //    ClientName = "Jimbo Jones",
                //    BillableDate = DateTime.Now,
                //    BillableTime = new TimeSpan(1, 45, 0),
                //    Notes = "Loves Beanies and breaking things"
                //});
                //Billables.Add(new Billable
                //{
                //    BillableId = Guid.NewGuid(),
                //    ClientName = "Nelson Muntz",
                //    BillableDate = DateTime.Now,
                //    BillableTime = new TimeSpan(2, 30, 0),
                //    Notes = "Little slow .. but loves a good laugh"
                //});
                //Billables.Add(new Billable
                //{
                //    BillableId = Guid.NewGuid(),
                //    ClientName = "Kearney Zzyzwicz",
                //    BillableDate = DateTime.Now,
                //    BillableTime = new TimeSpan(2, 30, 0),
                //    Notes = "Way to old to still be in school"
                //});
                //Billables.Add(new Billable
                //{
                //    BillableId = Guid.NewGuid(),
                //    ClientName = "Dolph Starbeam",
                //    BillableDate = DateTime.Now,
                //    BillableTime = new TimeSpan(1, 10, 0),
                //    Notes = "Hippie parents"
                //});
                //await FileHelper.SetBillablesAsync(Billables);
            }
            if (parameter != null && mode != NavigationMode.Back)
            {
                BillingEntry billable = (BillingEntry)parameter;
                BillingEntry b;
                try
                {
                    b = Billables.First(i => i.BillableId == billable.BillableId);
                    if (await DataHelper.UpdateBillableAsync(b))
                    { 
                        b.BillableDate = billable.BillableDate;
                        b.BillableTime = billable.BillableTime;
                        b.ClientName = billable.ClientName;
                        b.Notes = billable.Notes;
                        RaisePropertyChanged();
                    }
                }
                catch
                {
                    if (await DataHelper.CreateBillableAsync(billable))
                        Billables.Add(billable);
                }
                //await FileHelper.SetBillablesAsync(Billables);
            }
            SetDisplayTotal();
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

        public async void DeleteBillable()
        {
            if (selectedBillable != null)
            {
                if (await DataHelper.DeleteBillableAsync(selectedBillable))
                    Billables.Remove(selectedBillable);
            }
        }

        public void BillableSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = (ListView)sender;
            selectedBillable = (BillingEntry)lv.SelectedItem;
        }
        public void EditBillable() =>
            NavigationService.Navigate(typeof(Views.EditBillablePage), selectedBillable);
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
