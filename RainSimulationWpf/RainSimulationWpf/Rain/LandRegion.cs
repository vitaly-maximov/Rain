using System;

namespace RainSimulationWpf.Rain
{
    internal class LandRegion
    {
        public LandRegion(double height)
        {
            Height = height;
        }

        #region public methods

        public double Height { get; }

        public double Water { get; set; }

	    public double GetWaterLevel()
	    {
		    return (Height < 0) 
			    ? 0 
			    : Height + Water;
	    }

	    public void Add(double water)
	    {
		    Water += (Height < 0)
			    ? 0
			    : water;
	    }

	    public void Take(double water)
	    {
		    Water = Math.Max(0, Water - water);
	    }

        #endregion
    }
}
