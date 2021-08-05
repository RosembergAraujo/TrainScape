using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField]    public float speed;
    void FixedUpdate() 
    {
        transform.Rotate(0,0,speed);
    }
}
