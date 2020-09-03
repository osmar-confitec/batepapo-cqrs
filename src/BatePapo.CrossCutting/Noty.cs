using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace BatePapo.CrossCutting
{
    public enum Priority
    {

        Hight = 1,
        Average = 2,
        Low = 3

    }

    public enum Layer
    {
        App = 1,
        Domain = 2,
        Repository = 3,
        Others = 4
    }
    public enum NotyType
    {
        Alert = 1,
        Error = 2,
        Success = 3,
        Information = 4
    }

    public enum NotyIntention
    {





    }
    [NotMapped]
    public class Noty
    {



        public Priority Priority { get; set; }

        public Layer? Layer { get; set; }

        public NotyType NotyType { get; set; }

        public string Message { get; set; }

        public NotyIntention? NotyIntention { get; set; }

        public List<string> ErrorProperties { get; set; }

        public Noty()
        {
            Priority = Priority.Average;
            NotyType = NotyType.Error;
            ErrorProperties = new List<string>();
        }


        public override string ToString()
        {
            return Message;
        }

    }


    public class LNoty : List<Noty>
    {

        new public void Add(Noty item) {


            if ( item?.NotyIntention != null && this.Any(x => x.NotyIntention != null && x.NotyIntention == item.NotyIntention))
                return;

            base.Add(item);
        
        }

        new public void AddRange(IEnumerable<Noty> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


        public bool HaveErros { get { return this.Any(x => x.NotyType == NotyType.Error); } }

        public bool HaveAlerts { get { return this.Any(x => x.NotyType == NotyType.Alert); } }

        public bool HaveSucess { get { return this.Any(x => x.NotyType == NotyType.Success); } }

        public bool HaveInformations { get { return this.Any(x => x.NotyType == NotyType.Information); } }

        public bool HaveNotifications { get { return this.Any(); } }

    }
}
