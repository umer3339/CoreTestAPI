using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace STCAPI.Core.ViewModel.RequestModel
{
    public class CustomDeashbordVm
    {
        public List<string> EmailIds { get; set; }
        public List<IFormFile> Attachment { get; set; }
        public string Property { get; set; }
        public string Category { get; set; }
    }
}
