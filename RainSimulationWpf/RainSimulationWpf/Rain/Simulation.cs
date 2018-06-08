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

        private readonly Random _random = new Random();
        private readonly HashSet<Drop> _drops = new HashSet<Drop>();

        #endregion

        public Simulation(Land land)
        {
            if (land == null)
            {
                throw new ArgumentNullException(nameof(land));
            }
            Land = land;

            Initialize();
        }

        #region public methods

        public Land Land { get; }

        public IReadOnlyCollection<Drop> Drops
        {
            get { return _drops; }
        }

        public double Width { get; private set; }

        public double Height { get; private set; }

        public void Simulate(double time, double intensity)
        {
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

            for (int i = 0; i < Land.Regions.Count; ++i)
            {
                double excess = Land.Regions[i].Water / 2;

                double level = GetWaterLevel(i);
                double previousLevel = GetWaterLevel(i - 1);
                double nextLevel = GetWaterLevel(i + 1);

                if (level > previousLevel)
                {
                    previousLevel += excess;
                    level -= excess;

                    if (previousLevel > level)
                    {
                        double extra = (previousLevel - level) / 2;
                        previousLevel -= extra;
                        level += extra;
                    }
                }
                if (level > nextLevel)
                {
                    nextLevel += excess;
                    level -= excess;

                    if (nextLevel > level)
                    {
                        double extra = (nextLevel - level) / 2;
                        nextLevel -= extra;
                        level += extra;
                    }
                }

                SetWaterLevel(i, level);
                SetWaterLevel(i-1, previousLevel);
                SetWaterLevel(i+1, nextLevel);
            }

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
                    VelocityX = -0.005,
                    VelocityY = velocity
                });
            }
        }

        #endregion

        #region private methods

        private void Initialize()
        {
            Width = Land.Regions.Count();
            Height = 3 * Land.Regions.Max(region => region.Height) / 2;
        }

        private double GetWaterLevel(int landRegionIndex)
        {
            if ((landRegionIndex < 0) || (landRegionIndex > Land.Regions.Count - 1))
            {
                return 0;
            }

            LandRegion landRegion = Land.Regions[landRegionIndex];
            return landRegion.Height + landRegion.Water;
        }

        private void SetWaterLevel(int landRegionIndex, double level)
        {
            if ((landRegionIndex < 0) || (landRegionIndex > Land.Regions.Count - 1))
            {
                return;
            }

            LandRegion landRegion = Land.Regions[landRegionIndex];
            if (landRegion.Height >= 0)
            {
                landRegion.Water = Math.Max(0, level - landRegion.Height);
            }
        }

        #endregion
    }
}
