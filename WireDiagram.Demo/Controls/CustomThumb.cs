using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using WireDiagram.Core.Model;
using WireDiagram.Core.Lists;
using WireDiagram.Core.Items;

namespace WireDiagram.Controls
{
    public class CustomThumb : Thumb
    {
        //bool IsSelected = false;

        public CustomThumb()
        {
            this.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(CustomThumb_PreviewMouseLeftButtonDown);
            this.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(CustomThumb_PreviewMouseLeftButtonUp);
        }

        void CustomThumb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext is Node && Keyboard.Modifiers == ModifierKeys.Control)
            {
                var node = this.DataContext as Node;

                if (node.IsSelected)
                {
                    if (node.Diagram.SelectedItems != null)
                    {
                        if (node.Diagram.SelectedItems.Contains(node))
                        {
                            node.Diagram.SelectedItems.Remove(node);
                            if (node.Diagram.SelectedItems.Count == 0)
                            {
                                node.Diagram.SelectedItems = null;
                            }
                        }
                    }

                    node.IsSelected = false;
                }
                else
                {
                    if (node.Diagram.SelectedItems == null)
                    {
                        node.Diagram.SelectedItems = new ItemList();
                    }

                    if (!node.Diagram.SelectedItems.Contains(node))
                    {
                        node.IsSelected = true;
                        node.Diagram.SelectedItems.Add(node);
                    }
                }
            }

            //var item = this.DataContext as Item;
            //if (item != null)
            //{
            //    IsSelected = item.IsSelected;
            //    item.IsSelected = true;
            //}
            //e.Handled = false;
        }

        void CustomThumb_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //var item = this.DataContext as Item;
            //if (item != null)
            //{
            //    item.IsSelected = IsSelected;
            //}

            //e.Handled = false;
        }

    }
}
