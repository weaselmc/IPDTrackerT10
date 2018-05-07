using IPDTracker.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace IPDTracker.Services.DataServices
{
    public static class FileHelper
    {
        public static string filename = "ipdstore.xml";

        public static async Task<ObservableCollection<Billable>> GetBillablesAsync()
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Billable>));
            Stream s = await file.OpenStreamForReadAsync();
            ObservableCollection<Billable> items;
            try
            {
                items = (ObservableCollection<Billable>)serializer.Deserialize(s);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return new ObservableCollection<Billable>();
            }
            return items;
        }

        public static async Task SetBillablesAsync(ObservableCollection<Billable> items)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Billable>));
            Stream s = await file.OpenStreamForWriteAsync();
            try
            {
                using (s)
                    serializer.Serialize(s, items);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            await Task.CompletedTask;
        }
    }
}
