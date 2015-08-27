using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ColorPicker;


namespace ColorControlApp
{
    #region Window1 CLASS
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : System.Windows.Window
    {
        #region Constructor
        /// <summary>
        /// Constructs the code behind part of the Window1 object
        /// and subscribes to the contained <see cref="ColorPicker.ColorPickerControl">
        /// ColorPickerControl </see> SelectionChanged and NewColor events
        /// </summary>
        public Window1()
        {
            //the resultant merging of the XAML file and this code behind will
            //end up in a file called Window1.g.cs in the \obj\Debug or \obj\release
            //directory, depends on how you run your project. Also of note when the 
            //code behind and XAML file get compiled is a file called Window1.baml 
            //("bammel"), thats a binary 
            //XAML file
            InitializeComponent();
            //look up the imported external control which has been placed 
            //on the main canvas, and subscribe to its SelectionChanged event
            //remember it was a ListBox subclass, so this event should be in
            //the custom control
            lstColorPicker.SelectionChanged += new SelectionChangedEventHandler(lstColorPicker_SelectionChanged);
            lstColorPicker.NewColor += new RoutedEventHandler(lstColorPicker_NewColor);
            lstColorPicker.NewColorCustom += new ColorPicker.ColorPickerControl.NewColorCustomEventHandler(lstColorPicker_NewColorCustom);


        }
        #endregion
        #region Subscribed Events
        /// <summary>
        /// A user defined RoutedEvent within the  <see cref="ColorPicker.ColorPickerControl">
        /// ColorPickerControl</see> that simply uses custom ColorRoutedEventArgs event args
        /// </summary>
        /// <param name="sender">the ColorPickerControl</param>
        /// <param name="e">custom ColorRoutedEventArgs event args</param>
        private void lstColorPicker_NewColorCustom(object sender, ColorRoutedEventArgs e)
        {
            MessageBox.Show(e.ColorName);
        }
        
        /// <summary>
        /// A user defined RoutedEvent within the  <see cref="ColorPicker.ColorPickerControl">
        /// ColorPickerControl</see> that simply uses the standard RoutedEventArgs event args
        /// </summary>
        /// <param name="sender">the ColorPickerControl</param>
        /// <param name="e">standard RoutedEventArgs event args</param>
        private void lstColorPicker_NewColor(object sender, RoutedEventArgs e)
        {
            //get a reference to the source object and cast it to the correct type
            ColorPicker.ColorPickerControl lstSrc = (sender as ColorPicker.ColorPickerControl);
            //get the tooltip and show it as MessageBox
            ToolTip t = (ToolTip)(lstSrc.SelectedItem as Rectangle).ToolTip;
            this.lblColor.Content = t.Content;
        }

        /// <summary>
        /// raised when the selection change occurs in the source control
        /// </summary>
        /// <param name="sender">the source control</param>
        /// <param name="e">the event args</param>
        private  void lstColorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get a reference to the source object and cast it to the correct type
            ColorPicker.ColorPickerControl lstSrc = (e.Source as ColorPicker.ColorPickerControl);
            //set the background to the sources selected value, remember the SelectedValue
            //was set as Fill, which in fact returns a Brush. See the ColorPickerControl.cs class
            this.Background = (Brush)lstSrc.SelectedValue;
        }
        #endregion

    }
    #endregion
}