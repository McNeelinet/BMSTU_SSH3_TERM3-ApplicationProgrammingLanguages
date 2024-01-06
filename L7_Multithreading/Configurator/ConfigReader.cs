using System.Xml.Linq;

namespace Configurator;

public class ConfigReader
{
    public string Filename { get; set; }
    private XDocument XDoc { get; set; }
    private XElement? Configuration { get; set; }

    public ConfigReader(string filename)
    {
        Filename = filename;
        XDoc = XDocument.Load(filename);
        Configuration = XDoc.Element("configuration");
    }

    public List<int> GetTimers(string groupName, string elementName)
    {
        var timers = new List<int>();

        var elements = Configuration?.Element(groupName);
        if (elements != null)
        {
            foreach (var element in elements.Elements(elementName))
            {
                var time = element.Element("time");
                if (time is null) continue;

                timers.Add(Convert.ToInt32(time.Value));
            }
        }

        return timers;
    }
}