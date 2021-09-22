using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailHelper
{
    public record JournalDetail
    {
        public string Source { get; set; }
        public string Period { get; set; }
        public string Batch { get; set; }
        public string Journal { get; set; }
        public string Category { get; set; }
        public string LineNumber { get; set; }
        public string Account { get; set; }
        public string CodeCombination { get; set; }
        public string CR { get; set; }
        public string DR { get; set; }
        public string LineDescription { get; set; }
        public string HeaderLineKey { get; set; }
        
    }
}
