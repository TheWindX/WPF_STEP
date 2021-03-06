using System;
using System.Windows;
using System.Windows.Media;

namespace VideoText
{
    public partial class Window1 : Window
    {     
        public Window1()
        {
            InitializeComponent();   

            // Create the text as FormattedText object.
            FormattedText formattedText = new FormattedText(
                "FABLE",
                new System.Globalization.CultureInfo("en-US"),
                FlowDirection.LeftToRight,
                new Typeface(
                    new System.Windows.Media.FontFamily("Segoe UI"),
                    FontStyles.Normal,
                    FontWeights.Bold,
                    FontStretches.Normal),
                120,
                System.Windows.Media.Brushes.Red);

            // Build a geometry out of the text.
            Geometry geometry = new PathGeometry();
            geometry = formattedText.BuildGeometry(new System.Windows.Point(0, 0));

            // Adjust the geometry to fit the aspect ration of the video by scaling it.
            ScaleTransform myScaleTransform = new ScaleTransform();
            myScaleTransform.ScaleX = .85;
            myScaleTransform.ScaleY = 2.0;
            geometry.Transform = myScaleTransform;

            // Flatten the geometry and create a PathGeometry out of it.
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry = geometry.GetFlattenedPathGeometry();

            // Use the PathGeometry for the empty placeholder Path element in XAML.
            path.Data = pathGeometry;

            // Use the PathGeometry to clip the video.
            myMediaElement.Clip = pathGeometry;

            // Use the PathGeometry for the animated ball that follows the path of the text outline.
            matrixAnimation.PathGeometry = pathGeometry;  
        }

        // The handler for the MediaFailed event is invoked when MediaElement detects an error.
        public void OnMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            string exceptionString = e.ErrorException.Message + " : " + e.ErrorException.InnerException.Message;
        }
    }
}