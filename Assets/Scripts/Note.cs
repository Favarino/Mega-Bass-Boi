using System;
using System.Xml;
using System.Xml.Serialization;

public class Note
{
    [XmlAttribute("id")]
    public string id;

    public int BeatNumber;
    public int SubBeatNumber;
    public int BeatDuration;
    public int BeatError;

    public float NoteElapsedTime;

    public void ConvertNoteTime(int measure)
    {
        NoteElapsedTime = (float)measure * GameManager.Instance.OneBeat * 4f;
        NoteElapsedTime += (float)(BeatNumber - 1) * GameManager.Instance.OneBeat;
        NoteElapsedTime += (float)SubBeatNumber * GameManager.Instance.OneTick;
    }
}