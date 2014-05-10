using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WireDiagram.Core.Model;
using WireDiagram.Core.Items;
using WireDiagram.Core.Lists;

namespace WireDiagram.Nodes.Model
{
    public class LimiterNode : Node
    {
        public override void CreateDefaultPins()
        {
            var points = new PinPointList() 
            {
                new PinPoint(0.0, 0.5, PinAlignment.Left), 
                new PinPoint(1.0, 0.5, PinAlignment.Right),
                new PinPoint(0.5, 0.0, PinAlignment.Top), 
                new PinPoint(0.5, 1.0, PinAlignment.Bottom),
            };

            base.CreatePins(points);
        }
    }
}
