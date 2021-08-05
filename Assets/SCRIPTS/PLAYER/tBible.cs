using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tBible : MonoBehaviour
{
    [SerializeField]    public float velocityY;
    [SerializeField]    public float lifeTime;
    [SerializeField]    public float rotationSpeed;
    void Start()
    {
        Invoke("selfDestroy", lifeTime);
    }
    
    void selfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D (Collider2D collider) 
    {
        
        if(collider.tag == "Enemy") 
        {
            CancelInvoke();
            collider.GetComponent<Enemy_Controller>().TakeDamage(Player_Shoot_Controller.instance.damage, false);
            GetComponent<Animator>().SetTrigger("hit");
            // GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

}
