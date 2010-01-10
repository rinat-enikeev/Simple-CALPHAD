using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualPhaseCalculation
{
    public interface IElement
    {
        string id { get; }

        double Ta_b { get; }

        /// <summary>
        /// Функция расчета потенциала Гиббса чистого компонента при заданной температуре
        /// </summary>
        /// <param name="T">Температура</param>
        /// <returns>Значение потенциала Гиббса чистого компонента при заданной температуре</returns>
        double dG(double T);

        /// <summary>
        /// Функция расчета производной по температуре (энтропии смешения) потенциала Гиббса чистого компонента при заданной температуре
        /// </summary>
        /// <param name="T">Температура</param>
        /// <returns>Значение энтропии смешения чистого компонента при заданной температуре</returns>
        double ddG(double T);
    }


}
