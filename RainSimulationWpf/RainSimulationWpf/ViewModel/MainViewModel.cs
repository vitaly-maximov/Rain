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
            Simulation = new Simulation(
	            new double[] { 3, 2, 5, 8, 2, 4, 3, 9, 2, 4, 7, 3, 2, 2, 2, -1, 5, 8, 2, 4, 3, 9, 2, 4, 7 });

		    OpenMapCommand = new RelayCommand(OpenMap);
		}

        #region public methods

        public Simulation Simulation { get; private set; }

		public ICommand OpenMapCommand { get; }

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
			Simulation = new Simulation(map);
	    }

		#endregion
	}
}