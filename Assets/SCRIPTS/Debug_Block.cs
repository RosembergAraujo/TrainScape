using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Block : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player" && collider.isTrigger)
        {
            // Player_Controller.instance.TakeDamage(20);
        }
    }
}
