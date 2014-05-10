using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

using WireDiagram.Core.Model;

namespace WireDiagram.Core.Lists
{
    [XmlRoot("Items"), XmlType("Items")]
    public class ItemList : ObservableCollection<Item>
    {

    }
}
