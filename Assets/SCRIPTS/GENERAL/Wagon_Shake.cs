using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon_Shake : MonoBehaviour
{
    [SerializeField]    public float timeToShake;
    void Start()
    {
        StartCoroutine(shake());        
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator shake () 
    {
        yield return new WaitForSeconds(timeToShake);
        int r = Random.Range(1,3);
        if (r == 1) GetComponent<Animator>().SetTrigger("shake1");
        if (r == 2) GetComponent<Animator>().SetTrigger("shake2");
        StartCoroutine(shake());  
    }
}
