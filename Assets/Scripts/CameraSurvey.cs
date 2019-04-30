using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSurvey : MonoBehaviour
{
    public Camera camera;
    public Vector3 startAngle, endAngle;

    public float timeToReachEnd = 5f;
    public float timeToWait = 1f;

    private Quaternion startQuat, endQuat;
    private float counter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startQuat = Quaternion.Euler(startAngle);
        endQuat = Quaternion.Euler(endAngle);

        camera.transform.rotation = startQuat;

        StartCoroutine(ScanRoomForever());
    }

    private IEnumerator ScanRoomForever()
    {
        while (true)
        {
            camera.transform.rotation = startQuat;
            yield return null;
            
            counter = 0f;
            while (counter < timeToWait)
            {
                counter += Time.deltaTime;
                yield return null;
            }

            counter = 0f;
            while (counter < timeToReachEnd)
            {
                counter += Time.deltaTime;
                camera.transform.rotation = Quaternion.Lerp(startQuat, endQuat, counter/timeToReachEnd);
                yield return null;
            }

            camera.transform.rotation = endQuat;
            yield return null;

            counter = 0f;
            while (counter < timeToWait)
            {
                counter += Time.deltaTime;
                yield return null;
            }

            counter = 0f;
            while (counter < timeToReachEnd)
            {
                counter += Time.deltaTime;
                camera.transform.rotation = Quaternion.Lerp(endQuat, startQuat, counter / timeToReachEnd);
                yield return null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
