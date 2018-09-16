using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoteUIScript : MonoBehaviour {

    Vector3 startPosition;
    Note myNote;

    Tap tap;

    float errorThresholdTime;
    float positiveErrorThresh;
    float negativeErrorThresh;

    public Note MyNote
    {
        get
        {
            return myNote;
        }

        set
        {
            myNote = value;
        }
    }

    public Vector3 StartPosition
    {
        get
        {
            return startPosition;
        }

        set
        {
            startPosition = value;
        }
    }

    // Use this for initialization
    void Start () {
        StartPosition = transform.localPosition;
        RegisterNoteColor();
        Tap.onTapped += OnTapped;

        errorThresholdTime = myNote.BeatError * GameManager.Instance.OneTick;
        positiveErrorThresh = (myNote.NoteElapsedTime) + errorThresholdTime;
        negativeErrorThresh = (myNote.NoteElapsedTime) - errorThresholdTime;
        //print(errorThresholdTime);
    }
	
	// Update is called once per frame
	void Update () {
        //Used to visualize error threshold of notes

        //if (!myNote.IsInactive &&
        //    GameManager.Instance.SongElapsedTime > negativeErrorThresh
        //    && GameManager.Instance.SongElapsedTime< positiveErrorThresh)
        //{
        //    Material[] mats = GetComponent<MeshRenderer>().materials;
        //    for (int i = 0; i < mats.Length; i++)
        //    {
        //        mats[i].color = Color.white;
        //    }
        //}
        //else
        //{
        //    Material[] mats = GetComponent<MeshRenderer>().materials;
        //    for (int i = 0; i < mats.Length; i++)
        //    {
        //        mats[i].color = Color.black;
        //    }
        //}
        if(MyNote.IsInactive)
        {
            gameObject.SetActive(false);
        }
    }

    
    public void SetNote(Note n)
    {
        MyNote = n;
        RegisterNoteColor();
    }

    void RegisterNoteColor()
    {
        Material[] mats = GetComponent<MeshRenderer>().materials;
        switch (myNote.id)
        {
            case 1:
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i].color = Color.green;
                }
                break;
            case 2:
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i].color = Color.red;
                }
                break;
            case 3:
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i].color = Color.yellow;
                }
                break;
            case 4:
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i].color = Color.blue;
                }
                break;
            default:
                break;
        }
    }

    void OnTapped(int[] fingers)
    {
        if (!myNote.IsInactive &&
            GameManager.Instance.SongElapsedTime > negativeErrorThresh
            && GameManager.Instance.SongElapsedTime < positiveErrorThresh)
        {
            if (fingers[myNote.id] == 1)
            {
                OnTappedTheatrics();
            }
        }
    }

    //Spawn Particles and such
    void OnTappedTheatrics()
    {
        print("gotnote");
        MyNote.hasBeenHit = true;
        GameManager.Instance.score += 10*GameManager.Instance.scoreMultiplier;
        GameManager.Instance.currentStreak++;
        GameManager.Instance.CheckForNewCombo();
        if (transform.gameObject != null)
            transform.gameObject.SetActive(false);
    }
}
