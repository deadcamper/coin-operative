using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D body;
    public JetBody parent;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        JetBody fighter = collision.GetComponent<JetBody>();

        if (fighter != parent && fighter != null)
        {
            fighter.Hit();
            Destroy(gameObject);
        }
    }
}
