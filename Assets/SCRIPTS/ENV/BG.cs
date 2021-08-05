using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    [SerializeField]    private float scrollSpeed;
    private void FixedUpdate() 
    {
        transform.position = new Vector2(transform.position.x - scrollSpeed, transform.position.y);
    }
}
