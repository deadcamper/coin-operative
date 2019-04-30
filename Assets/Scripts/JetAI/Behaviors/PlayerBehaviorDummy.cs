using UnityEngine;

public class PlayerBehaviorDummy : PlayerBehavior
{
    private EnemyDroneAI focusEnemy;

    public override void Act(PlayerAI jetFighter)
    {
        if (focusEnemy == null)
        {
            jetFighter.jet.isShooting = false;

            if (jetFighter.liveEnemies.Count > 0)
            {
                focusEnemy = jetFighter.liveEnemies[0];
            }
        }
        else
        {
            jetFighter.targetPosition = focusEnemy.transform.localPosition;

            if (transform.localPosition.x > jetFighter.targetPosition.x + jetFighter.positionLimitX)
            {
                jetFighter.jet.isShooting = false;
                jetFighter.jet.velocity -= new Vector2(1, 0) * Time.deltaTime * jetFighter.acc;
            }
            else if (transform.localPosition.x < jetFighter.targetPosition.x - jetFighter.positionLimitX)
            {
                jetFighter.jet.isShooting = false;
                jetFighter.jet.velocity += new Vector2(1, 0) * Time.deltaTime * jetFighter.acc;
            }
            else
            {
                jetFighter.jet.isShooting = true;
            }
        }
    }

}
