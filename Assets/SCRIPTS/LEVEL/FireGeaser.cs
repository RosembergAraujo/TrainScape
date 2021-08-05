using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGeaser : MonoBehaviour
{
    [SerializeField]    public int damage = 25;

    private void OnTriggerEnter2D (Collider2D collider) 
    {
        if(collider.tag == "Player") Player_Controller.instance.TakeDamage(damage);
        Player_Controller.instance.rig.AddForce(new Vector2(
                Player_Controller.instance.knockBackForce * 
                (Mathf.Sign(Player_Controller.instance.transform.position.x - transform.position.x)),
                Player_Controller.instance.knockBackForce * 2),
                ForceMode2D.Impulse
        );            
    }

}
