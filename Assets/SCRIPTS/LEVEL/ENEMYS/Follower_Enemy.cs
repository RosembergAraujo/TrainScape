using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower_Enemy : Enemy_Controller
{
    [SerializeField]    public float moveSpeed;
    [SerializeField]    public float minDistanceToFollow;

    protected virtual void OnDrawGizmos() 
    {
        if (base.drawGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, minDistanceToFollow);
        }
    }

    protected virtual void FixedUpdate() 
    {
        if(canGetHit)
        {
            if(Mathf.Abs(transform.position.x - Player_Controller.instance.transform.position.x) < minDistanceToFollow * .35f) FollowPlayer(moveSpeed/2.5f);
            else FollowPlayer(moveSpeed);
        }
    }
    public void FollowPlayer(float speed)
    {
        if (MustFollowPlayer())
        {
            transform.position = new Vector2(
                Vector2.MoveTowards(transform.position, target.transform.position, speed).x,
                transform.position.y
            );
        }
    }

    public bool MustFollowPlayer() => Vector2.Distance(
        target.transform.position, transform.position) < minDistanceToFollow;
}
