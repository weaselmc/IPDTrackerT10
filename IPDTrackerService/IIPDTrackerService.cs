using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IPDTrackerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IIPDTrackerService
    {

        [OperationContract]
        List<BillingEntry> GetEntries();
        [OperationContract]
        BillingEntry GetEntry(Guid Id);
        [OperationContract]
        bool DeleteEntry(Guid Id);
        [OperationContract]
        bool UpdateEntry(BillingEntry entry);
        [OperationContract]
        bool CreateEntry(BillingEntry entry);

        // TODO: Add your service operations here
    }

        // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class BillingEntry
    {
        [DataMember]
        public Guid BillableId { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public DateTime BillableDate { get; set; }
        
        //[XmlIgnore]
        [DataMember ]
        public TimeSpan BillableTime { get; set; }

        //[DataMember]
        //[XmlElement(DataType = "duration", ElementName = "BillableTime")]
        //private string BillableTimeString
        //{
        //    get { return XmlConvert.ToString(BillableTime); }
        //    set
        //    {
        //        BillableTime = string.IsNullOrEmpty(value) ?
        //  TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
        //    }
        //}

    }
}
