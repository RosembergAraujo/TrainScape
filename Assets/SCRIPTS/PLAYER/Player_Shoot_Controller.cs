using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot_Controller : MonoBehaviour
{
    [Header("VARS")]
    [SerializeField]    public int damage;
    [SerializeField]    public float tBibleForce;
    [SerializeField]    public float shootDelay;
    [Header("BOOL")]
    [SerializeField]    public bool canShoot = true;
    [Header("OBJ")]
    [SerializeField]    public GameObject firePoint;
    [SerializeField]    public GameObject tBiblePrefab;
    [HideInInspector]   public static Player_Shoot_Controller instance;

    private void Awake() 
    {
        instance = this;
        firePoint = GameObject.Find("PlayerFirePoint");
    }
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.X) && canShoot && Game_Controller.instance.bibleAmmo > 0) Shoot();
    }
    private void Shoot() 
    {
        GameObject tBible = Instantiate(tBiblePrefab,firePoint.transform.position,firePoint.transform.rotation);
        Rigidbody2D tBRig = tBible.GetComponent<Rigidbody2D>();
        tBRig.AddForce(firePoint.transform.right * tBibleForce, ForceMode2D.Impulse);
        Game_Controller.instance.bibleAmmo--;
        canShoot = false;
        StartCoroutine(ResetCanShoot());
        
    }

    private IEnumerator ResetCanShoot()
    {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
