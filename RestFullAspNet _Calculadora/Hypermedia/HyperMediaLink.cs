using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFullAspNet.Hypermedia
{
    public class HyperMediaLink
    {
        private string href;
        public string Href {
            get
            {
                object _look = new object();
                lock (_look)
                {
                    StringBuilder sb = new StringBuilder(href);
                    return sb.Replace("%2F", "/").ToString();
                }
            }
            set { href = value; } }
        public string Rel { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
    }
}
