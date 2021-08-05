using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Limit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
