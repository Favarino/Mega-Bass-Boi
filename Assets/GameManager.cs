//Game Manager.  Handles all the major game logic
//Author: Evie Powell

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public NoteManagerScript NoteManager;

    private SongNoteCollection levelSong;
    private float songStartTime;

    public float songElapsedTime;

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

    public int score = 0;
    public int scoreMultiplier = 1;

    public int currentStreak;

    public AudioSource audioSource;

    [SerializeField] TextMeshProUGUI scoreText;

    string songPath = "StreamingAssets/song_hackathon_medium.xml";

    public float SongElapsedTime
    {
        get {
            return songElapsedTime;
        }

        set
        {
            songElapsedTime = value;
        }
    }

    public void SetPath(string path)
    {
        songPath = path;

        levelSong = SongNoteCollection.Load(Path.Combine(Application.dataPath, songPath));


        for (int i = 0; i < levelSong.Measures.Length; i++)
        {
            if (levelSong.Measures[i].Notes != null)
            {
                for (int j = 0; j < levelSong.Measures[i].Notes.Length; j++)
                {
                    Note n = levelSong.Measures[i].Notes[j];
                    n.ConvertNoteTime(i);
                }
            }
        }

        NoteManager.CalculateDistances();
        NoteManager.PopulateNotes();
    }

    public void SetScore(int value)
    {
        score = score+=value;
        scoreText.text = "Score:" + score;
    }

    void Awake() { 
        PlayerCurrentGameState = GameStates.WAITING_TO_START;

        //Check if instance already exists
        if (Instance == null)

            //if not, set instance to this
            Instance = this;

        //If instance already exists and it's not this:
        else if (Instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
	// Use this for initialization
	void Start () {
        OneBeat = 1f / ((float)BPM / 60f);
        OneTick = OneBeat / 120f; // there are 120 ticks in a beat (arbitrary but chosen for optimal resolution in  4/4 time)

       
        //Wait for player to press start And then start the song
        StartCoroutine(WaitForPlayerStart());

        // Subscripe to Pose Hit
        Poses.PoseHit += poseHit;
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
            audioSource.Play();
        }
        if(isPlaying())
        {
            songElapsedTime = Time.realtimeSinceStartup - songStartTime;
            NoteManager.MoveNotes();
            for (int i = 0; i < levelSong.Measures.Length; i++)
            {
                if (levelSong.Measures[i].Notes != null)
                {
                    for (int j = 0; j < levelSong.Measures[i].Notes.Length; j++)
                    {
                        Note n = levelSong.Measures[i].Notes[j];

                        if (!n.IsInactive && n.NoteElapsedTime + ((float)n.BeatError * OneTick) < songElapsedTime)
                        {
                            n.IsInactive = true;
                            if (!n.hasBeenHit)
                            {
                                currentStreak = 0;
                            }
                        }
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

    bool isPlaying()
    {
        return PlayerCurrentGameState == GameStates.PLAYING || PlayerCurrentGameState == GameStates.PLAYING_WITH_STYLE;
    }

    public void CheckForNewCombo()
    {
        switch (currentStreak)
        {
            case 0:
                scoreMultiplier = 1;
                break;
            case 8:
                scoreMultiplier = 2;
                break;
            case 16:
                scoreMultiplier = 4;
                break;
            case 32:
                scoreMultiplier = 8;
                break;
            default:
                break;
        }
    }

    public void poseHit() {
        Debug.Log("Hit Pose");
    }
}
