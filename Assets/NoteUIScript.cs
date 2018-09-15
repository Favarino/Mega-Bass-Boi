using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoteUIScript : MonoBehaviour {

    Vector3 startPosition;
    Note myNote;

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
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
