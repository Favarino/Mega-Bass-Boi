using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/**
 * Poses are specific positions that the player must hit in order to
 * score higher points.
 * 
 * The poses spawn in a specific order around the stage at different parts of the song. 
 * The player must match the position and rotation of the pose with their bass guitar.
 * 
 * Each pose will remain active for a short period of time until they expire.
 */
public class Poses : MonoBehaviour {

    [SerializeField]
    public float distanceTolerance = 1F;

    [SerializeField]
    public float rotationTolerance = 30F;

    [SerializeField]
    public GameObject phantomBassPrefab;

    Queue<Vector3> posePositions = new Queue<Vector3>();
    Queue<Vector3> eulerAngles = new Queue<Vector3>();

    Vector3 scale = new Vector3(0.05F, 0.05F, 0.05F);

    public GameObject bass;
    public GameObject phantomBass;

    public void Start() {
        posePositions.Enqueue(new Vector3(0.5F, 0.5F, 0.5F));
        eulerAngles.Enqueue(new Vector3(0, 0, 0));

        phantomBass = Instantiate(phantomBassPrefab, posePositions.Dequeue(), Quaternion.Euler(eulerAngles.Dequeue()));
        phantomBass.transform.localScale = scale;
    }

    public void Update() {
        if (bass == null) {
            bass = GameObject.Find("BassGuitarPrefab");
        }
        if (phantomBass != null) {

            Debug.Log(Quaternion.Angle(bass.transform.rotation, phantomBass.transform.rotation));

            if (MatchedPose(bass.transform, phantomBass.transform))
            {
                Debug.Log("Matched Pose!");
                Destroy(phantomBass);
            }
        }
    }

    private Boolean MatchedPose(Transform bass, Transform pose) {
        return Vector3.Distance(bass.position, pose.position) < distanceTolerance
            && Quaternion.Angle(bass.rotation, pose.rotation) < rotationTolerance;
    }

}
