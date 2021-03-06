﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManagerScript : MonoBehaviour {

    public GameObject NoteCollectionContainer;
    public NoteUIScript prefabNoteUI;
    public GameObject CueBarObject;
    public GameObject RevealBarObject;


    float measureDistance;
    float beatDistance;
    float tickDistance;

    public void CalculateDistances()
    {
        float delta = RevealBarObject.transform.localPosition.x - CueBarObject.transform.localPosition.x;
        measureDistance = delta / 2f;
        beatDistance = delta / 8f;
        tickDistance = beatDistance / 120f;
    }

    public void PopulateNotes()
    {

        SongNoteCollection song = GameManager.Instance.GetSong();

        for (int i = 0; i < song.Measures.Length; i++)
        {
            if (song.Measures[i].Notes != null)
            {
                for (int j = 0; j < song.Measures[i].Notes.Length; j++)
                {
                    Note n = song.Measures[i].Notes[j];
                    NoteUIScript newNote = GameObject.Instantiate<NoteUIScript>(prefabNoteUI, NoteCollectionContainer.transform);
                    newNote.SetNote(n);
                    newNote.transform.localPosition = new Vector3(CueBarObject.transform.localPosition.x + ((float)i * measureDistance)
                        + ((float)(n.BeatNumber - 1) * beatDistance) + (float)n.SubBeatNumber * tickDistance, DetermineNoteYPosition(newNote), transform.localPosition.z);
                }
            }
        }
    }

    float DetermineNoteYPosition(NoteUIScript note)
    {
        float noteHeight = note.GetComponent<MeshRenderer>().bounds.size.y * 8;
        float localY = note.transform.localPosition.y;
        float y = localY - (noteHeight * note.MyNote.id);
        return y;
    }
	
    public void MoveNotes()
    {
        SongNoteCollection song = GameManager.Instance.GetSong();

        foreach(Transform t in NoteCollectionContainer.transform)
        {
            NoteUIScript nuis = t.GetComponent<NoteUIScript>();
            MoveNote(nuis, CueBarObject.transform.localPosition);
            if (!t.GetComponent<MeshRenderer>().enabled && !nuis.MyNote.IsInactive)
            {
                if(t.localPosition.x < RevealBarObject.transform.localPosition.x)
                {
                    t.GetComponent<MeshRenderer>().enabled = true;
                    t.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                }             
            }          
        }
    }

    void MoveNote(NoteUIScript nuis, Vector3 target)
    {
        Vector3 startPos = nuis.StartPosition;
        float elapsedTime = GameManager.Instance.SongElapsedTime;
        //float t = (nuis.MyNote.NoteElapsedTime - elapsedTime) * .05f * measureDistance;


        nuis.transform.localPosition = nuis.StartPosition - new Vector3((elapsedTime / GameManager.Instance.OneTick) * tickDistance, 0f, 0f);  
    }

    // Update is called once per frame
    void Update () {
		
	}
}
