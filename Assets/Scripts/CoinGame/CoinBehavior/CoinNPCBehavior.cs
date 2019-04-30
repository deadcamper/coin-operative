using System.Collections;
using UnityEngine;

public class CoinNPCBehavior : MonoBehaviour
{
    public CoinBody controllingBody;

    public static bool isGroupFrenzy = false;
    internal CoinBody enemy;

    internal bool isFrenzy = false;

    public const int enemyCooldown = 10;
    private int enemyCountdown = 10;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoNPCStuff());
    }

    public IEnumerator DoNPCStuff()
    {
        while (this)
        {
            yield return new WaitWhile(GameIsPaused);

            if (CoinPlayer.instance == null)
            {
                yield return null;
                continue;
            }

            // Do aggro check
            if ((isGroupFrenzy || isFrenzy))
            {
                if (enemy == null)
                    enemy = CoinGame.instance.livePlayers[Random.Range(0,CoinGame.instance.livePlayers.Length)];

                if(enemy == controllingBody)
                {
                    enemy = null;
                }
            }

            if (enemy != null)
            {       
                yield return LookAtEnemy(40);
                yield return Shoot(50);

                if (Random.value > 0.3f)
                {
                    yield return LookAround(20);
                    yield return Jump(20);
                }

                enemyCountdown -= 1;
                if (enemyCountdown <= 0)
                {
                    enemy = null;
                    enemyCountdown = enemyCooldown;
                }
            }

            // Neutral mode
            if (!enemy && !isGroupFrenzy)
            {
                if (Random.value > 0.2f)
                {
                    yield return LookAround(50);
                }
                else if (Random.value > 0.2f)
                {
                    yield return CoolDown(20);
                }
                else if (Random.value > 0.5f)
                {
                    yield return Jump(20);
                }
                else if (Random.value > 0.8f)
                {
                    yield return Shoot(30);
                }
            }

            yield return null;
        }
    }

    public IEnumerator Jump(int maxCooldown)
    {
        float angle = Random.Range(Mathf.PI / 6, Mathf.PI / 2);
        controllingBody.Jump(angle);
        yield return CoolDown(maxCooldown);
    }

    public IEnumerator CoolDown(int maxFrames)
    {
        int numFrames = Random.Range(0, maxFrames);
        for (int n = 0; n < numFrames; n++)
        {
            yield return null;
        }
    }

    public IEnumerator LookAround(int maxFrames)
    {
        //float angle = Random.Range(0f, 360f);
        int numFrames = Random.Range(0, maxFrames);
        float rotSpeed = Random.Range(-80 / 60, 80 / 60);

        for (int n = 0; n < numFrames; n++)
        {
            controllingBody.Rotate(rotSpeed);
            yield return null;
        }
    }

    public IEnumerator LookAtEnemy(int maxFrames)
    {
        int numFrames = Random.Range(0, maxFrames);

        for (int n = 0; n < numFrames; n++)
        {
            if (!enemy)
            {
                break;
            }
            controllingBody.LookAt(enemy.transform.position);
            yield return null;
        }
    }

    public IEnumerator Shoot(int maxFrames)
    {
        int numFrames = Random.Range(0, maxFrames);

        for (int n = 0; n < numFrames; n++)
        {
            controllingBody.isShooting = true;
            yield return null;
        }
        controllingBody.isShooting = false;
    }

    public void OnDestroy()
    {
        StopAllCoroutines();
    }

    public bool GameIsPaused()
    {
        return Time.timeScale < 0.01f;
    }
}
