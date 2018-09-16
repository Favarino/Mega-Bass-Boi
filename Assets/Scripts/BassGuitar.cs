using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

/**
 * Player's bass guitar class. Neck of bass is based on the left controller's
 * position on the physical prop.
 */
public class BassGuitar : MonoBehaviour {

    // Pivot bass within parent so that pivot is at neck of guitar
    private Vector3 bassPivotPosition = new Vector3(-0.21F, 0.09F, -0.03F);
    private Vector3 bassPivotRotation = new Vector3(-181.8F, 28.45F, 33.64F);
    public static Vector3 bassScale = new Vector3(0.05F, 0.05F, 0.05F);

    private GameObject bassGuitar;
    private GameObject bassGuitarPrefab;

    public XRNode playerNode = XRNode.LeftHand;

    public void Start() {
        bassGuitar = GameObject.Find("Bass");
        bassGuitarPrefab = bassGuitar.transform.GetChild(0).gameObject;

       // bassGuitarPrefab.transform.position = bassPivotPosition;
       // bassGuitarPrefab.transform.rotation = Quaternion.Euler(bassPivotRotation);
       // bassGuitarPrefab.transform.localScale = bassScale;
    }

    public void Update() {

        // Bass neck tracking left controller
        bassGuitar.transform.position = InputTracking.GetLocalPosition(playerNode);
        bassGuitar.transform.rotation = InputTracking.GetLocalRotation(playerNode);
    }

}
