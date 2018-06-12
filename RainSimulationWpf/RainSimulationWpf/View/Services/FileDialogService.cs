using Microsoft.Win32;
using RainSimulationWpf.ViewModel.Services;

namespace RainSimulationWpf.View.Services
{
	internal class FileDialogService : IFileDialogService
	{
		#region IFileDialogService

		public string Open()
		{
			var openFileDialog = new OpenFileDialog();
			return (openFileDialog.ShowDialog() == true) 
				? openFileDialog.FileName 
				: string.Empty;
		}

		#endregion
	}
}
