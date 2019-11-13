using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainSimulationWpf.Rain
{
    internal class Simulation
    {
        #region fields

		private readonly LandRegion _outsideRegion = new LandRegion(-1);

        private readonly Random _random = new Random();
        private readonly HashSet<Drop> _drops = new HashSet<Drop>();

        #endregion

        public Simulation(double[] heights)
        {
            if (heights == null)
            {
                throw new ArgumentNullException(nameof(heights));
            }

            Land = new Land(heights.Select(height => new LandRegion(height)));

	        Width = Land.Regions.Count;
	        Height = 3 * Land.Regions.Max(region => region.Height) / 2;
        }

        #region public methods

	    public double Width { get; }

        public double Height { get; }

	    public Land Land { get; }

		public IReadOnlyCollection<Drop> Drops => _drops;
		
		public void Simulate(double time, double intensity, bool isRainy, double incline)
        {
	        foreach (Drop drop in _drops)
	        {
		        drop.VelocityX = incline * 0.005;
	        }

			foreach (Drop drop in _drops)
            {
                drop.PositionX += drop.VelocityX * time;
                drop.PositionY -= drop.VelocityY * time;
            }

            _drops.RemoveWhere(drop => drop.PositionX < 0 || drop.PositionX >= Width);

            Drop[] dropsToRemove = _drops.Where(drop => drop.PositionY < Land.Regions[(int)drop.PositionX].Height).ToArray();

            foreach(Drop drop in dropsToRemove)
            {
                _drops.Remove(drop);

                if (Land.Regions[(int)drop.PositionX].Height >= 0)
                {
                    Land.Regions[(int)drop.PositionX].Water += drop.Volume * 0.002;
                }
            }

            for (int regionIndex = 0; regionIndex < Land.Regions.Count; ++regionIndex)
            {
	            LandRegion currentRegion = GetRegion(regionIndex);
				LandRegion previousRegion = GetRegion(regionIndex - 1);
	            LandRegion nextRegion = GetRegion(regionIndex + 1);

	            double flowToPrevious = GetFlow(currentRegion, previousRegion);
	            double flowToNext = GetFlow(currentRegion, nextRegion);

				currentRegion.Take(flowToPrevious + flowToNext);
	            previousRegion.Add(flowToPrevious);
	            nextRegion.Add(flowToNext);
			}

			if (isRainy)
			{ 
				int newDropsCount = (int) (time * intensity * _random.NextDouble());
				for (int i = 0; i < newDropsCount; ++i)
				{
					double velocity = 0.005 + 0.01 * _random.NextDouble();
					double volume = 1 + _random.NextDouble();

					double x = Width * _random.NextDouble();
					double y = Height - velocity * time * _random.NextDouble();

					_drops.Add(new Drop
					{
						PositionX = x,
						PositionY = y,
						Volume = volume,
						VelocityX = incline * 0.005,
						VelocityY = velocity
					});
				}
				}
        }

        #endregion

        #region private methods
		
	    private LandRegion GetRegion(int regionIndex)
	    {
		    return ((regionIndex < 0) || (regionIndex > Land.Regions.Count - 1)) 
			    ? _outsideRegion 
			    : Land.Regions[regionIndex];
	    }

	    private double GetFlow(LandRegion from, LandRegion to)
	    {
		    return 0.5 * Math.Max(0, from.GetWaterLevel() - Math.Max(to.GetWaterLevel(), from.Height));
		}

        #endregion
    }
}
