using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowTarget : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float smoothTime = 0.3f;

    [SerializeField] Vector3 offset;

    Vector3 velocity = Vector3.zero;

	// Update is called once per frame
	void Update () {

        Vector3 targetPosition = target.TransformPoint(offset);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(target);
        transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z+90));
	}
}
