using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace WireDiagram
{
    /// <summary>
    /// Interaction logic for XmlPreviewWindow.xaml
    /// </summary>
    public partial class XmlPreviewWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public virtual void Notify(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        private string text;
        public string Text 
        {
            get
            {
                return text;
            }
            set
            {
                if (value != text)
                {
                    text = value;
                    Notify("Text");
                }
            }
        }

        #endregion

        public XmlPreviewWindow()
        {
            InitializeComponent();

            this.DataContext = this;
        }
    }
}
