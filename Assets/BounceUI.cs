using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceUI : MonoBehaviour {
    Vector3 startPos;
    int modifier;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.time/4, .3f), transform.localPosition.z);
        float y = transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y += modifier, transform.localPosition.z);
    }
}
