using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour
{
    private Vector2 leftThumbstick;
    private Vector2 rightThumbstick;
    private OVRGrabber grabber;
    Vector3 m_EulerAngleVelocity;
    Quaternion deltaRotation;
    Rigidbody rb;
    private Vector3 defaultSize;

    [Tooltip("The speed of rotation when using left thumbstick. A speed between 50-150 is recommended.")]
    public float speed;
    [Tooltip("Minimum size of object, applied to all scale coordinates.")]
    public float min;
    [Tooltip("Maximum size of object, applied to all scale coordinates.")]
    public float max;
    [Tooltip("Starting size of object, applied to all scale coordinates.")]
    public float size;

    // Use this for initialization
    void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
        leftThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        // If no rotation speed is specified, default to 5
        if (speed == 0) { speed = 5f; }

        // Set the minimum and maximum scale of the object
        // Minimum must be greater than zero
        if (min == 0) { min = 0.1f; }
        if (max == 0) { max = 1f; }
        if (size == 0) { size = min;  }
        InputTracking.Recenter();

        defaultSize = new Vector3(size, size, size);
}

    // FixedUpdate is called zero, once, or more times per frame
    private void FixedUpdate()
    {
        //this.transform.Rotate(new Vector3(leftThumbstick[1], (-1 * leftThumbstick[0]), 0) * Time.deltaTime * speed, Space.World); // rotate cube by thumbstick input
        //this.transform.Rotate(leftThumbstick[1], (-1 * leftThumbstick[0]), 0, Space.World);
        /*if (leftThumbstick != Vector2.zero)
        {
            m_EulerAngleVelocity = new Vector3(leftThumbstick[1], (-1 * leftThumbstick[0]), 0);
            deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * speed);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }*/

        if (rightThumbstick != Vector2.zero)
        {
            float zoom = rightThumbstick[1] / 100;

            this.transform.localScale = new Vector3 (
                Mathf.Clamp(transform.localScale.x + zoom, min, max),
                Mathf.Clamp(transform.localScale.y + zoom, min, max),
                Mathf.Clamp(transform.localScale.z + zoom, min, max)
            );

        }

    }

    // Update is called once per frame
    void Update ()
    {
        // (x, y) --> x is left/right, y is up/down
        leftThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick); // get input of left thumbstick
        rightThumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick); // get input of right thumbstick
        this.transform.Rotate(new Vector3(leftThumbstick[1], (-1 * leftThumbstick[0]), 0) * Time.deltaTime * speed, Space.World); // rotate cube by thumbstick input

        // When Y pressed, return cube to origin, and stop all movement and rotation
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            grabber = this.GetComponent<OVRGrabbable>().grabbedBy;
            if ( grabber != null)
            {
                grabber.ForceRelease(this.GetComponent<OVRGrabbable>());
            }
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            this.transform.localScale = defaultSize;
            this.transform.SetPositionAndRotation(Vector3.zero, new Quaternion(0, 0, 0, 0));
        }
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            InputTracking.Recenter();
        }
    }
}
