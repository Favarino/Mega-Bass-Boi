using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("NoteCollection")]
public class SongNoteCollection
{
    [XmlArray("Measures"), XmlArrayItem("Measure")]
    public Measure[] Measures;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(SongNoteCollection));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static SongNoteCollection Load(string path)
    {
        var serializer = new XmlSerializer(typeof(SongNoteCollection));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as SongNoteCollection;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static SongNoteCollection LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(SongNoteCollection));
        return serializer.Deserialize(new StringReader(text)) as SongNoteCollection;
    }
}