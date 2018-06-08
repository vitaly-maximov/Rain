using GalaSoft.MvvmLight;
using RainSimulationWpf.Rain;
using System.Threading.Tasks;

namespace RainSimulationWpf.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        #region fields

        private Simulation _simulation;

        #endregion

        public MainViewModel()
        {
            //_simulation = new Simulation(new double[] { 5, 4, 3, 2, 1, 2, 3, 4, 5 }, new double[] { 0, 1, 2, 3, 4, 3, 2, 1, 0 });
            //_simulation.Render(2);
            //_simulation.Render(2);
            //_simulation.Render(2);
            //_simulation.Render(2);
            //Simulation = _simulation;

            //Start();

            Simulation = new Simulation(new Land(new[] {
                new LandRegion(3),
                new LandRegion(2),
                new LandRegion(5),
                new LandRegion(8),
                new LandRegion(2),
                new LandRegion(4),
                new LandRegion(3),
                new LandRegion(9),
                new LandRegion(2),
                new LandRegion(4),
                new LandRegion(7),
                new LandRegion(3),
                new LandRegion(2),
                new LandRegion(2),
                new LandRegion(2),
                new LandRegion(-1),
                new LandRegion(5),
                new LandRegion(8),
                new LandRegion(2),
                new LandRegion(4),
                new LandRegion(3),
                new LandRegion(9),
                new LandRegion(2),
                new LandRegion(4),
                new LandRegion(7),
            }));
        }

        #region public methods

        public Simulation Simulation { get; set; }

        #endregion
    }
}