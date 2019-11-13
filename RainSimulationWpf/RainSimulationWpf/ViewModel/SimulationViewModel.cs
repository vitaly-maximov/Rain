using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using RainSimulationWpf.Rain;

namespace RainSimulationWpf.ViewModel
{
    internal class SimulationViewModel : ViewModelBase
    {
		#region const

	    private const int c_fps = 50;

		#endregion

		#region public methods

		public event EventHandler SimulationUpdated;

		public Simulation Simulation { get; set; }

	    public bool IsRainy { get; set; } = true;

	    public double Intensity { get; set; } = 1;

	    public double Incline { get; set; } = 0;

		#endregion

		#region private methods

	    private async void Update(Simulation simulation, double simulationTime)
	    {
		    if (simulation != Simulation)
		    {
			    return;
		    }

		    var startTime = DateTime.Now;

		    await Task.Run(() => simulation.Simulate(simulationTime, Intensity, IsRainy, Incline));
			RaiseSimulationUpdated();
			
			await Task.Delay(1000 / c_fps);

			Update(simulation, (DateTime.Now - startTime).TotalMilliseconds);
	    }

	    private void OnSimulationChanged()
	    {
		    if (Simulation != null)
		    {
				Update(Simulation, 0);
		    }
	    }

	    private void RaiseSimulationUpdated()
	    {
			SimulationUpdated?.Invoke(this, EventArgs.Empty);
	    }

		#endregion
	}
}
