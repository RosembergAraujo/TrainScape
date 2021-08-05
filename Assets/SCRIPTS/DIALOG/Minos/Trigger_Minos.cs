using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Minos : MonoBehaviour
{
    [SerializeField]    public GameObject beatriceObj;
    [SerializeField]    public GameObject ladderBlock;
    public void Trigger() 
    {
        beatriceObj.GetComponent<SpriteRenderer>().enabled = true;
        ladderBlock.GetComponent<BoxCollider2D>().enabled = false;
    }
}
