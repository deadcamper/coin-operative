using UnityEngine;

public class CoinSpawnPoint : MonoBehaviour
{

    public Vector3 trajectory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(CoinBody template)
    {
        Rigidbody item = Instantiate(template, transform.position, transform.rotation, null).body;
        item.AddForce(trajectory, ForceMode.VelocityChange);
    }
}
