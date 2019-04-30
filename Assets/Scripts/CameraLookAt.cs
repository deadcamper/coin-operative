using System.Collections;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public Transform targetLookAt;
    public Transform targetForwardLook;
    public Rigidbody body;
    public Collider collide;

    public float cameraSpeed;
    public float allowedDistanceFromGoal;
    public float dangerZoneFromGoal = 10f;

    private bool stopFloating = false;

    internal Vector3 idealPosition;

    public float preferredXZDistance = 0f, preferredYDistance = 0f;

    void Start()
    {
        idealPosition = transform.position;
        StartCoroutine(DoCameraThings());
    }

    // Update is called once per frame
    void Update()
    {
        if (targetLookAt != null)
        {
            transform.LookAt(targetLookAt, Vector3.up);
        }


        if (Input.GetMouseButton(1))
        {
            stopFloating = true;
            float rotX = Input.GetAxis("Mouse X");
            float rotY = Input.GetAxis("Mouse Y");

            CameraLookAt lookAt = Camera.main.GetComponent<CameraLookAt>();

            lookAt.body.AddRelativeForce(Vector3.right * rotX, ForceMode.VelocityChange);
            lookAt.body.AddRelativeForce(Vector3.forward * rotY, ForceMode.VelocityChange);
        }
        else
        {
            stopFloating = false;
        }
    }

    IEnumerator DoCameraThings()
    {
        yield return LookForTarget();

        while(true)
        {
            yield return FollowTarget();
        }
    }

    IEnumerator LookForTarget()
    {
        while (targetLookAt == null)
        {
            yield return null;
        }

        Vector3 distance = transform.position - targetLookAt.position;
        if (preferredXZDistance == 0)
        {
            preferredXZDistance = Mathf.Sqrt(distance.x * distance.x + distance.z * distance.z);
        }
        if (preferredYDistance == 0)
        {
            preferredYDistance = distance.y;
        }
    }

    IEnumerator FollowTarget()
    {
        yield return LookForIdealPosition();
        yield return FloatTowardIdealPosition();
        yield return null;
    }

    IEnumerator LookForIdealPosition()
    {
        if (targetLookAt != null)
        {
            Vector3 targetPosition = targetLookAt.position;
            Vector3 targetForward;
            if (targetForwardLook != null)
            {
                targetForward = targetForwardLook.forward;
                targetForward.y = 0;
                targetForward.Normalize();
            }
            else
            {
                targetForward = (targetPosition - transform.position);
                targetForward.y = 0;
                targetForward.Normalize();
            }

            Vector3 newIdeal = targetPosition - (preferredXZDistance * targetForward) + new Vector3(0,preferredYDistance,0);
            idealPosition = newIdeal;
        }
        yield break;
    }

    IEnumerator FloatTowardIdealPosition()
    {
        if (stopFloating)
            yield break;

        Vector3 distance = (idealPosition - transform.position);

        float magnitude = distance.magnitude;
        collide.enabled = magnitude < dangerZoneFromGoal;

        if (magnitude > allowedDistanceFromGoal)
        {
            Vector3 direction = distance.normalized;
            Vector3 velocity = direction * cameraSpeed;

            body.velocity = velocity;
        }

        yield break;
    }
}
