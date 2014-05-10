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

using WireDiagram.Core.Model;

namespace WireDiagram
{
    /// <summary>
    /// Interaction logic for TreeViewWindow.xaml
    /// </summary>
    public partial class TreeViewWindow : Window
    {
        public TreeViewWindow()
        {
            InitializeComponent();

            this.Closed += new EventHandler(TreeViewWindow_Closed);
        }

        void TreeViewWindow_Closed(object sender, EventArgs e)
        {
            if (previousItem != null)
            {
                previousItem.IsSelected = false;
            }
        }

        private Item previousItem = null;

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeView = sender as TreeView;

            if (treeView.SelectedItem is Item)
            {
                var item = treeView.SelectedItem as Item;

                if (previousItem != null)
                    previousItem.IsSelected = false;

                item.IsSelected = true;
                previousItem = item;
            }
            else
            {
                if (previousItem != null)
                {
                    previousItem.IsSelected = false;
                    previousItem = null;
                }
            }
        }
    }
}
