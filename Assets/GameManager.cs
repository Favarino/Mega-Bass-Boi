//Game Manager.  Handles all the major game logic
//Author: Evie Powell

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public NoteManagerScript NoteManager;

    private SongNoteCollection levelSong;
    private float songStartTime;

    private float songElapsedTime;

    [SerializeField] GameObject notePrefab;

    public enum GameStates
    {
        WAITING_TO_START,
        PLAYING,
        PLAYING_WITH_STYLE,
        PAUSED,
        DEAD
    }

    public GameStates PlayerCurrentGameState;

    public static GameManager Instance;

    public int BPM = 120;
    public float OneBeat;
    public float OneTick;

    [SerializeField] readonly int measureBuffer = 2;

    public float SongElapsedTime
    {
        get
        {
            return songElapsedTime;
        }

        set
        {
            songElapsedTime = value;
        }
    }

    void Awake()
    {
        Instance = this;
        PlayerCurrentGameState = GameStates.WAITING_TO_START;
    }
	// Use this for initialization
	void Start () {
        OneBeat = 1f / ((float)BPM / 60f);
        OneTick = OneBeat / 120f; // there are 120 ticks in a beat (arbitrary but chosen for optimal resolution in  4/4 time)

        levelSong = SongNoteCollection.Load(Path.Combine(Application.dataPath, "StreamingAssets/song_hackathon.xml"));


        for(int i = 0; i < levelSong.Measures.Length; i++)
        {
            for(int j = 0; j < levelSong.Measures[i].Notes.Length; j++)
            {
                Note n = levelSong.Measures[i].Notes[j];
                n.ConvertNoteTime(i);
            }
        }

        NoteManager.CalculateDistances();
        NoteManager.PopulateNotes();
        //Wait for player to press start And then start the song
        StartCoroutine(WaitForPlayerStart());
    }
	
    IEnumerator WaitForPlayerStart()
    {
        yield return null;
        while (PlayerCurrentGameState == GameStates.WAITING_TO_START)
        {
            yield return null;  //shorthand for WaitOneFrame
            songStartTime = Time.realtimeSinceStartup;
        }

        songStartTime = Time.realtimeSinceStartup;

        NoteManager.MoveNotes();
    }
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerCurrentGameState = GameStates.PLAYING;
        }
        if(isPlaying())
        {
            songElapsedTime = Time.realtimeSinceStartup - songStartTime;
            NoteManager.MoveNotes();
            for (int i = 0; i < levelSong.Measures.Length; i++)
            {
                for (int j = 0; j < levelSong.Measures[i].Notes.Length; j++)
                {
                    Note n = levelSong.Measures[i].Notes[j];

                    if (!n.IsInactive && n.NoteElapsedTime + (8f * OneBeat) +((float)n.BeatError * OneTick) < songElapsedTime)
                    {
                        n.IsInactive = true;
                        Debug.Log("Note " + n.id + " of mesasure " + i + " was played");
                    }
                }
            }
                    //Enter Update Note Scoring
                    //Enter Update Note Display On Screen
        }
	}
    public SongNoteCollection GetSong()
    {
        return levelSong;
    }
    GameObject CreateNote(Note n)
    {
        int val = ParseId(n);
        GameObject note = Instantiate(notePrefab);

        RegisterNoteValuesToObject(n, note);

        Material noteMat = note.GetComponent<Material>();
        switch (val)
        {
            case 1:
                noteMat.color = Color.red;
                break;
            case 2:
                noteMat.color = Color.yellow;
                break;
            case 3:
                noteMat.color = Color.green;
                break;
            case 4:
                noteMat.color = Color.blue;
                break;
            default:
                break;
        }

        return note;
    }

    void RegisterNoteValuesToObject(Note n, GameObject obj)
    {
        Note noteCom = obj.GetComponent<Note>();
        noteCom.id = n.id;
        noteCom.IsInactive = n.IsInactive;
        noteCom.NoteElapsedTime = n.NoteElapsedTime;
        noteCom.SubBeatNumber = n.SubBeatNumber;
        noteCom.BeatDuration = n.BeatDuration;
        noteCom.BeatError = n.BeatError;
        noteCom.BeatNumber = n.BeatNumber;
    }

    int ParseId(Note n)
    {
        int val;
        if (!Int32.TryParse(n.id, out val))
        {
            print("parse failed");
            val = -1;
        }
        return val;
    }

    bool isPlaying()
    {
        return PlayerCurrentGameState == GameStates.PLAYING || PlayerCurrentGameState == GameStates.PLAYING_WITH_STYLE;
    }
}
