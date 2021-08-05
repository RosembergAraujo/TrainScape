using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    [Header("Storage")]
    [SerializeField]    public int damage;
    [SerializeField]    public float maxLife;
    [SerializeField]    public float currentLife;
    [SerializeField]    public float knockBackForce;
    [SerializeField]    public float playerKnockBackForce;
    [SerializeField, Range(0, 1)]    public float canGetHitDelay;
    [SerializeField]    public bool canGetHit = true;
    [Header("Settings")]
    [HideInInspector]   public bool drawGizmos;
    [Header("OBJ")]
    [HideInInspector]   public GameObject target;
    [HideInInspector]   public Rigidbody2D rig;
    [HideInInspector]   public Animator anim;

    protected virtual void Awake() 
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").gameObject;
        currentLife = maxLife;
    }

    protected virtual void Update()
    {
        #region DEBUGS
        if(Input.GetKeyDown(KeyCode.P)) drawGizmos = !drawGizmos;
        #endregion
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && Player_Controller.instance.canTakeDamage) 
        {
            Player_Controller.instance.TakeDamage(damage);
            if(damage != 0) 
            {
                Player_Controller.instance.rig.AddForce(new Vector2(
                    Player_Controller.instance.knockBackForce * 
                    (Mathf.Sign(Player_Controller.instance.transform.position.x - transform.position.x)),
                    Player_Controller.instance.knockBackForce * 2),
                    ForceMode2D.Impulse
                );
            }            
        }
    }

    public virtual void TakeDamage(int damage, bool knock) 
    {
        if(canGetHit) 
        {
            if(knock)
            {
                rig.AddForce(
                new Vector2(
                    knockBackForce * (Mathf.Sign(
                        transform.position.x - Player_Controller.instance.transform.position.x
                        )),0),
                        ForceMode2D.Impulse);
                canGetHit = false;
                StartCoroutine(resetcanGetHit(canGetHitDelay));
            }
            else 
            {
                canGetHit = false;
                StartCoroutine(resetcanGetHit(canGetHitDelay * 3));
            }
            anim.SetTrigger("hit");
            currentLife -= damage;
            CancelInvoke();
            Invoke("verifyDeath", 3f);
        }
        
    }

    public virtual void verifyDeath() 
    {
        if(currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator resetcanGetHit(float delay) 
    {
        yield return new WaitForSeconds(delay);
        canGetHit = true;
    }
}
