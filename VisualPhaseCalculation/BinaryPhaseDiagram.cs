using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualPhaseCalculation
{
    /// <summary>
    /// Класс бинарной диаграммы состояния 
    /// </summary>
    public class BinaryPhaseDiagram
    {
        public IBinarySystem system { get; private set; }

        #region Accuracy
        /// <summary>
        /// Входная погрешность по Т
        /// </summary>
        const double deltaT = 0.005;

        /// <summary>
        /// Входная погрешность по dG
        /// </summary>
        const double deltaG = 5e-8;

        /// <summary>
        /// Входная точность по х
        /// </summary>
        const double epsilon = 0.00000001;

        #endregion

        #region Calculated arrays
        public double[] TArr;       
        public double[] xLRightArr;
        public double[] xjRightArr;
        public double[] xLLeftArr;
        public double[] xjLeftArr;
        public double[] xArr;
        public double[] dGjjLArr;
        public double[] dGjjjArr;
        public double[] ddGjjLArr;
        public double[] ddGjjjArr;
        public double[] zjLRightArr;
        public double[] zjLLeftArr;
        #endregion

        #region Useful coefficients. See report for more details (only in russian). 
        private double c0;
        private double c1;
        private double a0;
        private double a1;
        #endregion

        /// <summary>
        /// Binary phase diagram constructor.
        /// </summary>
        /// <param name="system">Binary system to calphad</param>
        public BinaryPhaseDiagram(IBinarySystem system)
        {
            this.system = system;  
 
            // Расчет коэффициентов c0 и с1 в приближении модели субрегулярных растворов
            this.c0 = -2 * (system.leftElement.dG(system.azeotrope.temperature) 
                        / system.azeotrope.coordinate) 
                      - system.rightElement.dG(system.azeotrope.temperature) 
                        * (1 - 2 * system.azeotrope.coordinate) 
                        / (Math.Pow((1 - system.azeotrope.coordinate), 2));

            this.c1 = system.leftElement.dG(system.azeotrope.temperature) 
                        / Math.Pow(system.azeotrope.coordinate, 2) 
                      - system.rightElement.dG(system.azeotrope.temperature) 
                        / Math.Pow((1 - system.azeotrope.coordinate), 2);

            // Вспомогательные переменные
            double a11 = (Math.Pow(system.experimentalPoint.solidusCoordinate, 2) 
                           - Math.Pow(system.experimentalPoint.liquidusCoordinate, 2)) 
                          / (Constants.R * system.experimentalPoint.temperature);

            double a12 = (2 * Math.Pow(system.experimentalPoint.solidusCoordinate, 3) 
                            - Math.Pow(system.experimentalPoint.solidusCoordinate, 2) 
                              - Math.Pow(system.experimentalPoint.liquidusCoordinate, 2) 
                                + 2 * Math.Pow(system.experimentalPoint.liquidusCoordinate, 3)) 
                          / (Constants.R * system.experimentalPoint.temperature);

            double a21 = (2 * system.experimentalPoint.liquidusCoordinate 
                            - 2 * system.experimentalPoint.solidusCoordinate) 
                          / (Constants.R * system.experimentalPoint.temperature);

            double a22 = (2 * system.experimentalPoint.solidusCoordinate 
                            - 3 * Math.Pow(system.experimentalPoint.solidusCoordinate, 2) 
                                - 2 * system.experimentalPoint.liquidusCoordinate 
                                    + 3 * Math.Pow(system.experimentalPoint.liquidusCoordinate, 2)) 
                          / (Constants.R * system.experimentalPoint.temperature);

            double b1 = Math.Log((1 - system.experimentalPoint.liquidusCoordinate) 
                                    / (1 - system.experimentalPoint.solidusCoordinate)) 
                          + (system.leftElement.dG(system.experimentalPoint.temperature) 
                               - Math.Pow(system.experimentalPoint.liquidusCoordinate, 2) * (c0 - c1) 
                                 + 2 * Math.Pow(system.experimentalPoint.liquidusCoordinate, 3) * c1) 
                             / (Constants.R * system.experimentalPoint.temperature);

            double b2 = Math.Log((system.experimentalPoint.liquidusCoordinate 
                                   * (1 - system.experimentalPoint.solidusCoordinate)) 
                                   / (system.experimentalPoint.solidusCoordinate 
                                   * (1 - system.experimentalPoint.liquidusCoordinate))) 
                          + (system.rightElement.dG(system.experimentalPoint.temperature) 
                               - system.leftElement.dG(system.experimentalPoint.temperature) 
                                 + c0 + 2 * c1 * system.experimentalPoint.liquidusCoordinate 
                                   - 2 * c0 * system.experimentalPoint.liquidusCoordinate 
                                     - 3 * Math.Pow(system.experimentalPoint.liquidusCoordinate, 2) * c1) 
                             / (Constants.R * system.experimentalPoint.temperature);

            // Расчет коэффициентов a0 и a1 в приближении модели субрегулярных растворов
            this.a0 = (b1 * a22 - a12 * b2) / (a11 * a22 - a21 * a12);
            this.a1 = (b2 * a11 - b1 * a21) / (a11 * a22 - a21 * a12);
        }

        /// <summary>
        /// Расчет нормированного потенциала Гиббса смешения при переходе через границу L/L+BCC
        /// </summary>
        /// <param name="T">Температура</param>
        /// <param name="x">Концентрация правого компонента</param>
        /// <returns>Значение нормированного потенциала Гиббса смешения при переходе через границу L/L+BCC</returns>
        private double dGjjL(double T, double x)
        {
            return ((1 - x) * system.leftElement.dG(T) / Constants.R / T 
                    + x * system.rightElement.dG(T) / Constants.R / T + x * Math.Log(x)
                    + (1 - x) * Math.Log(1 - x) + x * (1 - x) * (c0 + a0 + x * (c1 + a1)) / Constants.R / T);
        }

        /// <summary>
        /// Расчет нормированного потенциала Гиббса смешения при переходе через границу BCC/L+BCC
        /// </summary>
        /// <param name="T">Температура</param>
        /// <param name="x">Концентрация правого компонента</param>
        /// <returns>Значение нормированного потенциала Гиббса смешения при переходе через границу BCC/L+BCC</returns>
        private double dGjjj(double T, double x)
        {
            return (x * Math.Log(x) + (1 - x) * Math.Log(1 - x) + x * (1 - x) * (a0 + x * a1) / Constants.R / T);
        }

        /// <summary>
        /// Расчет первой производной нормированного потенциала Гиббса смешения при переходе через границу L/L+BCC
        /// </summary>
        /// <param name="T">Температура</param>
        /// <param name="x">Концентрация правого компонента</param>
        /// <returns>Значение первой производной нормированного потенциала Гиббса смешения при переходе через границу L/L+BCC</returns>
        private double ddGjjL(double T, double x)
        {
            return - system.leftElement.dG(T) / Constants.R / T 
                   + system.rightElement.dG(T) / Constants.R / T 
                   + Math.Log(x) - Math.Log(1 - x)
                   + (a0 + c0 + 2 * x * (a1 + c1) 
                   - 2 * x * (a0 + c0) 
                   - 3 * x * x * (a1 + c1)) / Constants.R / T;
        }

        /// <summary>
        /// Расчет первой производной нормированного потенциала гиббса при переходе через границу BCC/L+BCC
        /// </summary>
        /// <param name="T">Температура</param>
        /// <param name="x">Концентрация правого компонента</param>
        /// <returns>Значение первой производной нормированного потенциала гиббса при переходе через границу BCC/L+BCC</returns>
        private double ddGjjj(double T, double x)
        {
            return Math.Log(x) - Math.Log(1 - x) 
                   + (a0 + 2 * x * a1 - 2 * x * a0 - 3 * x * x * a1) / Constants.R / T;
        }

        /// <summary>
        /// Расчет второй производной нормированного потенциала Гиббса смешения при переходе через границу BCC/L+BCC
        /// </summary>
        /// <param name="T">Температура</param>
        /// <param name="x">Концентрация правого компонента</param>
        /// <returns>Значение второй производной нормированного потенциала Гиббса смешения при переходе через границу BCC/L+BCC</returns>
        private double d2dGjjj(double T, double x)
        {
            return 1 / ((1 - x) * x) + (2 * a1 - 2 * a0 - 6 * x * a1) / Constants.R / T;
        }

        /// <summary>
        /// Расчет второй производной нормированного потенциала Гиббса смешения при переходе через границу BCC/L+BCC
        /// </summary>
        /// <param name="T">Температура</param>
        /// <param name="x">Концентрация правого компонента</param>
        /// <returns>Значение второй производной нормированного потенциала Гиббса смешения при переходе через границу BCC/L+BCC</returns>
        private double d2dGjjL(double T, double x)
        {
            return 1 / ((1 - x) * x) + (2 * (a1 + c1) - 2 * (a0 + c0) - 6 * x * (a1 + c1)) / Constants.R / T;
        }

        /// <summary>
        /// Расчет погрешности x в зависимости от шага по T
        /// </summary>
        /// <param name="deltaT">Входная абсолютная погрешность по T</param>
        /// <param name="T">Температура</param>
        /// <param name="x">Концентрация правого компонента</param>
        /// <returns>Значение абсолютной погрешности по х</returns>
        private double dX(double deltaT, double T, double x)
        {
            double dfT = (1 - x) * system.leftElement.ddG(T) + x * system.rightElement.ddG(T);
            double dfx = -system.leftElement.dG(T) + system.rightElement.dG(T) + (1 - 2 * x) * c0 + x * (2 - 3 * x) * c1;
            return -dfT / dfx * deltaT;
        }

        /// <summary>
        /// Расчет изменения нормированного потенциала Гиббса смешения при переходе BCC/L по модели субрегулярных растворов
        /// </summary>
        /// <param name="T">Температура</param>
        /// <param name="x">Концентрация правого компонента</param>
        /// <returns></returns>
        private double dGj_L(double T, double x)
        {
            return ((1 - x) * system.leftElement.dG(T) + x * system.rightElement.dG(T) + x * (1 - x) * (c0 + x * c1)) 
                    / Constants.R / T;
        }

        /// <summary>
        /// Нахождение корня уравнения методом дихотомии
        /// </summary>
        /// <param name="xLeft">Левая граница по х</param>
        /// <param name="xRight">Правая граница по х</param>
        /// <param name="T">Температура</param>
        /// <returns>Корень для функции</returns>
        public double dichotomySolutionZjL(double xLeft, double xRight, double T)
        {
            // Переменная для записи предыдущего значения
            double xPrev;

            // Расчетная погрешность по х
            double deltaX;

            // Результат
            double result;

            xPrev = xRight;

            for (; ; )	//Цикл дихотомии
            {
                result = (xLeft + xRight) / 2;

                deltaX = Math.Abs(dX(deltaT, T, result));

                if (Math.Abs(xPrev - result) < deltaX) //условие выхода
                {
                    break;
                }
                if (dGj_L(T, result) < 0)
                {
                    xLeft = result;
                }
                else
                {
                    xRight = result;
                }

                xPrev = result;
            }

            return result;
        }

        /// <summary>
        /// Заполняет массивы G(x) и dG(x)
        /// </summary>
        /// <param name="T">Температура</param>
        /// <param name="xmin">Левая граница по концентрации</param>
        /// <param name="xmax">Правая граница по концентрации</param>
        /// <param name="xstep">Шаг по концентрации</param>
        public void CalculateGX(double T, double xmin, double xmax, double xstep)
        {
            int arrIterator = (int)((xmax - xmin) / xstep) + 1;
            xArr = new double[arrIterator];
            dGjjLArr = new double[arrIterator];
            dGjjjArr = new double[arrIterator];
            ddGjjLArr = new double[arrIterator];
            ddGjjjArr = new double[arrIterator];

            arrIterator = 0;

            do
            {
                xArr[arrIterator] = xmin;
                dGjjjArr[arrIterator] = dGjjj(T, xmin);
                dGjjLArr[arrIterator] = dGjjL(T, xmin);
                ddGjjjArr[arrIterator] = ddGjjj(T, xmin);
                ddGjjLArr[arrIterator] = ddGjjL(T, xmin);
                xmin = xmin + xstep;
                arrIterator++;

            } while (xmin < xmax);
        }

        private int calcRightPart(double TStep, int arrIndex) 
        {
            // Переменные концентраций 
            double zjL = 1;
            double deltaZ;

            // Переменные по x
            double xVar;
            double xL1;
            double xL2;
            double xj1;
            double xj2;
            double deltaxL1;
            double deltaxL2;
            double deltaxj1;
            double deltaxj2;

            // Левая граница по концентрации
            double xLeft;
            // Правая граница по концентрации
            double xRight;

            // ОДЗ по К
            double Q;
            double q;
            double deltaQ;
            double deltaq;

            // ОДЗ по х
            double xL;
            double deltaxL;
            double xj;
            double deltaxj;

            // Переменные по К1, К2, w1
            double K1;
            double K2;
            double w1;

            // Переменные для временного хранения значений при проверке условий выхода из цикла по k
            double temp_ddGjjL;
            double temp_ddGjjj;
            double temp_dGjjL;
            double temp_dGjjj;

            // Цикл для правой части
            for (double T = system.rightElement.Ta_b - TStep; T > system.azeotrope.temperature; T = T - TStep)
            {
                //Задаем границы расчетного интервала
                xLeft = system.azeotrope.coordinate;
                xRight = 1 - epsilon;

                // Методом дихотомии вычисляем 
                zjL = dichotomySolutionZjL(xLeft, xRight, T);

                // Заполняем массив по zjL
                zjLRightArr[arrIndex] = zjL;

                // Выбираем минимальную погрешность по zj и zL
                deltaZ = Math.Min(Math.Abs(deltaG / ddGjjj(T, zjL)), Math.Abs(deltaG / ddGjjL(T, zjL)));

                // Определяем ОДЗ по k
                Q = ddGjjL(T, zjL - deltaZ);
                deltaQ = d2dGjjL(T, zjL) * deltaZ;
                q = ddGjjj(T, zjL + deltaZ);
                deltaq = d2dGjjj(T, zjL) * deltaZ;

                // Находим ОДЗ по x: xL
                xLeft = system.azeotrope.coordinate + epsilon;
                xRight = zjL - deltaZ;

                do
                {
                    xVar = (xLeft + xRight) / 2;

                    if ((ddGjjL(T, xVar) - (q + deltaq)) < 0)
                    {
                        xLeft = xVar;
                    }
                    else
                    {
                        xRight = xVar;
                    }
                } while (Math.Abs(Math.Abs(ddGjjL(T, xVar)) - Math.Abs(q + deltaq)) > Math.Abs(deltaG / d2dGjjL(T, xVar))); //условие выхода
                xL = xVar;
                deltaxL = deltaG / d2dGjjL(T, xL);

                // Находим ОДЗ по x: xj
                xLeft = zjL + deltaZ;
                xRight = 1 - epsilon;
                do
                {
                    xVar = (xLeft + xRight) / 2;

                    if ((ddGjjj(T, xVar) - (Q - deltaQ)) < 0)
                    {
                        xLeft = xVar;
                    }
                    else
                    {
                        xRight = xVar;
                    }
                } while (Math.Abs(Math.Abs(ddGjjj(T, xVar)) - Math.Abs(Q - deltaQ)) > Math.Abs(deltaG / d2dGjjj(T, xVar))); //условие выхода
                xj = xVar;
                deltaxj = deltaG / d2dGjjj(T, xj);

                // Первое приближение по К в дихотомии
                K1 = (Q + q) / 2;

                // Находим xL1
                xLeft = xL + deltaxL;
                xRight = zjL - deltaZ;
                do
                {
                    xVar = (xLeft + xRight) / 2;

                    if ((ddGjjL(T, xVar) - K1) > 0)
                    {
                        xRight = xVar;
                    }
                    else
                    {
                        xLeft = xVar;
                    }
                } while (Math.Abs(Math.Abs(ddGjjL(T, xVar)) - Math.Abs(K1)) > Math.Abs(deltaG / d2dGjjL(T, xVar))); //условие выхода
                xL1 = xVar;
                deltaxL1 = deltaG / d2dGjjL(T, xL1);

                // Находим xj1
                xLeft = zjL + deltaZ;
                xRight = xj - deltaxj;
                do
                {
                    xVar = (xLeft + xRight) / 2;

                    if ((ddGjjj(T, xVar) - K1) > 0)
                    {
                        xRight = xVar;
                    }
                    else
                    {
                        xLeft = xVar;
                    }
                } while (Math.Abs(Math.Abs(ddGjjj(T, xVar)) - Math.Abs(K1)) > Math.Abs(deltaG / d2dGjjj(T, xVar))); //условие выхода
                xj1 = xVar;
                deltaxj1 = deltaG / d2dGjjj(T, xj1);


                for (; ; )
                {
                    w1 = dGjjL(T, xL1) - dGjjj(T, xj1) - K1 * (xL1 - xj1);

                    if (w1 < 0)
                    {
                        q = K1;
                    }
                    else
                    {
                        Q = K1;
                    }

                    K2 = (Q + q) / 2;
                    K1 = K2;

                    // Поиск xL2
                    xLeft = xL + deltaxL;
                    xRight = zjL - deltaZ;

                    do
                    {
                        xVar = (xLeft + xRight) / 2;

                        if ((ddGjjL(T, xVar) - K2) > 0)
                        {
                            xRight = xVar;
                        }
                        else
                        {
                            xLeft = xVar;
                        }

                    } while (Math.Abs(Math.Abs(ddGjjL(T, xVar)) - Math.Abs(K2)) > Math.Abs(deltaG / d2dGjjL(T, xVar))); //условие выхода

                    xL2 = xVar;
                    deltaxL2 = deltaG / d2dGjjL(T, xL2);


                    // поиск xj2

                    xLeft = zjL + deltaZ;
                    xRight = xj - deltaxj;

                    do
                    {
                        xVar = (xLeft + xRight) / 2;

                        if ((ddGjjj(T, xVar) - K2) > 0)
                        {
                            xRight = xVar;
                        }
                        else
                        {
                            xLeft = xVar;
                        }

                    } while (Math.Abs(Math.Abs(ddGjjj(T, xVar)) - Math.Abs(K2)) > Math.Abs(deltaG / d2dGjjj(T, xVar)));  //условие выхода
                    xj2 = xVar;
                    deltaxj2 = deltaG / d2dGjjj(T, xj2);

                    temp_ddGjjL = ddGjjL(T, xL2);
                    temp_ddGjjj = ddGjjj(T, xj2);
                    temp_dGjjL = dGjjL(T, xL1);
                    temp_dGjjj = dGjjj(T, xj1);

                    if ((Math.Abs(temp_ddGjjL - (temp_dGjjL - temp_dGjjj) / (xL1 - xj1)) >= deltaG) ||
                        (Math.Abs(temp_ddGjjj - (temp_dGjjL - temp_dGjjj) / (xL1 - xj1)) >= deltaG) ||
                        (Math.Abs(xL2 - xL1) >= epsilon) ||
                        (Math.Abs(xj2 - xj1) >= epsilon))
                    {
                        xL1 = xL2;
                        xj1 = xj2;
                    }
                    else
                    {
                        break;
                    }
                }

                TArr[arrIndex] = T;
                xLRightArr[arrIndex] = xj2;
                xjRightArr[arrIndex] = xL2;

                arrIndex++;

            }

            return arrIndex;
        }

        private int calcLeftPart(double TStep, int arrIndex)
        {
            // Переменные концентраций 
            double zjL = 1;
            double deltaZ;

            // Переменные по x
            double xVar;
            double xL1;
            double xL2;
            double xj1;
            double xj2;
            double deltaxL1;
            double deltaxL2;
            double deltaxj1;
            double deltaxj2;

            // Левая граница по концентрации
            double xLeft;
            // Правая граница по концентрации
            double xRight;

            // ОДЗ по К
            double Q;
            double q;
            double deltaQ;
            double deltaq;

            // ОДЗ по х
            double xL;
            double deltaxL;
            double xj;
            double deltaxj;

            // Переменные по К1, К2, w1
            double K1;
            double K2;
            double w1;

            // Переменные для временного хранения значений при проверке условий выхода изцикла по k
            double temp_ddGjjL;
            double temp_ddGjjj;
            double temp_dGjjL;
            double temp_dGjjj;

            // Цикл для левой части
            for (double T = system.leftElement.Ta_b - TStep; T > system.azeotrope.temperature; T = T - TStep)
            {
                //Задаем границы расчетного интервала
                xLeft = system.azeotrope.coordinate;
                xRight = epsilon;

                zjL = dichotomySolutionZjL(xLeft, xRight, T);

                // Выводим zjL(T)
                zjLLeftArr[arrIndex] = zjL;

                // Выбираем минимальную погрешность по zj и zL
                deltaZ = Math.Min(Math.Abs(deltaG / ddGjjj(T, zjL)), Math.Abs(deltaG / ddGjjL(T, zjL)));

                // Определяем ОДЗ по k
                Q = ddGjjj(T, zjL - deltaZ);
                deltaQ = d2dGjjj(T, zjL) * deltaZ;
                q = ddGjjL(T, zjL + deltaZ);
                deltaq = d2dGjjL(T, zjL) * deltaZ;

                // Находим ОДЗ по x: xL
                xRight = system.azeotrope.coordinate - epsilon;
                xLeft = zjL + deltaZ;
                do
                {
                    xVar = (xLeft + xRight) / 2;

                    if ((ddGjjL(T, xVar) - (Q + deltaQ)) < 0)
                    {
                        xLeft = xVar;
                    }
                    else
                    {
                        xRight = xVar;
                    }
                } while (Math.Abs(Math.Abs(ddGjjL(T, xVar)) - Math.Abs(Q + deltaQ)) > Math.Abs(deltaG / d2dGjjL(T, xVar))); //условие выхода
                xL = xVar;
                deltaxL = deltaG / d2dGjjL(T, xL);

                // Находим ОДЗ по x: xj
                xLeft = epsilon;
                xRight = zjL - deltaZ;
                do
                {
                    xVar = (xLeft + xRight) / 2;

                    if ((ddGjjj(T, xVar) - (q - deltaq)) < 0)
                    {
                        xLeft = xVar;
                    }
                    else
                    {
                        xRight = xVar;
                    }
                } while (Math.Abs(Math.Abs(ddGjjj(T, xVar)) - Math.Abs(q - deltaq)) > Math.Abs(deltaG / d2dGjjj(T, xVar))); //условие выхода
                xj = xVar;
                deltaxj = deltaG / d2dGjjj(T, xj);

                // Первое приближение по К в дихотомии
                K1 = (Q + q) / 2;

                // Находим xL1
                xRight = xL - deltaxL;
                xLeft = zjL + deltaZ;
                do
                {
                    xVar = (xLeft + xRight) / 2;

                    if ((ddGjjL(T, xVar) - K1) > 0)
                    {
                        xRight = xVar;
                    }
                    else
                    {
                        xLeft = xVar;
                    }
                } while (Math.Abs(Math.Abs(ddGjjL(T, xVar)) - Math.Abs(K1)) > Math.Abs(deltaG / d2dGjjL(T, xVar))); //условие выхода
                xL1 = xVar;
                deltaxL1 = deltaG / d2dGjjL(T, xL1);

                // Находим xj1
                xLeft = xj + deltaxj;
                xRight = zjL - deltaZ;
                do
                {
                    xVar = (xLeft + xRight) / 2;

                    if ((ddGjjj(T, xVar) - K1) > 0)
                    {
                        xRight = xVar;
                    }
                    else
                    {
                        xLeft = xVar;
                    }
                } while (Math.Abs(Math.Abs(ddGjjj(T, xVar)) - Math.Abs(K1)) > Math.Abs(deltaG / d2dGjjj(T, xVar))); //условие выхода
                xj1 = xVar;
                deltaxj1 = deltaG / d2dGjjj(T, xj1);


                for (; ; )
                {
                    w1 = dGjjL(T, xL1) - dGjjj(T, xj1) - K1 * (xL1 - xj1);

                    if (w1 > 0)
                    {
                        q = K1;
                    }
                    else
                    {
                        Q = K1;
                    }

                    K2 = (Q + q) / 2;
                    K1 = K2;

                    // Поиск xL2
                    xLeft = zjL + deltaZ;
                    xRight = xL - deltaxL;

                    do
                    {
                        xVar = (xLeft + xRight) / 2;

                        if ((ddGjjL(T, xVar) - K2) > 0)
                        {
                            xRight = xVar;
                        }
                        else
                        {
                            xLeft = xVar;
                        }

                    } while (Math.Abs(Math.Abs(ddGjjL(T, xVar)) - Math.Abs(K2)) > Math.Abs(deltaG / d2dGjjL(T, xVar))); //условие выхода

                    xL2 = xVar;
                    deltaxL2 = deltaG / d2dGjjL(T, xL2);


                    // поиск xj2

                    xLeft = xj + deltaxj;
                    xRight = zjL - deltaZ;

                    do
                    {
                        xVar = (xLeft + xRight) / 2;

                        if ((ddGjjj(T, xVar) - K2) > 0)
                        {
                            xRight = xVar;
                        }
                        else
                        {
                            xLeft = xVar;
                        }

                    } while (Math.Abs(Math.Abs(ddGjjj(T, xVar)) - Math.Abs(K2)) > Math.Abs(deltaG / d2dGjjj(T, xVar)));  //условие выхода
                    xj2 = xVar;
                    deltaxj2 = deltaG / d2dGjjj(T, xj2);

                    temp_ddGjjL = ddGjjL(T, xL2);
                    temp_ddGjjj = ddGjjj(T, xj2);
                    temp_dGjjL = dGjjL(T, xL1);
                    temp_dGjjj = dGjjj(T, xj1);

                    if ((Math.Abs(temp_ddGjjL - (temp_dGjjL - temp_dGjjj) / (xL1 - xj1)) >= deltaG) ||
                        (Math.Abs(temp_ddGjjj - (temp_dGjjL - temp_dGjjj) / (xL1 - xj1)) >= deltaG) ||
                        (Math.Abs(xL2 - xL1) >= epsilon) ||
                        (Math.Abs(xj2 - xj1) >= epsilon))
                    {
                        xL1 = xL2;
                        xj1 = xj2;
                    }
                    else
                    {
                        break;
                    }
                }

                TArr[arrIndex] = T;
                xLLeftArr[arrIndex] = xj2;
                xjLeftArr[arrIndex] = xL2;
                arrIndex++;
            }

            return arrIndex;
        }

        /// <summary>
        /// Рассчитать массивы T(x)
        /// </summary>
        /// <param name="TStep">Шаг по температуре</param>
        public void CalculateTX(double TStep)
        {
            int temperatureInterval = (int)((Math.Max(system.leftElement.Ta_b - system.azeotrope.temperature, 
                                                      system.rightElement.Ta_b - system.azeotrope.temperature)) 
                                                 / TStep) + 1;

            TArr = new double[temperatureInterval];
            xLRightArr = new double[temperatureInterval];
            xjRightArr = new double[temperatureInterval];
            xLLeftArr = new double[temperatureInterval];
            xjLeftArr = new double[temperatureInterval];
            zjLRightArr = new double[temperatureInterval];
            zjLLeftArr = new double[temperatureInterval];

            int arrIterator = 0;

            if (system.rightElement.Ta_b > system.leftElement.Ta_b)
            // правое ухо выше
            {
                // считаем точку плавления чистого правого компонента
                TArr[arrIterator] = system.rightElement.Ta_b;
                xLRightArr[arrIterator] = 1;
                xjRightArr[arrIterator] = 1;
                zjLRightArr[arrIterator] = 1;

                // считаем правое ухо
                arrIterator++;
                arrIterator = calcRightPart(TStep, arrIterator);

                // Выводим точку азеотропы
                TArr[arrIterator] = system.azeotrope.temperature;
                xLRightArr[arrIterator] = system.azeotrope.coordinate;
                xjRightArr[arrIterator] = system.azeotrope.coordinate;
                xLLeftArr[arrIterator] = system.azeotrope.coordinate;
                xjLeftArr[arrIterator] = system.azeotrope.coordinate;
                zjLRightArr[arrIterator] = system.azeotrope.coordinate;
                zjLLeftArr[arrIterator] = system.azeotrope.coordinate;

                // считаем левое ухо, возвращая курсор в массиве на позицию для начала координат для
                // левого уха. 
                arrIterator = arrIterator + (int)((system.azeotrope.temperature - system.leftElement.Ta_b) / TStep);

                // Точка плавления левого компонента
                xLLeftArr[arrIterator] = 0;
                xjLeftArr[arrIterator] = 0;
                zjLLeftArr[arrIterator] = 0;
                arrIterator++;

                calcLeftPart(TStep, arrIterator);
            }
            else
            // левое ухо выше
            {
                // Точка плавления левого компонента
                TArr[arrIterator] = system.leftElement.Ta_b;
                xLLeftArr[arrIterator] = 0;
                xjLeftArr[arrIterator] = 0;
                zjLLeftArr[arrIterator] = 0;
                arrIterator++;
                arrIterator = calcLeftPart(TStep, arrIterator);

                // Выводим точку азеотропы
                TArr[arrIterator] = system.azeotrope.temperature;
                xLRightArr[arrIterator] = system.azeotrope.coordinate;
                xjRightArr[arrIterator] = system.azeotrope.coordinate;
                xLLeftArr[arrIterator] = system.azeotrope.coordinate;
                xjLeftArr[arrIterator] = system.azeotrope.coordinate;
                zjLRightArr[arrIterator] = system.azeotrope.coordinate;
                zjLLeftArr[arrIterator] = system.azeotrope.coordinate;

                // считаем правое ухо, возвращая курсор в массиве на позицию для начала координат для
                // правого уха. 
                arrIterator = arrIterator + (int)((system.azeotrope.temperature - system.rightElement.Ta_b) / TStep);

                // Точка плавления правого компонента
                xLRightArr[arrIterator] = 1;
                xjRightArr[arrIterator] = 1;
                zjLRightArr[arrIterator] = 1;

                // считаем правое ухо
                arrIterator++;
                calcRightPart(TStep, arrIterator);
            }
           
        }

    }
}
