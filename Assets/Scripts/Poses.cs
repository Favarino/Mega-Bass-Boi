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

    // Pose Hit Event
    public delegate void PoseHitDelegate();
    public static PoseHitDelegate PoseHit;

    [SerializeField]
    public float distanceTolerance = 1F;

    [SerializeField]
    public float rotationTolerance = 30F;

    [SerializeField]
    public GameObject phantomBassPrefab;
    
    Queue<Vector3> posePositions = new Queue<Vector3>();
    Queue<Vector3> eulerAngles = new Queue<Vector3>();

    Queue<float> poseCues = new Queue<float>();

    public GameObject bass;
    public GameObject phantomBass;

    public void Start() {
        poseCues = new Queue<float>(new[] { 2F, 4F, 6F, 8F, 10F, 12F, 14F, 16F, 18F, 20F, 22F, 24F });

        // Pose One
        posePositions.Enqueue(new Vector3(-2.01F, 1.8F, 0F));
        eulerAngles.Enqueue(new Vector3(-226F, 41F, 69F));

        // Pose Two
        posePositions.Enqueue(new Vector3(-2.8F, 1.8F, 0.92F));
        eulerAngles.Enqueue(new Vector3(-226.9F, 80.3F, 69F));

        // Pose Three
        posePositions.Enqueue(new Vector3(-3.79F, 1.8F, 0.21F));
        eulerAngles.Enqueue(new Vector3(-226.88F, 55.5F, 69F));

        // Pose Four
        posePositions.Enqueue(new Vector3(-3.87F, 1.7F, -0.24F));
        eulerAngles.Enqueue(new Vector3(-200.43F, 65.1F, 110.2F));

        // Pose Five
        posePositions.Enqueue(new Vector3(-3F, 1.7F, -0.47F));
        eulerAngles.Enqueue(new Vector3(37.6F, -211F, -80.5F));

        // Pose Six
        posePositions.Enqueue(new Vector3(-2.94F, 1.69F, 1.17F));
        eulerAngles.Enqueue(new Vector3(41.9F, 76.7F, -70F));

        // Pose Seven
        posePositions.Enqueue(new Vector3(-3.96F, 1.69F, 1.59F));
        eulerAngles.Enqueue(new Vector3(43.73F, -0.8F, -70F));

        // Pose Eigth
        posePositions.Enqueue(new Vector3(-4.5F, 1.9F, 0.41F));
        eulerAngles.Enqueue(new Vector3(42.9F, -131.2F, -112F));

        // Pose Nine
        posePositions.Enqueue(new Vector3(-4.55F, 0.84F, 0.13F));
        eulerAngles.Enqueue(new Vector3(59.7F, -42.83F, -5F));

        // Pose Ten
        posePositions.Enqueue(new Vector3(-4.5F, 1.9F, 0.41F));
        eulerAngles.Enqueue(new Vector3(42.9F, -131.2F, -112F));

        // Pose Eleven
        posePositions.Enqueue(new Vector3(-4.55F, 0.84F, 0.13F));
        eulerAngles.Enqueue(new Vector3(59.7F, -42.83F, -5F));

        // Pose Twelve - Victory Pose
        posePositions.Enqueue(new Vector3(-5.13F, 2.71F, -0.27F));
        eulerAngles.Enqueue(new Vector3(172F, 13.6F, 33.8F));

        // Instantiate and set Inactive
        phantomBass = Instantiate(phantomBassPrefab, new Vector3(), Quaternion.Euler(new Vector3()));
        phantomBass.transform.localScale = BassGuitar.bassScale;
        phantomBass.SetActive(false);


        bass = GameObject.Find("BassGuitarPrefab");
    }

    public void Update() {
        if (poseCues.Count != 0 && poseCues.Peek() < Time.time) {

            poseCues.Dequeue();

            if (posePositions.Count != 0 && eulerAngles.Count != 0) {
                phantomBass.SetActive(true);
                phantomBass.transform.position = posePositions.Dequeue();
                phantomBass.transform.localEulerAngles = eulerAngles.Dequeue();
            }
        }

        if (MatchedPose(bass.transform, phantomBass.transform)) {
            PoseHit();
            phantomBass.SetActive(false);
        }
    }

    private Boolean MatchedPose(Transform bass, Transform pose) {
        return Vector3.Distance(bass.position, pose.position) < distanceTolerance
            && Quaternion.Angle(bass.rotation, pose.rotation) < rotationTolerance;
    }

}
