using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdAi : MonoBehaviour {
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.Instance.PlayerCurrentGameState == GameManager.GameStates.PLAYING)
        {
            anim.SetBool("Dance", true);
        }
	}
}
