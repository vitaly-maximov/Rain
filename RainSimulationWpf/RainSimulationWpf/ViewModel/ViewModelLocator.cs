using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
using RainSimulationWpf.View.Services;
using RainSimulationWpf.ViewModel.Services;

namespace RainSimulationWpf.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    internal class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

	        SimpleIoc.Default.Register<IFileDialogService>(() => new FileDialogService());

			SimpleIoc.Default.Register<MainViewModel>();
        }
		
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        
        public static void Cleanup()
        {
        }
    }
}