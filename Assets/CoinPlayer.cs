using UnityEngine;

public class CoinPlayer : MonoBehaviour
{
    public CameraLookAt cameraLookAt;

    internal CoinPlayerBehavior behavior;
    private CoinBody currentCoin;

    public static CoinPlayer instance;

    public int Health { get {
            if (currentCoin == null)
                return 0;
            return currentCoin.health;
        } }

    public CoinBody[] upgradePathPrefabs;

    private int currentUpgradeIndex = -1;

    public void UpgradeCoin()
    {
        currentUpgradeIndex++;
        CoinBody coinBody;

        if (currentCoin == null)
        {
            coinBody = Instantiate(upgradePathPrefabs[currentUpgradeIndex], transform.position, transform.rotation, null);
        }
        else
        {
            coinBody = Instantiate(upgradePathPrefabs[currentUpgradeIndex], currentCoin.transform.position + Vector3.up*0.1f,
                currentCoin.transform.rotation, null);

            currentCoin.Jump(90f);
            coinBody.body.velocity = currentCoin.body.velocity;
            coinBody.health = currentCoin.health;
        }

        Destroy(coinBody.GetComponent<CoinNPCBehavior>());
        behavior = coinBody.gameObject.AddComponent<CoinPlayerBehavior>();

        behavior.body = coinBody;
        cameraLookAt.targetLookAt = coinBody.transform;

        if (currentCoin != null)
            Destroy(currentCoin.gameObject);

        currentCoin = coinBody;
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpgradeCoin();
    }

    // Update is called once per frame
    void Update()
    {
        if (upgradePathPrefabs.Length > currentUpgradeIndex+1)
        {
            if (Health >= upgradePathPrefabs[currentUpgradeIndex + 1].health)
            {
                UpgradeCoin();
            }
        }
    }
}
