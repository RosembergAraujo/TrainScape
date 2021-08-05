using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud_Slime : Follower_Enemy
{
    [SerializeField]    public bool canWalk = false;
    protected override void FixedUpdate() 
    {
        if(canGetHit && canWalk)
        {
            if(Mathf.Abs(transform.position.x - Player_Controller.instance.transform.position.x) < minDistanceToFollow * .35f) FollowPlayer(moveSpeed/2.5f);
            else FollowPlayer(moveSpeed);
        }
        if (MustFollowPlayer() && canWalk)
        {
            anim.SetBool("walk",true);
        }
    }

    public override void verifyDeath() 
    {
        if(currentLife <= 0)
        {
            Game_Controller.instance.soulsCount++;
            Destroy(gameObject);
        }
    }

    public void CanWalkAnim() {canWalk = true;}
}
