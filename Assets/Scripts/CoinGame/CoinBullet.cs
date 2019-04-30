using UnityEngine;

public class CoinBullet : MonoBehaviour
{
    public float bulletSpeed = 40f;

    public float timeToLive = 5.0f;

    public Rigidbody body;
    public CoinBody parent;

    public void OnTriggerEnter(Collider collision)
    {
        CoinBody fighter = collision.GetComponent<CoinBody>();
        if (fighter == null)
        {
            fighter = collision.GetComponentInParent<CoinBody>();
        }

        if (fighter != null)
        {
            if (fighter != parent)
            {
                fighter.Hit();
                Destroy(gameObject);

                CheckUpdatePlayer();
                CheckUpdateTarget(fighter);
            }
        }
    }

    public void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive < 0)
        {
            Destroy(gameObject);
        }
    }

    private void CheckUpdatePlayer()
    {
        if (CoinPlayer.instance?.behavior != null && parent == CoinPlayer.instance.behavior.body)
        {
            parent.health++;
        }
    }

    private void CheckUpdateTarget(CoinBody fighter)
    {
        CoinNPCBehavior npc = fighter.GetComponent<CoinNPCBehavior>();
        if (npc)
        {
            npc.enemy = parent;
        }
    }
}
