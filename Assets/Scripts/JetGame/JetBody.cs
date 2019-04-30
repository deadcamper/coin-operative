using UnityEngine;

public class JetBody : MonoBehaviour
{
    public GameObject playingField;

    public Rigidbody2D body;

    public Vector2 velocity;
    public bool isShooting;

    public float shootCooldown = 0.5f;
    public Vector2 bulletVelocity;

    private float shootCountdown = 0f;

    public Bullet bulletTemplate;

    public int health = 3;

    private void Awake()
    {
        bulletTemplate.gameObject.SetActive(false);
        body.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 20 == 0)
        {
            velocity = body.velocity;
        }

        shootCountdown -= Time.deltaTime;
        shootCountdown = Mathf.Clamp(shootCountdown, 0, shootCooldown);

        if (isShooting && shootCountdown == 0f)
        {
            Shoot();
            shootCountdown = shootCooldown;
        }
    }

    private void LateUpdate()
    {
        body.velocity = velocity;
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletTemplate, transform.position, Quaternion.identity, null);
        bullet.parent = this;
        bullet.gameObject.SetActive(true);
        bullet.body.velocity = bulletVelocity;
    }

    public void Hit()
    {
        health -= 1;

        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
