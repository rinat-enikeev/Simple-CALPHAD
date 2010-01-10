using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualPhaseCalculation
{
    public struct Azeotrope
    {

        /// <summary>
        /// Temperature of azeotrope point
        /// </summary>
        public double temperature;

        /// <summary>
        /// Coordinate of azeotrope point
        /// </summary>
        public double coordinate;

        public Azeotrope(double temp, double coord)
        {
            this.temperature = temp;
            this.coordinate = coord;
        }
    }

    public struct ExperimentalPoint
    {
        /// <summary>
        /// Temperature of some experimental point on phase diagram
        /// </summary>
        public double temperature;

        /// <summary>
        /// Liquidus point coordinate for given temperature
        /// </summary>
        public double liquidusCoordinate;


        /// <summary>
        /// Solidus point for given temperature
        /// </summary>
        public double solidusCoordinate;

        public ExperimentalPoint(double temp, double liq, double sol)
        {
            this.temperature = temp;
            this.liquidusCoordinate = liq;
            this.solidusCoordinate = sol;
        }
    }

    public interface IBinarySystem
    {
        /// <summary>
        /// Left component on phase diagram
        /// </summary>
        IElement leftElement { get; }

        /// <summary>
        /// Right component on phase diagram
        /// </summary>
        IElement rightElement { get; }


        /// <summary>
        /// Azeotrope point
        /// </summary>
        Azeotrope azeotrope { get; }

        ExperimentalPoint experimentalPoint { get; }

    }
}
