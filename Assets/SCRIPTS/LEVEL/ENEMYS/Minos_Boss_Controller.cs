using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minos_Boss_Controller : MonoBehaviour
{
    [SerializeField]    public GameObject lastBeatrice;
    void FixedUpdate() 
    {
        if(MinosHead.instance.currentLife <= 0) 
        {
            lastBeatrice.SetActive(true);
            GetComponent<Animator>().SetBool("dead", true);
            Destroy(gameObject, 2f);
        }
    }
}
