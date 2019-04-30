using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPlayerBehavior : MonoBehaviour
{
    public CoinBody body;

    public enum MouseLookMode
    {
        MOUSE_TO_ROTATE,
        MOUSE_TO_AIM,
        MOUSE_TO_AIM_2
    }

    private float timeJumpHeld = 0.0f;

    private Ray mouseRay;

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0)
        {
            return;
        }
        if (body != null)
        {
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            LookToMouse();

            body.isShooting = Input.GetMouseButton(0);
            if (Input.GetMouseButton(0) && !body.canJump)
            {
                RaycastHit hit;
                // You successfully hi
                if (Physics.Raycast(mouseRay, out hit))
                {
                    body.playerShootPoint = hit.point;
                }
            }
            else
            {
                body.playerShootPoint = null;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                body.Jump(Mathf.PI / 3);
            }

            //if (!body.canJump)
            /*
            {
                Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * body.jumpStrength/20;

                body.body.AddForce(movement, ForceMode.Impulse);
            }
            */

        }
    }

    void LookToMouse()
    {
        MouseLookMode lookMode = GetLookMode();

        if (lookMode == MouseLookMode.MOUSE_TO_AIM)
        {
            // Cast a ray from screen point
            Ray ray = mouseRay;
            // Save the info
            RaycastHit hit;
            // You successfully hi
            if (Physics.Raycast(ray, out hit))
            {
                // Find the direction to move in
                Vector3 dir = hit.point - transform.position;

                // Make it so that its only in x and y axis
                dir.y = 0; // No vertical movement

                // Now move your character in world space 
                body.LookAt(hit.point);

                // transform.Translate (dir * Time.DeltaTime * speed); // Try this if it doesn't work
            }
            
        }
        else if (lookMode == MouseLookMode.MOUSE_TO_AIM_2)
        {
            // this creates a horizontal plane passing through this object's center
            var plane = new Plane(Vector3.up, body.transform.position);
            // create a ray from the mousePosition
            var ray = mouseRay;
            // plane.Raycast returns the distance from the ray start to the hit point
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                // some point of the plane was hit - get its coordinates
                Vector3 hitPoint = ray.GetPoint(distance);
                // use the hitPoint to aim your cannon

                // Now move your character in world space 
                body.LookAt(hitPoint);
            }
        }
        else
        {
            body.Rotate(Input.GetAxis("Mouse X") * 30f);
        }

    }

    private MouseLookMode GetLookMode()
    {
        return MouseLookMode.MOUSE_TO_AIM_2;
    }
}
