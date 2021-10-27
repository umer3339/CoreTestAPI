using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Core.ViewModel.ResponseModel
{
    public class AdminPortalResponseModel
    {
        public string userName { get; set; }
        public string directory { get; set; }
        public List<Stage> stages { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Form
    {
        public bool accessLevel { get; set; }
        public object name { get; set; }
    }

    public class Dashboard
    {
        public bool accessLevel { get; set; }
        public string name { get; set; }
    }

    public class Report
    {
        public bool accessLevel { get; set; }
        public string name { get; set; }
    }

    public class SubStream
    {
        public string subStreamName { get; set; }
        public bool accessLevel { get; set; }
        public List<Form> form { get; set; }
        public List<Dashboard> dashboard { get; set; }
        public List<Report> report { get; set; }
    }

    public class MainStream
    {
        public string streamName { get; set; }
        public bool accessLevel { get; set; }
        public List<SubStream> subStream { get; set; }
    }

    public class Stage
    {
        public string stageName { get; set; }
        public bool accessLevel { get; set; }
        public List<MainStream> mainStream { get; set; }
    }
}
