using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyBottle : MonoBehaviour {

    public delegate void OnHit(string path);
    public static OnHit onHit;

    [SerializeField] string path;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            onHit(path);
        }
    }
}
