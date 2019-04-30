using UnityEngine;

public class LevelStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1f;
        CoinNPCBehavior.isGroupFrenzy = false;
        if(CoinPlayer.instance == null)
        {
            CoinPlayer.instance = null;
        }
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
