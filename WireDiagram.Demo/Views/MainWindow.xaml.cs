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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.Windows.Controls.Primitives;

using WireDiagram.Core.Model;
using WireDiagram.Core.Items;
using WireDiagram.Core.Lists;
using WireDiagram.Nodes.Model;

namespace WireDiagram
{
    public partial class MainWindow : Window
    {
        #region Properties

        private double GridSize = UnitConverter.CmToDip(0.5);
        private double GridSizeCreate = UnitConverter.CmToDip(1.0);
        private double GridOffsetLeft = UnitConverter.CmToDip(0.0);
        private double GridOffsetTop = UnitConverter.CmToDip(0.0);

        private Point dragStartPoint;

        #endregion

        #region MainWindow

        public MainWindow()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);

            InitializeDiagram();
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    {
                        var diagram = this.DataContext as Diagram;
                        diagram.RemoveSelected();
                    }
                    break;
                case Key.Left:
                    {
                        Diagram diagram = this.DataContext as Diagram;
                        MoveNodes(diagram.SnapToGrid ? -GridSize : -1.0, 0.0, diagram);
                    }
                    break;
                case Key.Right:
                    {
                        Diagram diagram = this.DataContext as Diagram;
                        MoveNodes(diagram.SnapToGrid ? GridSize : 1.0, 0.0, diagram);
                    }
                    break;
                case Key.Up:
                    {
                        Diagram diagram = this.DataContext as Diagram;
                        MoveNodes(0.0, diagram.SnapToGrid ? -GridSize : -1.0, diagram);
                    }
                    break;
                case Key.Down:
                    {
                        Diagram diagram = this.DataContext as Diagram;
                        MoveNodes(0.0, diagram.SnapToGrid ? GridSize : 1.0, diagram);
                    }
                    break;
            }
        }

        #endregion

        #region Initialize Diagram

        static Func<PinPointList, PinList> GetNodePins = (points) =>
        {
            PinList list = new PinList();
            foreach (var p in points)
            {
                list.Add(new Pin()
                {
                    Id = Guid.NewGuid(),
                    OffsetX = UnitConverter.CmToDip(p.X),
                    OffsetY = UnitConverter.CmToDip(p.Y)
                });
            }
            return list;
        };

        static Func<double, double, PinPointList, Node> GetNode = (x, y, points) =>
        {
            return new Node()
            {
                Id = Guid.NewGuid(),
                X = UnitConverter.CmToDip(x),
                Y = UnitConverter.CmToDip(y),
                Z = 1,
                Pins = GetNodePins(points)
            };
        };

        static Func<double, double, Pin, Pin, Wire> GetWire = (x, y, startPin, endPin) =>
        {
            return new Wire()
            {
                Id = Guid.NewGuid(),
                X = UnitConverter.CmToDip(x),
                Y = UnitConverter.CmToDip(y),
                Z = 0,
                StartPin = startPin,
                EndPin = endPin
            };
        };

        private void InitializeDiagram()
        {
            BackgroundWorker bw = new BackgroundWorker();

            bw.DoWork += (_sender, _e) =>
            {
                #region New Diagram

                // create diagram
	            Diagram diagram = new Diagram()
	            {
	                Id = Guid.NewGuid(),
	                Width = UnitConverter.CmToDip(42.0),
	                Height = UnitConverter.CmToDip(29.7),
	                Items = new ItemList(),
                    SnapToGrid = true,
                    ShowGrid = true,
                    PreviewSelection = true
	            };

                /*
                var pinPoints = new PinPointList() 
                { 
                    new PinPoint(0.0, 0.5), 
                    new PinPoint(1.0, 0.5),
                    new PinPoint(0.5, 0.0), 
                    new PinPoint(0.5, 1.0) 
                };

                Node[] nodes = new Node[4];
                nodes[0] = GetNode(6.0, 2.0, pinPoints);
                nodes[1] = GetNode(10.0, 2.0, pinPoints);
                nodes[2] = GetNode(6.0, 6.0, pinPoints);
                nodes[3] = GetNode(2.0, 2.0, pinPoints);

                foreach(var node in nodes)
                    diagram.Items.Add(node);

                Wire[] wires = new Wire[3];
                wires[0] = GetWire(0.0, 0.0, nodes[0].Pins[1], nodes[1].Pins[0]);
                wires[1] = GetWire(0.0, 0.0, nodes[0].Pins[3], nodes[2].Pins[2]);
                wires[2] = GetWire(0.0, 0.0, nodes[0].Pins[0], nodes[3].Pins[1]);

                foreach (var wire in wires)
                {
                    diagram.Items.Add(wire);
                }

                // nodes

                var andGateNode = new AndGateNode()
                {
                    Id = Guid.NewGuid(),
                    X = UnitConverter.CmToDip(14.0),
                    Y = UnitConverter.CmToDip(2.0),
                    Z = 1
                };
                andGateNode.CreateDefaultPins();
                diagram.Items.Add(andGateNode);

                var orGateNode = new OrGateNode()
                {
                    Id = Guid.NewGuid(),
                    X = UnitConverter.CmToDip(14.0),
                    Y = UnitConverter.CmToDip(3.5),
                    Z = 1
                };
                orGateNode.CreateDefaultPins();
                diagram.Items.Add(orGateNode);
                */

                /*
                Pin pin = new Pin()
                {
                    Id = Guid.Empty,
                    ParentId = Guid.Empty,
                    OffsetX = 0.0,
                    OffsetY = 0.0,
                    X = UnitConverter.CmToDip(12.0),
                    Y = UnitConverter.CmToDip(2.5),
                    Z= 1
                };

                diagram.Items.Add(pin);
                */

                diagram.Update(diagram);

                #endregion

                _e.Result = diagram;
            };

            bw.RunWorkerCompleted += (_sender, _e) =>
            {
            	if(_e.Result != null)
            	{
                	// diagram => DataContext
                	this.DataContext = _e.Result as Diagram;
            	}
            };

            bw.RunWorkerAsync();
        }

        #endregion

        #region Serialize/Deserialize Diagram

        XmlSerializer GetDiagramSerializer()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Diagram),
                new Type[] 
                {
                    typeof(Wire),
                    typeof(Pin),
                    typeof(Node), 
                    typeof(AndGateNode),
                    typeof(OrGateNode),
                    typeof(InverterNode),
                    typeof(PulseNode),
                    typeof(PulseGeneratorNode),
                    typeof(TimerOffDelayNode),
                    typeof(TimerOnDelayNode),
                    typeof(MemoryResetPriorityNode),
                    typeof(MemorySetPriorityNode),
                    typeof(LimiterNode),
                    typeof(LowerLimitNode),
                    typeof(HigherLimitNode),
                    typeof(DeadBandNode),
                    typeof(MultiplierNode),
                    typeof(PiControllerNode),
                    typeof(PidControllerNode),
                    typeof(SignalSwitchNode),
                    typeof(SignalTransmitterNode),
                    typeof(MinimumSelectorNode),
                    typeof(MaximumSelectorNode)
                });

            return serializer;
        }

        Diagram Open()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog()
                {
                    FileName = "",
                    Filter = "All Files (*.*)|*.*|Xml Files (*.xml)|*.xml",
                    FilterIndex = 2
                };

                if (dlg.ShowDialog() == true)
                {
                    using (System.IO.TextReader reader = new System.IO.StreamReader(dlg.FileName))
                    {
                        XmlSerializer serializer = GetDiagramSerializer();

                        Diagram diagram = new Diagram();
                        diagram = (Diagram)serializer.Deserialize(reader);

                        if (diagram != null)
                        {
                            diagram.Update(diagram);
                        }
                        
                        return diagram;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }

            return null;
        }

        void Save(Diagram diagram)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog()
                {
                    FileName = "Diagram",
                    Filter = "All Files (*.*)|*.*|Xml Files (*.xml)|*.xml",
                    FilterIndex = 2
                };

                if (dlg.ShowDialog() == true)
                {
                    using (System.IO.TextWriter writer = new System.IO.StreamWriter(dlg.FileName))
                    {
                        XmlSerializer serializer = GetDiagramSerializer();

                        serializer.Serialize(writer, diagram);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        #endregion

        #region Thumb Events

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
			var node = (sender as FrameworkElement).DataContext as Node;
			if (node == null)
				return;

            Diagram diagram = this.DataContext as Diagram;

            if (diagram.SelectedItems != null && node.IsSelected == true)
            {
                if (diagram.SelectedItems.Count == 0)
                {
                    MoveNode(e.HorizontalChange, e.VerticalChange, node, diagram);
                }
                else
                {
                    MoveNodes(e.HorizontalChange, e.VerticalChange, diagram);
                }
            }
            else
            {
                MoveNode(e.HorizontalChange, e.VerticalChange, node, diagram);
            }

            e.Handled = true;
        }

        private void MoveNodes(double x, double y, Diagram diagram)
        {
            if (diagram.SelectedItems != null)
            {
                if (diagram.SelectedItems.Count > 0)
                {
                    var nodes = from n in diagram.SelectedItems where n is Node select n as Node;

                    foreach (var n in nodes)
                    {
                        MoveNode(x, y, n, diagram);
                    }
                }
            }
        }

        private void MoveNode(double x, double y, Node node, Diagram diagram)
        {
            if (diagram.SnapToGrid)
            {
                node.X = SnapGrid.Snap(node.X + x, GridSize, GridOffsetLeft);
                node.Y = SnapGrid.Snap(node.Y + y, GridSize, GridOffsetTop);
            }
            else
            {
                node.X += x;
                node.Y += y;
            }
        }

        #endregion

        #region Menu Events

        private void MenuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();

            bw.DoWork += (_sender, _e) =>
            {
                try
                {
                    _e.Result = Open();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print(ex.Message);
                    _e.Result = null;
                }
            };

            bw.RunWorkerCompleted += (_sender, _e) =>
            {
                if (_e.Result != null)
                {
                    // diagram => DataContext
                    this.DataContext = _e.Result as Diagram;
                }

                this.Focus();
            };

            bw.RunWorkerAsync();
        }

        private void MenuFileSave_Click(object sender, RoutedEventArgs e)
        {
            Diagram diagram = this.DataContext as Diagram;
            BackgroundWorker bw = new BackgroundWorker();

            bw.DoWork += (_sender, _e) =>
            {
                try
                {
                    Save(diagram);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print(ex.Message);
                    _e.Result = null;
                }
            };

            bw.RunWorkerCompleted += (_sender, _e) =>
            {
                this.Focus();
            };

            bw.RunWorkerAsync();
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuDiagramNew_Click(object sender, RoutedEventArgs e)
        {
            InitializeDiagram();
        }

        private void MenuDiagramClear_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as Diagram).RemoveAllItems();
        }

        private void MenuDiagramStats_Click(object sender, RoutedEventArgs e)
        {
            Diagram diagram = this.DataContext as Diagram;

            int pins = 0;
            foreach (var n in diagram.Nodes)
                pins += n.Pins.Count();

            MessageBox.Show(string.Format("Items: {0}\nNodes: {1} (Pins: {2})\nWires: {3}\nPins: {4}",
                diagram.Items.Count(),
                diagram.Nodes.Count(),
                pins,
                diagram.Wires.Count(),
                diagram.Pins.Count()),
                "Diagram Stats",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void MenuDiagramTree_Click(object sender, RoutedEventArgs e)
        {
            TreeViewWindow window = new TreeViewWindow();
            window.DataContext = this.DataContext as Diagram;
            window.Owner = this;
            window.Show();
        }

        private void MenuDiagramPreviewXml_Click(object sender, RoutedEventArgs e)
        {
            Diagram diagram = this.DataContext as Diagram;

            try
            {
                StringBuilder sb = new StringBuilder();
                using (System.IO.TextWriter writer = new System.IO.StringWriter(sb))
                {
                    XmlSerializer serializer = GetDiagramSerializer();

                    serializer.Serialize(writer, diagram);

                    XmlPreviewWindow window = new XmlPreviewWindow();
                    window.Owner = this;
                    window.Text = sb.ToString();

                    window.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        #endregion

        #region Drag & Drop

        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;

            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindVisualParent<T>(parentObject);
            }
        }

        private void WrapPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragStartPoint = e.GetPosition(null);
        }

        private void WrapPanel_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = dragStartPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                var panel = sender as StackPanel;
                UserControl control = FindVisualParent<UserControl>((DependencyObject)e.OriginalSource);
                if (control != null)
                {
                    string type = control.GetType().Name;
                    DataObject dragData = new DataObject("ControlTypeFormat", type);
                    DragDrop.DoDragDrop(control, dragData, DragDropEffects.Move);
                }
            } 
        }

        private void ItemsControl_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("ControlTypeFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ItemsControl_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("ControlTypeFormat"))
            {
                string type = e.Data.GetData("ControlTypeFormat") as string;

                var parent = (ItemsControl)sender;
                var diagram = this.DataContext as Diagram;
                Point p = e.GetPosition(parent);
                if (diagram.SnapToGrid)
                {
                    p = new Point()
                    {
                        X = SnapGrid.Snap(p.X, GridSize, GridOffsetLeft),
                        Y = SnapGrid.Snap(p.Y, GridSize, GridOffsetTop)
                    };
                }

                DiagramAddDefaultItem(type, diagram, p);
            }
        }

        private void DiagramAddDefaultItem(string type, Diagram diagram, Point p)
        {
            switch (type)
            {
                case "AndGate":
                    {
                        var node = new AndGateNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "OrGate":
                    {
                        var node = new OrGateNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "Inverter":
                    {
                        var node = new InverterNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "Pulse":
                    {
                        var node = new PulseNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "PulseGenerator":
                    {
                        var node = new PulseGeneratorNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "TimerOffDelay":
                    {
                        var node = new TimerOffDelayNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "TimerOnDelay":
                    {
                        var node = new TimerOnDelayNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "MemoryResetPriority":
                    {
                        var node = new MemoryResetPriorityNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "MemorySetPriority":
                    {
                        var node = new MemorySetPriorityNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "Limiter":
                    {
                        var node = new LimiterNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "LowerLimit":
                    {
                        var node = new LowerLimitNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "HigherLimit":
                    {
                        var node = new HigherLimitNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "DeadBand":
                    {
                        var node = new DeadBandNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "Multiplier":
                    {
                        var node = new MultiplierNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "PiController":
                    {
                        var node = new PiControllerNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "PidController":
                    {
                        var node = new PidControllerNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "SignalSwitch":
                    {
                        var node = new SignalSwitchNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "SignalTransmitter":
                    {
                        var node = new SignalTransmitterNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "MinimumSelector":
                    {
                        var node = new MinimumSelectorNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                case "MaximumSelector":
                    {
                        var node = new MaximumSelectorNode()
                        {
                            Id = Guid.NewGuid(),
                            X = p.X,
                            Y = p.Y,
                            Z = 1
                        };

                        node.CreateDefaultPins();
                        node.Update(diagram);

                        diagram.Items.Add(node);
                    }
                    break;
                default:
                    {

                    }
                    break;
            }
        }

        #endregion
    }
}
