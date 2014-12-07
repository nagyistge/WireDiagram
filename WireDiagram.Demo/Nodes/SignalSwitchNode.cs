using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WireDiagram.Core.Model;
using WireDiagram.Core.Items;
using WireDiagram.Core.Lists;

namespace WireDiagram.Nodes.Model
{
    public class SignalSwitchNode : Node
    {
        public override void CreateDefaultPins()
        {
            var points = new PinPointList() 
            {
                new PinPoint(0.25, 0.0, PinAlignment.Top), 
                new PinPoint(0.75, 0.0, PinAlignment.Top),
                new PinPoint(0.5, 1.0, PinAlignment.Bottom),
                new PinPoint(0.0, 0.5, PinAlignment.Left)
            };

            base.CreatePins(points);
        }
    }
}
