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

        public double Overflow { get; set; }

        #endregion
    }
}
