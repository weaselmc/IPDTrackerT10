using System;
using IPDTracker.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

namespace IPDTracker.Views
{
    public sealed partial class ContactsPage : Page
    {
        
        public ContactsPage()
        {
            InitializeComponent();            
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
    }
}