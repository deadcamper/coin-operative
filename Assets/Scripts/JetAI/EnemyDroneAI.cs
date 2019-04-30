using UnityEngine;

public class EnemyDroneAI : JetFighterAI
{
    public Vector2 targetBounds;

    public float acc;

    public float direction;

    // Start is called before the first frame update
    void Start()
    {
        jet.isShooting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x < targetBounds.x)
        {
            direction = +1;
        }
        else if (transform.localPosition.x > targetBounds.y)
        {
            direction = -1;
        }
        else
        {
            direction = 0;
        }

        jet.velocity += direction * new Vector2(1, 0) * Time.deltaTime * acc;
    }
}
