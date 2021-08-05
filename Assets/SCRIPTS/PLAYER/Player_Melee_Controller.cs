using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Melee_Controller : MonoBehaviour
{  
    [SerializeField]    private int damage;
    [SerializeField]    private float atkDelay;
    [SerializeField]    public float detectionRadius;
    [SerializeField]    private bool canAtk = true;
    [HideInInspector]   public static Player_Melee_Controller instance;

    private void Awake() 
    {
        instance = this;
    }

    private void Update()
    {
        if (!Player_Controller.instance.isInLadder && Input.GetKeyDown(KeyCode.Z) && canAtk)
        {
            Player_Controller.instance.rig.velocity = new Vector2(0,Player_Controller.instance.rig.velocity.y);
            Player_Controller.instance.anim.SetBool("atk", true);
            Player_Controller.instance.canMove = false;
            canAtk = false;
            StartCoroutine(resetCanAtk());
        }
    }

    private IEnumerator resetCanAtk() 
    {
        yield return new WaitForSeconds(atkDelay);
        canAtk = true;
    }

    private void SetAtkOff()
    {
        Player_Controller.instance.anim.SetBool("atk", false);
    }

    private void DetectEnemy() 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Player_Shoot_Controller.instance.firePoint.transform.position, detectionRadius);
        foreach (Collider2D item in colliders)
        {  
            if (item.gameObject.tag != "Enemy") continue;
            item.GetComponent<Enemy_Controller>().TakeDamage(damage, true);
        }
    }

    private void CanMove() 
    {
        Player_Controller.instance.canMove = true;
    }
}

