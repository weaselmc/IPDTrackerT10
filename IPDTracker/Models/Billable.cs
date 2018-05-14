using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace IPDTracker.Models
{
    public class Billable : INotifyPropertyChanged
    {
        public Guid BillableId { get; set; }
        private string clientname;
        public string ClientName
        {
            get
            {
                return clientname;
            }
            set
            {
                if (value != clientname)
                {
                    clientname = value;
                    OnPropertyChanged("ClientName");
                }
            }
        }
        private string notes;
        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                if (value != notes)
                {
                    notes = value;
                    OnPropertyChanged("Notes");
                }
            }
        }
        private DateTime billabledate = DateTime.Now;

        //[XmlIgnore]
        public DateTime BillableDate //Offset
        {
            get
            {
                return billabledate;  //DateTime.SpecifyKind(billabledate, DateTimeKind.Local);
            }
            set
            {
                if (value != billabledate) //.Date
                {
                    billabledate = value; //.Date
                    OnPropertyChanged("BillableDate");
                    //OnPropertyChanged("BillableDateString");
                }
            }
        }

        //[XmlElement(ElementName = "BillableDate")]
        //public string BillableDateString
        //{
        //    get { return BillableDate.ToString("yyyy-MM-ddTHH:mm:ss.fffffffzzz"); }
        //    set { BillableDate = DateTimeOffset.Parse(value); }
        //}


        //public string BillableDateString
        //{
        //    get
        //    {
        //        return billabledate.ToString("dd-MMM-yyyy");
        //    }
        //}
        private TimeSpan billabletime;
        public TimeSpan BillableTime
        {
            get
            {
                return billabletime;
            }
            set
            {
                if (value != billabletime)
                {
                    billabletime = value;
                    OnPropertyChanged("BillableTime");
                    //OnPropertyChanged("BillableTimeString");
                }
            }
        }
        //public string BillableTimeString
        //{
        //    get
        //    {
        //        return new DateTime(billabletime.Ticks).ToString("hh:mm");
        //    }
        //}

        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
