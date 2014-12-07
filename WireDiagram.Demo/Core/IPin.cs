using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WireDiagram.Core.Model
{
    public interface IPin
    {
        double OffsetX { get; set; }
        double OffsetY { get; set; }
    }
}
