//Game Manager.  Handles all the major game logic
//Author: Evie Powell

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private SongNoteCollection levelSong;
    private float songStartTime;

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
                Debug.Log("Note on measure for starts: " + n.NoteElapsedTime);
            }
        }
        //Wait for player to press start And then start the song
        StartCoroutine(WaitForPlayerStart());
    }
	
    IEnumerator WaitForPlayerStart()
    {
        yield return null;
        while (PlayerCurrentGameState == GameStates.WAITING_TO_START)
        {
            yield return null;
        }

        songStartTime = Time.realtimeSinceStartup;
    }
	// Update is called once per frame
	void Update () {

        if(isPlaying())
        {
            //Enter Update Note Scoring
            //Enter Update Note Display On Screen
            Debug.Log("TODO: game started spam logic");
        }
	}

    bool isPlaying()
    {
        return PlayerCurrentGameState == GameStates.PLAYING || PlayerCurrentGameState == GameStates.PLAYING_WITH_STYLE;
    }
}
