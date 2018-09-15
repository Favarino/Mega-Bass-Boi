using System.Xml;
using System.Xml.Serialization;

public class Measure
{
    [XmlArray("Notes"), XmlArrayItem("Note")]
    public Note[] Notes;
}