using RainSimulationWpf.Rain;
using RainSimulationWpf.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RainSimulationWpf.View
{
    /// <summary>
    /// Interaction logic for SimulationSurfaceView.xaml
    /// </summary>
    public partial class SimulationSurfaceView : UserControl
    {
        public SimulationSurfaceView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
	        if (DataContext is SimulationViewModel simulation)
	        {
		        simulation.SimulationUpdated += OnSimulationUpdated;
	        }
        }

	    private void OnSimulationUpdated(object sender, EventArgs e)
	    {
		    Draw((sender as SimulationViewModel)?.Simulation);
	    }

        private void Draw(Simulation simulation)
        {
            if (_surfaceImage.Source != null)
            {
                return;
            }

            var group = new DrawingGroup();
            using (DrawingContext context = group.Open())
            {
                var surfaceRect = new Rect(0, 0, ActualWidth, ActualHeight);

                context.PushClip(new RectangleGeometry(surfaceRect));

                context.DrawRectangle(
                    Brushes.White, 
                    null, 
                    surfaceRect);

                double xRatio = ActualWidth / simulation.Width;
                double yRatio = ActualHeight / simulation.Height;

                

                Pen pen = new Pen
                {
                    Brush = Brushes.Blue,

                    //Thickness = 2
                };

                foreach (Drop drop in simulation.Drops)
                {
                    

                    double x = xRatio * drop.PositionX;
                    double y = ActualHeight - yRatio * drop.PositionY;

                    context.DrawLine(pen, new Point(x, y), new Point(x - ActualWidth * drop.VelocityX * drop.Volume, y - 5 * ActualHeight / 300 * drop.Volume));
                }

                for (int i = 0; i < simulation.Land.Regions.Count; ++i)
                {
                    LandRegion landRegion = simulation.Land.Regions[i];

                    double landRegionWidth = xRatio;
                    double landRegionHeight = yRatio * landRegion.Height;

                    if (landRegionHeight > 0)
                    {
                        var landRegionRect = new Rect(
                            x: i * landRegionWidth,
                            y: ActualHeight - landRegionHeight,
                            width: landRegionWidth,
                            height: landRegionHeight);


                        context.DrawRectangle(Brushes.Black, null, landRegionRect);
                    }

                    double landRegionWaterHeight = yRatio * landRegion.Water;

                    var landRegionWaterRect = new Rect(
                        x: i * landRegionWidth,
                        y: ActualHeight - landRegionHeight - landRegionWaterHeight,
                        width: landRegionWidth,
                        height: landRegionWaterHeight);

                    context.DrawRectangle(Brushes.DarkBlue, null, landRegionWaterRect);
				}
            }
            group.Freeze();
            _surfaceImage.Source = new DrawingImage(group);
        }
    }
}
