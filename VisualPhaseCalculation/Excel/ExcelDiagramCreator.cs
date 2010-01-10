using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace VisualPhaseCalculation
{
    class ExcelDiagramCreator
    {

        const double epsilon = 0.00000001;

        /// <summary>
        /// Выводит в Excel бездиффузионную диаграмму состояния
        /// </summary>
        public void drawNonDiffusionDiagramInExcel(BinaryPhaseDiagram diagram)
        {
            // Инициализация Excel
            Excel.Application excelapp = new Excel.Application();
            excelapp.Visible = true;
            Excel.Workbooks excelappworkbooks = excelapp.Workbooks;

            Excel.Workbook excelappworkbook = excelapp.Workbooks.Add(Type.Missing);

            Excel.Sheets excelsheets = excelappworkbook.Worksheets;
            Excel.Worksheet excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);

            // Объединение ячеек 
            Excel.Range rng = excelworksheet.get_Range("A2", "A2");
            Excel.Range rng1 = excelworksheet.get_Range("A1", "C1");
            rng1.Merge(Type.Missing);

            // Шапка
            rng1.Value2 = "Рассчитанные массивы";
            rng1.Font.Bold = true;
            rng1.HorizontalAlignment = Excel.Constants.xlCenter;

            rng.Value2 = "T";
            rng.EntireRow.HorizontalAlignment = Excel.Constants.xlCenter;
            rng.EntireRow.Font.Bold = true;
            rng = rng.get_Offset(0, 1);
            rng.Value2 = "zjL";
            rng = rng.get_Offset(1, -1);

            // Переменные концентраций 
            double zjL = 1;

            // Левая граница по концентрации
            double xLeft;
            // Правая граница по концентрации
            double xRight;

            // Выводим точку плавления чистого правого компонента
            rng.Value2 = diagram.system.rightElement.Ta_b;
            rng = rng.get_Offset(0, 1);
            rng.Value2 = 1;
            rng = rng.get_Offset(1, -1);

            // Цикл для правой части
            for (int T = (int)diagram.system.rightElement.Ta_b - 1; T > diagram.system.azeotrope.temperature; T--)
            {
                //Задаем границы расчетного интервала
                xLeft = diagram.system.azeotrope.coordinate;
                xRight = 1 - epsilon;

                // Методом дихотомии вычисляем 
                zjL = diagram.dichotomySolutionZjL(xLeft, xRight, T);

                // Выводим точку zjL(T)
                rng.Value2 = T;
                rng = rng.get_Offset(0, 1);
                rng.Value2 = zjL;
                rng = rng.get_Offset(1, -1);
            }

            // Выводим точку азеотропы
            rng.Value2 = diagram.system.azeotrope.temperature;
            rng = rng.get_Offset(0, 1);
            rng.Value2 = diagram.system.azeotrope.coordinate;
            rng = rng.get_Offset(0, 1);
            rng.Value2 = diagram.system.azeotrope.coordinate;

            // Смещаем позицию на точку плавления левого компонента
            rng = rng.get_Offset((int)diagram.system.azeotrope.temperature - (int)diagram.system.leftElement.Ta_b, 0);

            // Выводим точку плавления чистого левого компонента
            rng.Value2 = 0;
            rng = rng.get_Offset(1, 0);

            // Цикл для левой части
            for (int T = (int)diagram.system.leftElement.Ta_b - 1; T > diagram.system.azeotrope.temperature; T--)
            {
                //Задаем границы расчетного интервала
                xLeft = diagram.system.azeotrope.coordinate;
                xRight = epsilon;

                zjL = diagram.dichotomySolutionZjL(xLeft, xRight, T);

                // Выводим zjL(T)
                rng.Value2 = zjL;
                rng = rng.get_Offset(1, 0);
            }

            // Строим график в Excel
            //Выделяем ячейки с данными  в таблице
            Excel.Range excelcells = excelworksheet.get_Range("A3", "C" + ((int)diagram.system.rightElement.Ta_b - diagram.system.azeotrope.temperature + 3).ToString());
            //И выбираем их
            excelcells.Select();
            //Создаем объект Excel.Chart диаграмму по умолчанию
            Excel.Chart excelchart = (Excel.Chart)excelapp.Charts.Add(Type.Missing,
             Type.Missing, Type.Missing, Type.Missing);
            //Выбираем диграмму - отображаем лист с диаграммой
            excelchart.Activate();
            excelchart.Select(Type.Missing);
            //Изменяем тип диаграммы
            excelapp.ActiveChart.ChartType = Excel.XlChartType.xlXYScatterLines;
            //Создаем надпись - Заглавие диаграммы
            excelapp.ActiveChart.HasTitle = false;
            //excelapp.ActiveChart.ChartTitle.Text
            //   = "F(N) Диаграмма";
            //Меняем шрифт, можно поменять и другие параметры шрифта
            //excelapp.ActiveChart.ChartTitle.Font.Size = 14;
            //Даем названия осей
            ((Excel.Axis)excelapp.ActiveChart.Axes(Excel.XlAxisType.xlCategory,
                Excel.XlAxisGroup.xlPrimary)).HasTitle = true;
            ((Excel.Axis)excelapp.ActiveChart.Axes(Excel.XlAxisType.xlCategory,
                Excel.XlAxisGroup.xlPrimary)).AxisTitle.Text = "x";
            //((Excel.Axis)excelapp.ActiveChart.Axes(Excel.XlAxisType.xlSeriesAxis,
            //    Excel.XlAxisGroup.xlPrimary)).HasTitle = false;
            ((Excel.Axis)excelapp.ActiveChart.Axes(Excel.XlAxisType.xlValue,
                Excel.XlAxisGroup.xlPrimary)).HasTitle = true;
            ((Excel.Axis)excelapp.ActiveChart.Axes(Excel.XlAxisType.xlValue,
                Excel.XlAxisGroup.xlPrimary)).AxisTitle.Text = "T";
            //Координатная сетка - оставляем только крупную сетку
            ((Excel.Axis)excelapp.ActiveChart.Axes(Excel.XlAxisType.xlCategory,
               Excel.XlAxisGroup.xlPrimary)).HasMajorGridlines = false;
            ((Excel.Axis)excelapp.ActiveChart.Axes(Excel.XlAxisType.xlCategory,
              Excel.XlAxisGroup.xlPrimary)).HasMinorGridlines = false;
            //((Excel.Axis)excelapp.ActiveChart.Axes(Excel.XlAxisType.xlSeriesAxis,
            //  Excel.XlAxisGroup.xlPrimary)).HasMajorGridlines = true;
            //((Excel.Axis)excelapp.ActiveChart.Axes(Excel.XlAxisType.xlSeriesAxis,
            //  Excel.XlAxisGroup.xlPrimary)).HasMinorGridlines = false;
            ((Excel.Axis)excelapp.ActiveChart.Axes(Excel.XlAxisType.xlValue,
              Excel.XlAxisGroup.xlPrimary)).HasMinorGridlines = false;
            ((Excel.Axis)excelapp.ActiveChart.Axes(Excel.XlAxisType.xlValue,
              Excel.XlAxisGroup.xlPrimary)).HasMajorGridlines = true;

            ((Excel.Axis)excelapp.ActiveChart.Axes(Excel.XlAxisType.xlCategory,
              Excel.XlAxisGroup.xlPrimary)).MaximumScale = 1;

            //((Excel.Axis)excelapp.ActiveChart.Axes(Excel.XlAxisType.xlValue,
            //  Excel.XlAxisGroup.xlPrimary)).TickLabels.NumberFormat = "0,0E+00";
            excelapp.ActiveChart.PlotArea.Interior.Color = 14277081;

            //Будем отображать легенду и уберем строки, 
            //которые отображают пустые строки таблицы
            excelapp.ActiveChart.HasLegend = true;
            //Расположение легенды
            excelapp.ActiveChart.Legend.Position
               = Excel.XlLegendPosition.xlLegendPositionLeft;
            //Можно изменить шрифт легенды и другие параметры 
            ((Excel.LegendEntry)excelapp.ActiveChart.Legend.LegendEntries(1)).Font.Size = 12;
            //((Excel.LegendEntry)excelapp.ActiveChart.Legend.LegendEntries(3)).Font.Size = 12;
            //Легенда тесно связана с подписями на осях - изменяем надписи
            // - меняем легенду, удаляем чтото на оси - изменяется легенда
            Excel.SeriesCollection seriesCollection =
             (Excel.SeriesCollection)excelapp.ActiveChart.SeriesCollection(Type.Missing);
            Excel.Series series = seriesCollection.Item(1);
            series.Name = "";
            //Помним, что у нас объединенные ячейки, значит каждая второя строка - пустая
            //Удаляем их из диаграммы и из легенды
            series = seriesCollection.Item(2);
            series.Delete();

            series = seriesCollection.Item(1);

            series.MarkerStyle = Microsoft.Office.Interop.Excel.XlMarkerStyle.xlMarkerStyleNone;

            //Переименуем ось X
            int usedrange = excelworksheet.UsedRange.Rows.Count;
            Excel.Range excelcellX = excelworksheet.get_Range("B3", "B" + usedrange.ToString());
            series.XValues = excelcellX;
            Excel.Range excelcellY = excelworksheet.get_Range("A3", "A" + usedrange.ToString());
            series.Values = excelcellY;

            series = seriesCollection.Item(2);

            series.MarkerStyle = Microsoft.Office.Interop.Excel.XlMarkerStyle.xlMarkerStyleNone;

            //Переименуем ось X
            excelcellX = excelworksheet.get_Range("C3", "C" + usedrange.ToString());
            series.XValues = excelcellX;
            excelcellY = excelworksheet.get_Range("A3", "A" + usedrange.ToString());
            series.Values = excelcellY;

            //Если закончить код на этом месте то у нас Диаграммы на отдельном листе - Рис.9.
            //Строку легенды можно удалить здесь, но строка на оси не изменится
            //Поэтому мы удаляли в Excel.Series
            //((Excel.LegendEntry)excelapp.ActiveChart.Legend.LegendEntries(2)).Delete();
            //Перемещаем диаграмму на лист 1
            excelapp.ActiveChart.Location(Excel.XlChartLocation.xlLocationAsObject, "Лист1");
            //Получаем ссылку на лист 1
            excelsheets = excelappworkbook.Worksheets;
            excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
            //Перемещаем диаграмму в нужное место
            excelworksheet.Shapes.Item(1).IncrementLeft(-201);
            excelworksheet.Shapes.Item(1).IncrementTop((float)1);
            //Задаем размеры диаграммы
            excelworksheet.Shapes.Item(1).Height = 500;
            excelworksheet.Shapes.Item(1).Width = 500;
            excelapp.ActiveChart.HasLegend = false;
        }
    }
}
