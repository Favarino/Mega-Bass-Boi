using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public void SetNote(Note n)
    {
        MyNote = n;
    }
}
