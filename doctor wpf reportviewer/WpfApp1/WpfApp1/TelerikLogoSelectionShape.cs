using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.RadialMenu;
using Telerik.Windows.Controls.RadialMenu.Commands;
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging.FormatProviders;
using Telerik.Windows.Media.Imaging.ImageEditorCommands;
using Telerik.Windows.Media.Imaging.Shapes;
using Path = System.Windows.Shapes.Path;

namespace WpfApp1
{



    












    class TelerikLogoSelectionShape : IShape
    {






    





















        public string DisplayName
        {
            get
            {
                return "Telerik";
            }
        }

        public Geometry GetShapeGeometry()
        {
            PathFigure outer = new PathFigure();
            outer.IsClosed = true;
            outer.StartPoint = new Point(0, 2.5);
            outer.Segments.Add(new LineSegment() { Point = new Point(2.5, 0) });
            outer.Segments.Add(new LineSegment() { Point = new Point(5, 2.5) });
            outer.Segments.Add(new LineSegment() { Point = new Point(7.5, 0) });
            outer.Segments.Add(new LineSegment() { Point = new Point(10, 2.5) });
            outer.Segments.Add(new LineSegment() { Point = new Point(9, 3.5) });
            outer.Segments.Add(new LineSegment() { Point = new Point(7.5, 2) });
            outer.Segments.Add(new LineSegment() { Point = new Point(6, 3.5) });
            outer.Segments.Add(new LineSegment() { Point = new Point(8.5, 6) });
            outer.Segments.Add(new LineSegment() { Point = new Point(5, 9.5) });
            outer.Segments.Add(new LineSegment() { Point = new Point(1.5, 6) });
            outer.Segments.Add(new LineSegment() { Point = new Point(4, 3.5) });
            outer.Segments.Add(new LineSegment() { Point = new Point(2.5, 2) });
            outer.Segments.Add(new LineSegment() { Point = new Point(1, 3.5) });

            PathFigure inner = new PathFigure();
            inner.StartPoint = new Point(3.5, 6);
            inner.IsClosed = true;
            inner.Segments.Add(new LineSegment() { Point = new Point(5, 7.5) });
            inner.Segments.Add(new LineSegment() { Point = new Point(6.5, 6) });
            inner.Segments.Add(new LineSegment() { Point = new Point(5, 4.5) });

            PathGeometry logoGeometry = new PathGeometry();
            logoGeometry.Figures.Add(inner);
            logoGeometry.Figures.Add(outer);

            return logoGeometry;
        }
    }





    public class CommandsViewModel : ViewModelBase
    {
        private double normalScale = 0.0;

        public ICommand SetFrameColorCommand { get; private set; }
        public ICommand NavigateCommand { get; private set; }
        public ICommand ZoomInCommand { get; private set; }
        public ICommand ZoomOutCommand { get; private set; }
        public ICommand SetSymbolCommand { get; private set; }
        public ICommand ResetToDefaultCommand { get; private set; }

        public CommandsViewModel()
        {
            this.NavigateCommand = new DelegateCommand(this.OnNavigateExecuted);
            this.SetFrameColorCommand = new DelegateCommand(this.OnSetFrameColorExecuted);
            this.ZoomInCommand = new DelegateCommand(this.OnZoomInExecuted, this.CanExecuteZoomInCommand);
            this.ZoomOutCommand = new DelegateCommand(this.ZoomOutCommandExecuted, this.CanExecuteZoomOutCommand);
            this.SetSymbolCommand = new DelegateCommand(this.SetSymbolCommandExecuted, this.CanExecuteSetSymbolCommand);
            this.ResetToDefaultCommand = new DelegateCommand(this.ResetToDefaultCommandExecuted);
        }

        private void ResetToDefaultCommandExecuted(object obj)
        {
            var menuItem = obj as RadRadialMenuItem;
            var imageEditor = menuItem.Menu.TargetElement as RadImageEditor;
            imageEditor.ScaleFactor = normalScale;
        }

        private void SetSymbolCommandExecuted(object obj)
        {
            var menuItem = obj as RadRadialMenuItem;
            var imageEditor = menuItem.Menu.TargetElement as RadImageEditor;
            var parentGrid = imageEditor.Parent as Grid;

            if (parentGrid != null)
            {
                var menuItemPathData = (menuItem.IconContent as Path).Data as Geometry;
                if (menuItemPathData != null)
                {
                    var symbolHolderStackPanel = VisualTreeHelper.GetChild(parentGrid, 2) as StackPanel;
                    var path = symbolHolderStackPanel.Children.OfType<Path>().FirstOrDefault((p) => FindMatchingPath(menuItemPathData, p));

                    // Invert the visibility of the Path.
                    path.Visibility = path.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                }
            }
        }

