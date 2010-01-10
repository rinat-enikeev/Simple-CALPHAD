using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Globalization;

namespace VisualPhaseCalculation
{
    class BinarySystemFactory
    {
        private IList<IBinarySystem> systems;

        public IList<IBinarySystem> getSystems()
        {
            return systems;
        }

        public BinarySystemFactory()
        {
            Dictionary<string, Element> elements = readElements();
            this.systems = new List<IBinarySystem>(readSystems(elements).Cast<IBinarySystem>());
        }

        private Dictionary<string, Element> readElements()
        {
            XElement elementsXml = XElement.Parse(Properties.Resources.ElementsData);
            Dictionary<string, Element> elements = (from element in elementsXml.Elements("element")
                select new Element
                {
                    id = (string)element.Element("id"),
                    Ta_b = Double.Parse((string)element.Element("phaseChange").Element("temperature"), CultureInfo.InvariantCulture),
                    dH = Double.Parse((string)element.Element("phaseChange").Element("enthalpy"), CultureInfo.InvariantCulture),
                    dS = Double.Parse((string)element.Element("phaseChange").Element("entropy"), CultureInfo.InvariantCulture),
                    dCp = Double.Parse((string)element.Element("phaseChange").Element("heatCapacityChange"), CultureInfo.InvariantCulture)
                }).ToDictionary(e => e.id);
            return elements;
        }

        private IList<BinarySystem> readSystems(IDictionary<string, Element> elements)
        {
            XElement elementsXml = XElement.Parse(Properties.Resources.BinarySystemsData);
            return (from element in elementsXml.Elements("system")
                select new BinarySystem
                {
                    leftElement = elements[(string)element.Element("leftElement").Attribute("id").Value],
                    rightElement = elements[(string)element.Element("rightElement").Attribute("id").Value],
                    azeotrope = new Azeotrope(Double.Parse((string)element.Element("azeotrope").Element("temperature"), CultureInfo.InvariantCulture),
                                              Double.Parse((string)element.Element("azeotrope").Element("coordinate"), CultureInfo.InvariantCulture)),
                    experimentalPoint = new ExperimentalPoint(Double.Parse((string)element.Element("experimental").Element("temperature"), CultureInfo.InvariantCulture),
                                                              Double.Parse((string)element.Element("experimental").Element("liquidusCoordinate"), CultureInfo.InvariantCulture),
                                                              Double.Parse((string)element.Element("experimental").Element("solidusCoordinate"), CultureInfo.InvariantCulture))
                }).ToList();

        }

    }
}
