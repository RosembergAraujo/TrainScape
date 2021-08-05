using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diabretes : Follower_Enemy
{
    [Header("DIABRETEEEE")]
    [SerializeField]    public bool canWalk = true;
    [SerializeField]    public bool isAtacking = false;
    [SerializeField]    public bool canAtk = true;
    [SerializeField]    public float atkRange;
    [SerializeField]    public LayerMask playerLayer;

    protected override void FixedUpdate() 
    {
        isAtacking = Physics2D.OverlapCircle(transform.position, atkRange, playerLayer);
        if (!isAtacking) 
        {
            if(canGetHit && canWalk && currentLife > 0)
            {
                if(Mathf.Abs(transform.position.x - Player_Controller.instance.transform.position.x) < minDistanceToFollow * .2f) FollowPlayer(moveSpeed*.7f);
                else FollowPlayer(moveSpeed);
                if (MustFollowPlayer()) anim.SetBool("walk",true);
            }
            
        }
        else if(canAtk)
        {
            anim.SetTrigger("atk");
            canAtk = false;
        }
    }

    public void endOfAtkAnimation()
    {
        canAtk = true;
    }

    public void applyDamage() 
    {
        if (isAtacking) 
        {
            Player_Controller.instance.TakeDamage(damage);
        }
    }

    protected override void OnDrawGizmos() 
    {
        if (base.drawGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, minDistanceToFollow);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, atkRange);
        }

    }
}
