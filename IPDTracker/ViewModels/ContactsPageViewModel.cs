using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using IPDTracker.Models;
using IPDTracker.Views;
using Windows.UI.Xaml.Controls;
using IPDTracker.Services.DataServices;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Xaml;

namespace IPDTracker.ViewModels
{
    public class ContactsPageViewModel : ViewModelBase
    {
        public ObservableCollection<Contact> Contacts { get; set; }
        private bool isLoading = false;
        public Visibility IsLoading {
            get{
                if (isLoading)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
        public Contact selectedContact;
        //public string DisplayTotal { get; set; }
        public ContactsPageViewModel()
        {            
            Contacts = new ObservableCollection<Contact>();                       
        }

        async Task<ObservableCollection<Contact>> GetContactsAsync()
        {            
            ContactStore store = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AppContactsReadWrite);
            //find contact list
            ContactList contactlist = await store.CreateContactListAsync(Windows.ApplicationModel.Package.Current.DisplayName); //problem if it already exists
            ContactReader contactreader = contactlist.GetContactReader();
            ContactBatch contactbatch = await contactreader.ReadBatchAsync();           
            return new ObservableCollection<Contact>(contactbatch.Contacts); // store.FindContactsAsync()
            
        }
        
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                
            }
            if(mode == NavigationMode.New && Contacts.Count==0)
            {
                
                Contacts = await GetContactsAsync();  // await FileHelper.GetBillablesAsync();

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
                //Billable billable = (Billable)parameter;
                //Billable b;
                //try
                //{ 
                //    b = Billables.First(i => i.BillableId == billable.BillableId);
                //    b.BillableDate = billable.BillableDate;
                //    b.BillableTime = billable.BillableTime;
                //    b.ClientName = billable.ClientName;
                //    b.Notes = billable.Notes;
                //}
                //catch
                //{
                //    Billables.Add(billable);
                //}
                //await FileHelper.SetBillablesAsync(Billables);
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

        public void DeleteBillable()
        {
            //if(selectedBillable != null)
            //    Billables.Remove(selectedBillable);
        }

        public void ContactSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = (ListView)sender;
            selectedContact = (Contact)lv.SelectedItem;
        }
        public void EditBillable() =>
            NavigationService.Navigate(typeof(Views.EditBillablePage)); //, selectedBillable
        //public void GotoDetailsPage() => NavigationService.Navigate(typeof(Views.DetailPage), Value);
        public void GotoAddNew() =>
            NavigationService.Navigate(typeof(Views.NewContactPage));
        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}
