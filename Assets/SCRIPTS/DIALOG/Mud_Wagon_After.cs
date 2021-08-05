using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud_Wagon_After : MonoBehaviour
{
    [SerializeField]    public GameObject beatricePre;
    [SerializeField]    public bool active = false;

    void FixedUpdate() 
    {
        if(Game_Controller.instance.soulsCount >= 15 && !active) 
        {
            GameObject[] spawns = GameObject.FindGameObjectsWithTag("Mud_Slime_Spawner");
            foreach (var item in spawns)
            {
                item.GetComponent<Mud_Slime_Intantiate>().spawning = false;
            }
            beatricePre.SetActive(false);
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            active = true;
        }
    }
}
