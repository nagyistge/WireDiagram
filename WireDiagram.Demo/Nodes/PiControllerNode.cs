using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WireDiagram.Core.Model;
using WireDiagram.Core.Items;
using WireDiagram.Core.Lists;

namespace WireDiagram.Nodes.Model
{
    public class PiControllerNode : Node
    {
        public override void CreateDefaultPins()
        {
            var points = new PinPointList() 
            {
                new PinPoint(0.5, 0.0, PinAlignment.Top), 
                new PinPoint(0.5, 1.5, PinAlignment.Bottom)
            };

            base.CreatePins(points);
        }
    }
}
