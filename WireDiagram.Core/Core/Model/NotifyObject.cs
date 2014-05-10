using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace WireDiagram.Core.Model
{
    [XmlRoot("Notify"), XmlType("Notify")]
    public abstract class NotifyObject : INotifyPropertyChanged
    {
        public virtual void Notify(string propertyName)
        {
        	var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
