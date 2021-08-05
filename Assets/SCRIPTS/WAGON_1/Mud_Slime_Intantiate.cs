using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud_Slime_Intantiate : MonoBehaviour
{
    [SerializeField]    public bool spawning = true;
    [SerializeField]    public float timeToSpawn;
    [SerializeField]    public GameObject mudSlimePrefab;
    [HideInInspector]   public Animator anim;

    void Start() {anim = GetComponent<Animator>(); StartCoroutine(SpawningLoop());}
    public IEnumerator SpawningLoop () 
    {
        while (true)
        {
            float delay = Random.Range(timeToSpawn * .25f, timeToSpawn);
            yield return new WaitForSeconds(delay);
            if (spawning) anim.SetTrigger("Spawn");      
        }
    }

    public void spawnByAnim() {Instantiate(mudSlimePrefab, transform.position, transform.rotation);}
}
