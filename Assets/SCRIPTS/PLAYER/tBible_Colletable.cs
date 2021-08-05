using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tBible_Colletable : MonoBehaviour
{
    public bool collected = false;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player" && collider.isTrigger && !collected)
        {
            collected = true;
            Game_Controller.instance.bibleAmmo ++;
            try
            {
                GetComponent<Animator>().SetTrigger("Collected");
            }
            catch (System.Exception)
            {
                return;
            }
        }
    }

    public void DestroyThis() 
    {
        Destroy(gameObject);
    }
}
