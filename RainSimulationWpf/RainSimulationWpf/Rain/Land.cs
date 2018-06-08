using System;
using System.Collections.Generic;
using System.Linq;

namespace RainSimulationWpf.Rain
{
    internal class Land
    {
        public Land(IEnumerable<LandRegion> regions)
        {
            if (regions == null)
            {
                throw new ArgumentNullException(nameof(regions));
            }
            Regions = regions.ToArray();
        }

        #region public methods

        public IReadOnlyList<LandRegion> Regions { get; }

        #endregion
    }
}
