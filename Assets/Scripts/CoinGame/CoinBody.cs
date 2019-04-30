using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBody : MonoBehaviour
{
    public GameObject playingField;

    public Rigidbody body;
    public Transform visual;
    public Transform dirVisual;
    public MeshCollider collision;

    public string coinName;

    public Transform bulletSpawnPoint;

    //public Vector2 velocity;
    public bool isShooting;

    public float shootCooldown = 0.5f;
    public Vector3 facingDirection;

    public float jumpCooldown = 1f;

    public float jumpStrength = 400f;

    private float shootCountdown = 0f;
    private float jumpCountdown = 0f;

    internal bool canJump = false;

    public CoinBullet bulletTemplate;

    public AudioSource shootSound;
    public AudioSource hitSound;
    public AudioSource flipSound;

    private Vector3 lastPosition;

    public Vector3? playerShootPoint;

    public int health = 3;

    internal HashSet<Collider> currentCollisions = new HashSet<Collider>();

    private void Awake()
    {
        if (bulletTemplate != null && bulletTemplate.gameObject.scene != null)
        {
            bulletTemplate.gameObject.SetActive(false);
        }
        //body.velocity = velocity;
        shootCountdown = shootCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //velocity = body.velocity;

        shootCountdown -= Time.deltaTime;
        shootCountdown = Mathf.Clamp(shootCountdown, 0, shootCooldown);

        jumpCountdown -= Time.deltaTime;
        jumpCountdown = Mathf.Clamp(jumpCountdown, 0, jumpCooldown);

        if (isShooting && shootCountdown == 0f)
        {
            Shoot();
            shootCountdown = shootCooldown;
        }
    }

    private void LateUpdate()
    {
        //body.velocity = velocity;
        dirVisual.gameObject.SetActive(canJump);
    }

    private void OnCollisionEnter(Collision coll)
    {
        currentCollisions.Add(coll.collider);
        if (canJump != true)
        {
            Debug.Log(gameObject.name + " Landed!");
        }
        canJump = true;
    }

    private void OnCollisionExit(Collision coll)
    {
        currentCollisions.Remove(coll.collider);
        if (currentCollisions.Count == 0)
        {
            canJump = false;
        }
    }

    private void Shoot()
    {
        shootSound.Stop();
        shootSound.Play();

        CoinBullet bullet = Instantiate(bulletTemplate, bulletSpawnPoint.position, transform.rotation, null);
        bullet.parent = this;
        bullet.gameObject.SetActive(true);

        Vector3 forward = playerShootPoint != null ? (playerShootPoint.Value - bulletSpawnPoint.position).normalized : visual.transform.forward;

        bullet.body.velocity = forward * bullet.bulletSpeed;
    }

    public void Hit()
    {
        hitSound.Play();
        health -= 1;

        if (health <= 0)
        {
            StartCoroutine(ScheduleForCleanup());
            GetComponent<CoinNPCBehavior>()?.StopAllCoroutines();
            visual.gameObject.SetActive(false);
        }
    }

    public void LookAt(Vector3 target)
    {
        facingDirection = target;

        Vector3 facingY = new Vector3(target.x, transform.position.y, target.z);
        
        Quaternion rot = Quaternion.LookRotation(target - transform.position, Vector3.up);

        Quaternion localRot = Quaternion.Inverse(transform.rotation) * rot;
        Vector3 eul = localRot.eulerAngles;
        
        eul = new Vector3(0, eul.y, 0);

        visual.transform.localEulerAngles = eul;
    }

    public void Rotate(float amount)
    {
        visual.Rotate(Vector3.up, amount);
    }

    public void Jump(float angle)
    {
        if (canJump && jumpCountdown <= 0f)
        {
            if (GetComponent<CoinPlayerBehavior>())
                flipSound.Play();

            float sine = Mathf.Sin(angle);
            float cosine = Mathf.Cos(angle);
            body.AddForce(Vector3.up*jumpStrength*sine, ForceMode.Acceleration);
            body.AddForce(visual.forward*jumpStrength*cosine, ForceMode.Acceleration);

            Vector3 randomTorque = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            body.AddRelativeTorque(randomTorque, ForceMode.Acceleration);

            body.AddTorque(visual.right * 250 * Mathf.Sign(visual.up.y), ForceMode.VelocityChange);

            jumpCountdown = jumpCooldown;
            canJump = false;
        }
    }

    IEnumerator ScheduleForCleanup()
    {
        yield return new WaitForSecondsRealtime(0.15f);
        Destroy(gameObject);
    }
}
