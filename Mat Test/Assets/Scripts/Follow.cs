/*using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{   
    public GameObject GrabbySphere;

    // The target marker.
    public Transform target GrabbySphere.transform;

    // Speed in units per sec.
    public float speed;

    void Update()
    {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}*/