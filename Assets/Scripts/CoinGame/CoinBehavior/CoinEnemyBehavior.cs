using System.Collections.Generic;
using UnityEngine;

public class CoinEnemyBehavior : MonoBehaviour
{
    public CoinBody body;

    private CoinBody playerEnemy;
    private List<CoinBody> enemies;

    public float startDelay = 5f;

    public float rateOfJump = 0.1f;
    public float chanceOfJump = 0.1f;

    private float jumpCoolDown = 0f;
    private float startCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startCooldown = startDelay;
        PollForEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (body != null)
        {
            if (playerEnemy == null)
            {
                body.isShooting = false;
                PollForEnemies();
            }

            if (playerEnemy != null)
            {
                body.isShooting = body.canJump && playerEnemy.canJump;

                if (jumpCoolDown <= 0f && body.canJump && Random.value < chanceOfJump)
                {
                    body.Jump(Mathf.PI / 4);
                    jumpCoolDown = rateOfJump * Random.Range(1f, 2f);
                }
                else
                {
                    body.LookAt(playerEnemy.transform.position);
                }

            }

            jumpCoolDown -= Time.deltaTime;
        }
    }

    private void PollForEnemies()
    {
        enemies = new List<CoinBody>(GameObject.FindObjectsOfType<CoinBody>());
        enemies.Remove(body);
        Shuffle(enemies);

        if (enemies.Count > 0)
        {
            playerEnemy = enemies[0];
        }
    }

    public static void Shuffle<T>(IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {
            n--;  
            int k = Random.Range(0, n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}
