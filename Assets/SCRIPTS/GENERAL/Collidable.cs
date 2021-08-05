using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// [RequireComponent(typeof(BoxCollider2D))] //REQUER O COMPONENTE DAHORA DE USAR
public class Collidable : MonoBehaviour
{
    [SerializeField]    private ContactFilter2D filter;
    [HideInInspector]   private BoxCollider2D boxCollider;
    [HideInInspector]   private Collider2D[] hits = new Collider2D[10];
    
    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    protected virtual void FixedUpdate()
    {
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i] == null) continue;

            OnCollide(hits[i]);

            hits[i] = null;
        }
    }
    protected virtual void OnCollide(Collider2D collider)
    {
        Debug.Log(collider.name);
    }
    

}
