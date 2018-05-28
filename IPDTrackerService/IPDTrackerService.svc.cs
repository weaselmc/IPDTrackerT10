using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace IPDTrackerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class IPDTrackerService : IIPDTrackerService
    {
        private static string path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "ipdstore.xml");
        List<BillingEntry> Entries = new List<BillingEntry>();
        IPDTrackerService()
        {
            LoadList();
        }
        public bool CreateEntry(BillingEntry entry)
        {
            try
            {
                Entries.Add(entry);
                SaveList();
                return true;
            }
            catch { return false; }
        }

        public bool DeleteEntry(Guid Id)
        {
            try
            {
                BillingEntry entry = Entries.First(i => i.BillableId == Id);
                Entries.Remove(entry);
                SaveList();
                return true;
            }
            catch { return false; }
        }        

        public List<BillingEntry> GetEntries()
        {
            return Entries;
        }

        public BillingEntry GetEntry(Guid Id)
        {
            BillingEntry entry = Entries.First(i => i.BillableId == Id);
            return entry;
        }

        public bool UpdateEntry(BillingEntry entry)
        {
            try
            {
                BillingEntry oldentry = Entries.First(i => i.BillableId == entry.BillableId);
                oldentry.BillableDate = entry.BillableDate;
                oldentry.BillableTime = entry.BillableTime;
                oldentry.ClientName = entry.ClientName;
                oldentry.Notes = entry.Notes;
                SaveList();
                return true;
            }
            catch { return false; }
        }

        void SaveList()
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(List<BillingEntry>));
            FileStream fs = new FileStream(path, FileMode.Truncate);
            try
            {
                using (fs)
                    //foreach (BillingEntry entry in Entries)
                        serializer.WriteObject(fs, Entries); //.Serialize(fs, Entries);
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        void LoadList()
        {
            //File file = ApplicationData.Current.LocalFolder.CreateFileAsync(path, CreationCollisionOption.OpenIfExists);
            DataContractSerializer serializer = new DataContractSerializer(typeof(List<BillingEntry>));
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            //Stream s = await fs. .OpenStreamForReadAsync();            
            try
            {
                using (fs)
                {
                    while (fs.CanRead)
                    {
                        Entries = (List<BillingEntry>)serializer.ReadObject(fs);
                    }
                }            
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                //Entries = new List<BillingEntry>();
            }
        }

    }
}
