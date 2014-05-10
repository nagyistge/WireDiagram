using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

using WireDiagram.Core.Items;

namespace WireDiagram.Core.Lists
{
    [XmlRoot("Nodes"), XmlType("Nodes")]
    public class NodeList : ObservableCollection<Node>
    {

    }
}
