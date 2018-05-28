using IPDTracker.IPDTrackerServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPDTracker.Services.DataServices
{
    static class DataHelper
    {

        static public IPDTrackerServiceReference.IPDTrackerServiceClient client = new IPDTrackerServiceReference.IPDTrackerServiceClient();

        public static async Task<ObservableCollection<BillingEntry>> GetBillablesAsync()
        {
            if (client.State != System.ServiceModel.CommunicationState.Opened)
                await client.OpenAsync();
            ObservableCollection<BillingEntry> entries = await client.GetEntriesAsync();
            await client.CloseAsync();
            return entries;
        }

        public static async Task<bool> CreateBillableAsync(BillingEntry entry)
        {

            if (client.State != System.ServiceModel.CommunicationState.Opened)
                await client.OpenAsync();
            bool result = await client.CreateEntryAsync(entry);
            await client.CloseAsync();
            return result;
        }

        public static async Task<bool> UpdateBillableAsync(BillingEntry entry)
        {

            if (client.State != System.ServiceModel.CommunicationState.Opened)
                await client.OpenAsync();
            bool result = await client.UpdateEntryAsync(entry);
            await client.CloseAsync();
            return result;
        }

        public static async Task<bool> DeleteBillableAsync(BillingEntry entry)
        {
            if (client.State != System.ServiceModel.CommunicationState.Opened)
                await client.OpenAsync();
            bool result = await client.DeleteEntryAsync(entry.BillableId);
            await client.CloseAsync();
            return result;
        }
    }
}
