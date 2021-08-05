using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating_Platform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player"){collider.gameObject.transform.SetParent(transform);}
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player"){collider.gameObject.transform.SetParent(null);}
    }
}
