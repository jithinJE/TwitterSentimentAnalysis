using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Model
{
    class AzureResponseRootObject
    {
        public List<SentimentResponse> documents { get; set; }
        public List<object> errors { get; set; }
    }
}