        private static bool FindMatchingPath(Geometry menuItemPathData, Path p)
        {
            return p.Data.ToString() == menuItemPathData.ToString();
        }

        private bool CanExecuteSetSymbolCommand(object obj)
        {
            var menuItem = obj as RadRadialMenuItem;
            if (menuItem != null && menuItem.Menu != null)
            {
                var imageEditor = menuItem.Menu.TargetElement as RadImageEditor;
                if (imageEditor != null)
                {
                    var parentGrid = imageEditor.Parent as Grid;

                    if (parentGrid != null)
                    {
                        if (menuItem.IconContent is Path)
                        {
                            var symbolHolderStackPanel = VisualTreeHelper.GetChild(parentGrid, 2) as StackPanel;
                            return symbolHolderStackPanel != null;
                        }
                    }
                }
            }
            return false;
        }

        private void ZoomOutCommandExecuted(object obj)
        {
            var menuItem = obj as RadRadialMenuItem;
            var imageEditor = menuItem.Menu.TargetElement as RadImageEditor;

            if (this.normalScale == 0.0) this.normalScale = imageEditor.ActualScaleFactor;
            if (imageEditor.ScaleFactor == 0.0) imageEditor.ScaleFactor = imageEditor.ActualScaleFactor;

            imageEditor.ScaleFactor = imageEditor.ActualScaleFactor - 0.1;
        }

        private bool CanExecuteZoomOutCommand(object obj)
        {
            var menuItem = obj as RadRadialMenuItem;
            if (menuItem != null && menuItem.Menu != null)
            {
                var imageEditor = menuItem.Menu.TargetElement as RadImageEditor;

                if (imageEditor != null)
                {
                    return imageEditor.ActualScaleFactor >= 0.4;
                }
            }
            return false;
        }

        private void OnZoomInExecuted(object obj)
        {
            var menuItem = obj as RadRadialMenuItem;
            var imageEditor = menuItem.Menu.TargetElement as RadImageEditor;
            if (this.normalScale == 0.0) this.normalScale = imageEditor.ActualScaleFactor;

            imageEditor.ScaleFactor = imageEditor.ActualScaleFactor + 0.1;
        }

        private bool CanExecuteZoomInCommand(object obj)
        {
            var menuItem = obj as RadRadialMenuItem;
            if (menuItem != null && menuItem.Menu != null)
            {
                var imageEditor = menuItem.Menu.TargetElement as RadImageEditor;
                if (imageEditor != null)
                {
                    return imageEditor.ScaleFactor <= 1.2;
                }
            }

            return false;
        }

        private void OnNavigateExecuted(object obj)
        {
            var menuItem = obj as RadRadialMenuItem;
            NavigateContext context = new NavigateContext(menuItem);
            menuItem.Menu.CommandService.ExecuteDefaultCommand(CommandId.NavigateToView, context);
        }

        private void OnSetFrameColorExecuted(object obj)
        {
            var menuItem = obj as RadRadialMenuItem;
            var color = menuItem.ContentSectorBackground;

            var borderElement = VisualTreeHelper.GetChild(menuItem.Menu.TargetElement.Parent, 1) as Border;
            if (borderElement != null)
            {
                borderElement.BorderBrush = color;
            }
        }
    }



    public static class HandledEscapeKeyBehavior
    {
        public static bool GetHandleEscapeKey(DependencyObject obj)
        {
            return (bool)obj.GetValue(HandleEscapeKeyProperty);
        }

        public static void SetHandleEscapeKey(DependencyObject obj, bool value)
        {
            obj.SetValue(HandleEscapeKeyProperty, value);
        }

        public static readonly DependencyProperty HandleEscapeKeyProperty =
            DependencyProperty.RegisterAttached("HandleEscapeKey", typeof(bool), typeof(HandledEscapeKeyBehavior), new PropertyMetadata(OnHandleEscapeKey));

        private static void OnHandleEscapeKey(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;
            if (element != null && (bool)e.NewValue)
            {
                element.AddHandler(FrameworkElement.KeyDownEvent, new KeyEventHandler(Element_KeyDown), true);
                var userControl = element.ParentOfType<UserControl>();
                if (userControl != null)
                {
                    userControl.AddHandler(UserControl.KeyDownEvent, new KeyEventHandler(Element_KeyDown), true);
                }
            }
        }

        private static void Element_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                RadialMenuCommands.Hide.Execute(null, sender as UIElement);
            }
        }
    }


    public class RadialMenuProvider
    {
        public RadRadialMenu Menu { get; set; }
    }


}








