using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualPhaseCalculation
{
    /// <summary>
    /// Класс, реализующий компоненты диаграммы состояния
    /// </summary>
    public class Element : IElement
    {
        /// <summary>
        /// Название компонента
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Температура фазового перехода чистого компонента
        /// </summary>
        public double Ta_b { get; set; }

        /// <summary>
        /// Энтальпия фазового перехода чистого компонента
        /// </summary>
        public double dH { get; set; }

        /// <summary>
        /// Энтропия фазового перехода чистого компонента
        /// </summary>
        public double dS { get; set; }

        /// <summary>
        /// Изменение изобарической теплоемкости при фазовом переходе чистого компонента
        /// </summary>
        public double dCp { get; set; }

        /// <summary>
        /// Функция расчета потенциала Гиббса чистого компонента при заданной температуре
        /// </summary>
        /// <param name="T">Температура</param>
        /// <returns>Значение потенциала Гиббса чистого компонента при заданной температуре</returns>
        public double dG(double T)
        {
            if (dS != 0 && Ta_b != 0)
            {
                return (dH - T * dS) * (1 + dCp * (T / Ta_b - 1) / (2 * dS));
            }
            else
            {
                throw new Exception("Entropy or phase change temperature is zero. ");
            }
        }

        /// <summary>
        /// Функция расчета производной по температуре (энтропии смешения) потенциала Гиббса чистого компонента при заданной температуре
        /// </summary>
        /// <param name="T">Температура</param>
        /// <returns>Значение энтропии смешения чистого компонента при заданной температуре</returns>
        public double ddG(double T)
        {
            if (dS != 0 && Ta_b != 0)
            {
                return (-dS) * (1 + dCp / (2 * dS) * (T / Ta_b - 1))
                       + (dH - T * dS) * (dCp / 2 / dS / Ta_b);
            }
            else
            {
                throw new Exception("Entropy or phase change temperature is zero. ");
            }
        }
    }
}
