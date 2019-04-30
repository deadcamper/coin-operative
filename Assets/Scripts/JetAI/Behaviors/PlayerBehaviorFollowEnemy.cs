using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorFollowEnemy : PlayerBehavior
{
    private EnemyDroneAI focusEnemy;

    private Vector2 focus;

    private bool lastSeenEnemy = false;

    public override void Act(PlayerAI jetFighter)
    {
        if (focusEnemy == null)
        {
            if (lastSeenEnemy)
            {
                jetFighter.jet.isShooting = false;
                jetFighter.targetPosition = new Vector2(0, 0);
                jetFighter.jet.velocity = jetFighter.jet.velocity / 3;

                lastSeenEnemy = false;
            }
            
            if (jetFighter.liveEnemies.Count > 0)
            {
                focusEnemy = jetFighter.liveEnemies[0];
                lastSeenEnemy = true;
            }
        }
        else
        {
            Vector2 velocity = focusEnemy.jet.body.velocity;
            float acc = focusEnemy.direction * focusEnemy.acc;
            
            float distanceY = (focusEnemy.transform.localPosition - transform.localPosition).y;
            float bulletVelocity = jetFighter.jet.bulletVelocity.y;

            float dt = distanceY / bulletVelocity;

            focus = new Vector2(0.5f * acc * dt * dt + velocity.x * dt + focusEnemy.transform.localPosition.x, transform.localPosition.y);

            jetFighter.targetPosition = focus;
            
            jetFighter.jet.isShooting = true;

            /*
            if (transform.localPosition.x > jetFighter.targetPosition.x + jetFighter.positionLimitX)
            {
                jetFighter.jet.isShooting = false;
            }
            else if (transform.localPosition.x < jetFighter.targetPosition.x - jetFighter.positionLimitX)
            {
                jetFighter.jet.isShooting = false;
            }
            else
            {
            }
            */
        }

        if (transform.localPosition.x > jetFighter.targetPosition.x)
        {
            //jetFighter.jet.isShooting = false;
            jetFighter.jet.velocity -= new Vector2(1, 0) * Time.deltaTime * jetFighter.acc;
        }
        else if (transform.localPosition.x < jetFighter.targetPosition.x)
        {
            //jetFighter.jet.isShooting = false;
            jetFighter.jet.velocity += new Vector2(1, 0) * Time.deltaTime * jetFighter.acc;
        }
    }
}
