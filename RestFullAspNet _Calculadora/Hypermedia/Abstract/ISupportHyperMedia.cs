using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullAspNet.Hypermedia.Abstract
{
    interface ISupportHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
