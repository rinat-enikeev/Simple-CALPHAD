using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualPhaseCalculation
{
    class BinarySystem : IBinarySystem
    {
        public IElement leftElement { get; set; }
        public IElement rightElement { get; set; }
        public Azeotrope azeotrope { get; set; }
        public ExperimentalPoint experimentalPoint { get; set; }
    }
}
