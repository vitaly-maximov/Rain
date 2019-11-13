using System;
using System.Windows.Input;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RainSimulationWpf.Rain;
using RainSimulationWpf.ViewModel.Services;

namespace RainSimulationWpf.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Simulation = new SimulationViewModel();

			Simulation.Simulation = new Simulation(
				new double[] { 3, 2, 5, 8, 2, 4, 3, 9, 2, 4, 7, 3, 2, 2, 2, -1, 5, 8, 2, 4, 3, 9, 2, 4, 7 });

			OpenMapCommand = new RelayCommand(OpenMap);
			ToggleRainCommand = new RelayCommand(ToggleRain);
			IncreaseRainCommand = new RelayCommand(IncreaseRain);
			DecreaseRainCommand = new RelayCommand(DecreaseRain);
	        InclineLeftRainCommand = new RelayCommand(InclineLeftRain);
	        InclineRightRainCommand = new RelayCommand(InclineRightRain);
		}

        #region public methods

        public SimulationViewModel Simulation { get; private set; }

		public ICommand OpenMapCommand { get; }

		public ICommand ToggleRainCommand { get; }

		public ICommand IncreaseRainCommand { get; }

		public ICommand DecreaseRainCommand { get; }

	    public ICommand InclineLeftRainCommand { get; }

	    public ICommand InclineRightRainCommand { get; }

		#endregion

		#region private methods

		private void OpenMap()
	    {
		    var fileDialogService = ServiceLocator.Current.GetInstance<IFileDialogService>();

		    string filename = fileDialogService.Open();
		    if (string.IsNullOrEmpty(filename))
		    {
			    return;
		    }

		    double[] map = MapReader.Read(filename);
			Simulation.Simulation = new Simulation(map);
	    }

	    private void ToggleRain()
	    {
		    Simulation.IsRainy = !Simulation.IsRainy;
	    }

	    private void IncreaseRain()
	    {
		    Simulation.Intensity = Math.Min(2, Simulation.Intensity + 0.1);
	    }

	    private void DecreaseRain()
	    {
		    Simulation.Intensity = Math.Max(0, Simulation.Intensity - 0.1);
		}

	    private void InclineLeftRain()
	    {
		    Simulation.Incline = Math.Max(-1, Simulation.Incline - 0.1);
	    }

	    private void InclineRightRain()
	    {
		    Simulation.Incline = Math.Min(1, Simulation.Incline + 0.1);
	    }

		#endregion
	}
}