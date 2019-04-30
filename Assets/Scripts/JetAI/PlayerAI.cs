using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : JetFighterAI
{
    public GameObject gameWorld;
    public float positionLimitX;

    public PlayerBehavior behavior;

    public float acc = 20;

    internal List<EnemyDroneAI> liveEnemies;
    internal Vector2 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        PollForEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        behavior.Act(this);

        liveEnemies.RemoveAll(enemy => enemy == null);
    }

    public void PollForEnemies()
    {
        liveEnemies = new List<EnemyDroneAI>(gameWorld.GetComponentsInChildren<EnemyDroneAI>());
    }
}
