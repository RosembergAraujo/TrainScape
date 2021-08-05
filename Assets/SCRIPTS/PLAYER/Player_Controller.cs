using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("VARS")]
    [SerializeField, Range(3,20)]    public float moveSpeed;
    [SerializeField, Range(15,30)]    private float jumpHeight;
    [SerializeField, Range(0,1)]    private float canJumpDelay;
    [SerializeField, Range(0,1)]    private float canTakeDamageDelay;
    [SerializeField, Range(0,1)]    private float ladderDetectionRadius;
    [SerializeField]    public float knockBackForce;
    [SerializeField]    public float gravityScale;
    [SerializeField]    public int targetHealth;
    [HideInInspector]   public float movementH;
    [HideInInspector]   public float movementV;
    [HideInInspector]   public float oldPos;
    
    [Header("BOOL")]
    [SerializeField]    public bool isFacingRight = true;
    [SerializeField]    private bool isUpsideDown = false;
    [SerializeField]    public bool isInLadder;
    [SerializeField]    public bool onFloor;
    [SerializeField]    public bool canTakeDamage = true;
    [SerializeField]    public bool canJump = true;
    [SerializeField]    public bool canMove = true;
    [SerializeField]    public bool isAtacking = false;
    [HideInInspector]   public bool canStopClimbAnim = false; //CONTROLLED BY ANIMATOR
    [HideInInspector]    private bool drawGizmos;
    
    [Header("OBJs")]
    [SerializeField]    private LayerMask groundMask;
    [SerializeField]    private LayerMask leaderMask;
    [HideInInspector]   public Rigidbody2D rig;
    [HideInInspector]   public Animator anim;
    [HideInInspector]   public static Player_Controller instance;
    
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        instance = this;
    }

    private void Start() 
    {
        targetHealth = Game_Controller.instance.maxHealth;
        oldPos = transform.position.y;
    }

    private void FixedUpdate() 
    {
        onFloor = Physics2D.Linecast(GameObject.Find("LegA").transform.position,GameObject.Find("LegB").transform.position, groundMask);
        isInLadder = Physics2D.OverlapCircle(transform.position, ladderDetectionRadius, leaderMask);
        
        #region 
            anim.SetBool("isInLadder", isInLadder);
            anim.SetBool("onFloor", onFloor);
        #endregion

        if(isInLadder) rig.gravityScale = 0;
        else rig.gravityScale = gravityScale;

        if(rig.velocity.y > 25) rig.velocity = new Vector2(rig.velocity.x, 17);
        else if(rig.velocity.y < -25) rig.velocity = new Vector2(rig.velocity.x, -17);

        if(canMove)
        {
            anim.SetFloat("velocityH", Mathf.Abs(movementH));
            Move();
            if(onFloor && !isInLadder) 
            {
                Jump();
                // Gravity();
            }   
        }
        else 
        {
            anim.SetFloat("velocityH", 0);
        }

        if(!isInLadder)
        {
            anim.speed = 1;
            canStopClimbAnim = false;
        }
        else  
        {
            if (Mathf.Abs(movementV) != 0 && !onFloor && Mathf.Abs(oldPos) != Mathf.Abs(transform.position.y)) 
            {anim.speed = 1;}
            else if(canStopClimbAnim){anim.speed = 0;}
        }  
        oldPos = transform.position.y;      
    }

    public void StopAnimSpeed() {canStopClimbAnim = true;}

    private void Update()
    {
        #region DEBUGS
        if(Input.GetKeyDown(KeyCode.P)) drawGizmos = !drawGizmos;
        if(Input.GetKeyDown(KeyCode.L)) TakeDamage(30);
        #endregion
    }
    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.blue;
            Debug.DrawLine(GameObject.Find("LegA").transform.position,GameObject.Find("LegB").transform.position);
            Gizmos.DrawWireSphere(transform.position, ladderDetectionRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Player_Shoot_Controller.instance.firePoint.transform.position, Player_Melee_Controller.instance.detectionRadius);
        }
    }

    private void Move()
    {
        movementH = Input.GetAxisRaw("Horizontal");
        movementV = Input.GetAxisRaw("Vertical");
        bool diagonal = Mathf.Abs(movementH) > 0 && Mathf.Abs(movementV) > 0 ? true : false;

        if (isInLadder)
        {
            rig.velocity = Vector2.zero;
            if(onFloor && movementH == 0) //NO CHAO SUBINDO 
            {
              rig.velocity = new Vector2(rig.velocity.x, movementV * moveSpeed);
            }
            else if (onFloor && movementV == 0) // NO CHAO ANDANDO PARA OS LADOS
            {
                rig.velocity = new Vector2(movementH * moveSpeed, rig.velocity.y);
            }
            else if (!onFloor && !diagonal)
            {
                rig.velocity = new Vector2(movementH * moveSpeed, movementV * moveSpeed); 
            }
        }
        else if (movementH != 0)
        {
            rig.velocity = new Vector2(movementH * moveSpeed, rig.velocity.y);
        }

        if(movementH > 0 && !isFacingRight && !(!onFloor && isInLadder))FlipH();
        else if (movementH < 0 && isFacingRight && !(!onFloor && isInLadder))FlipH();
    }

    private void Jump()
    {
        if (Input.GetButton("Jump") && onFloor && canJump)
        {
            rig.velocity = new Vector2(rig.velocity.x, 0);
            canJump = false;
            rig.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
            StartCoroutine(resetCanJump(canJumpDelay));
        }
    }

    private void Gravity() 
    {
        if(Input.GetKeyDown(KeyCode.F)) gravityScale*=-1;
        if(gravityScale < 0 && !isUpsideDown) FlipV();
        if(gravityScale >= 0 && isUpsideDown) FlipV();
    }

    private IEnumerator resetCanJump(float resetDelay) 
    {
        yield return new WaitForSeconds(resetDelay);
        canJump = true;
    }

    private void FlipH()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f,180f,0f);
    }
    private void FlipV()
    {
        isUpsideDown = !isUpsideDown;
        transform.Rotate(180f,0f,0f);
        jumpHeight *= -1;
    }

    public void TakeDamage(int damage) 
    {
        if (canTakeDamage) 
        {
            if (Game_Controller.instance.currentHealth > 0)
            {
                canTakeDamage = false;
                targetHealth -= damage;
                StartCoroutine(applyDamage(.01f));
                StartCoroutine(resetCanTakeDamage(canTakeDamageDelay));
            }
            else 
            {
                Game_Controller.instance.GoToNextScene(); //IF LIFE <= 0 HE DIES AFTER FADE OUT
                Game_Controller.instance.dead = true;
            }


        }
    }

    private IEnumerator resetCanTakeDamage (float delay) 
    {
        yield return new WaitForSeconds(delay);
        canTakeDamage = true;
    }

    private IEnumerator applyDamage(float delay) 
    {
        while (targetHealth < Game_Controller.instance.currentHealth)
        {
            Game_Controller.instance.currentHealth--;
            Game_Controller.instance.healthBar.SetHealth(Game_Controller.instance.currentHealth);
            yield return new WaitForSeconds(delay);
        }
    }
}
