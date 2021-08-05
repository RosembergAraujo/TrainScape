using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinosHead : Enemy_Controller
{
    [SerializeField]    public static MinosHead instance;
    [SerializeField]    public GameObject tBiblePrefab;

    protected override void Awake() 
    {
        base.Awake();
        instance = this;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        return;
    }

    public override void TakeDamage(int damage, bool knock)
    {
        if(canGetHit) 
        {
            canGetHit = false;
            StartCoroutine(resetcanGetHit(canGetHitDelay * 3));
            anim.SetTrigger("hit");
            currentLife -= damage;
            CancelInvoke();
            Invoke("verifyDeath", 3f);
            GameObject tBible = Instantiate(tBiblePrefab, transform.position, transform.rotation);
        }
    }
}
