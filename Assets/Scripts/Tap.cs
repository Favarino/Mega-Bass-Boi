using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 * Tap Class
 * 
 * Delegates key presses from Tap wearable keyboard to an array of fingers.
 * Index 0 -> Thumb
 * Index 4 -> Pinky
 * 
 * Supports single and double tapping.
 */
public class Tap : MonoBehaviour {
	
	public delegate void OnTapped(int[] fingers);
	public OnTapped onTapped;

	void Start () {

	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Alpha1)) {
			onTapped(new int[] { 1, 0, 0, 0, 0 });
		} else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha2)) {
			onTapped(new int[] { 0, 1, 0, 0, 0 });
		} else if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Alpha3)) {
			onTapped(new int[] { 0, 0, 1, 0, 0 });
		} else if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.Alpha4)) {
			onTapped(new int[] { 0, 0, 0, 1, 0 });
		} else if (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.Alpha5)) {
			onTapped(new int[] { 0, 0, 0, 0, 1 });
		}

		// Doubles
		else if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			onTapped(new int[] { 1, 1, 0, 0, 0 });
		} else if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.UpArrow)) {
			onTapped(new int[] { 0, 1, 1, 0, 0 });
		} else if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.RightArrow)) {
			onTapped(new int[] { 0, 0, 1, 1, 0 });
		} else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Alpha9)) {
			onTapped(new int[] { 0, 0, 0, 1, 1 });
		} else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Period)) {
			onTapped(new int[] { 1, 0, 1, 0, 0 });
		} else if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.H)) {
			onTapped(new int[] { 0, 1, 0, 1, 0 });
		} else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Alpha8)) {
			onTapped(new int[] { 0, 0, 1, 0, 1 });
		} else if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.DownArrow)) {
			onTapped(new int[] { 1, 0, 0, 1, 0 });
		} else if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Alpha7)) {
			onTapped(new int[] { 0, 1, 0, 0, 1 });
		} else if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Alpha6)) {
			onTapped(new int[] { 1, 0, 0, 0, 1 });
		}
	}

}