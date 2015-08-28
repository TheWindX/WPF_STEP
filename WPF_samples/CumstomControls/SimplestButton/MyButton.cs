using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Markup;
using System.Windows.Input; // [ContentProperty()]

namespace WpfApplication1
{
    
    public class MyButton : ContentControl
    {
        static MyButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyButton),
                new FrameworkPropertyMetadata(typeof(MyButton)));
        }

        public MyButton()
        {
            AddHandler(UIElement.MouseLeftButtonDownEvent, new RoutedEventHandler((sender, arg) => OnMouseLeftButtonDown1(sender, arg)), true);
        }

        public static RoutedEvent clickEvent = EventManager.RegisterRoutedEvent(
            "click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MyButton));

        // Provide CLR accessors for the event
        public event RoutedEventHandler click
        {
            add { AddHandler(clickEvent, value); }
            remove { clickEvent = null; }
        }

        // This method raises the Tap event
        void RaiseClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(MyButton.clickEvent);
            RaiseEvent(newEventArgs);
        }

        protected void OnMouseLeftButtonDown1(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(clickEvent);
            RaiseEvent(args);
        }


    }
}
