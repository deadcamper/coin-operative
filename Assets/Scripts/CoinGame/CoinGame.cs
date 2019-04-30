using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinGame : MonoBehaviour
{
    public int expectedLivePlayerCount = 10;

    public static CoinGame instance;

    public CoinGameUI ui;

    public List<CoinSpawnPoint> spawnPoints;
    public List<CoinBody> templates;

    internal CoinBody[] livePlayers;

    public List<float> biases;

    private int highestHealth = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        spawnPoints = new List<CoinSpawnPoint>(FindObjectsOfType<CoinSpawnPoint>());

        StartCoroutine(PulseCheck());
    }

    // Update is called once per frame
    void Update()
    {
        int nextHighestHealth = Mathf.Max(highestHealth, CoinPlayer.instance.Health);

        if (nextHighestHealth > highestHealth)
        {
            OnMaxHealthChange(nextHighestHealth);
        }

        highestHealth = nextHighestHealth;
    }

    IEnumerator PulseCheck()
    {
        while(true)
        {
            livePlayers = FindObjectsOfType<CoinBody>();
            if (livePlayers.Length < expectedLivePlayerCount)
            {
                float totalBias = biases.Sum();

                float selected = Random.Range(0, totalBias);

                int index = -1;
                
                do {
                    index++;
                    selected -= biases[index];
                } while (selected > 0);

                CoinBody template = templates[index];
                CoinSpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

                spawnPoint.Spawn(template);
            }
            
            yield return new WaitForSeconds(2.5f);
        }
    }

    void OnMaxHealthChange(int health)
    {
        if (health < 1)
        {
            return;
        }

        if (health > 1 && health % 50 == 0)
        {
            CoinNPCBehavior.isGroupFrenzy = true; //forever frenzy!
        }

        if (health > 10)
        {
            if (Random.value < 0.1f)
            {
                int numFrenzy = health / 7; // Arbitrary math magic
                for (int i = 0; i < numFrenzy; i++)
                {
                    CoinNPCBehavior behave = livePlayers[Random.Range(0, livePlayers.Length)].GetComponent<CoinNPCBehavior>();
                    if (behave != null)
                    {
                        behave.isFrenzy = true;
                    }
                }
            }
            
            expectedLivePlayerCount++;
        }
    }

    void OnPlayerDeath()
    {

    }
}
