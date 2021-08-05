using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Controller : MonoBehaviour
{
    [SerializeField]    private List<GameObject> childrens;
    [SerializeField]    public GameObject bgPrefab;
    [HideInInspector]    public float sizeOf;
    [HideInInspector]    public float startPos;
    [HideInInspector]    public float diff;

    private void Awake() 
    {
        loadChildrens();   
    }
    private void Start() 
    {
        loadChildrens();
        startPos = childrens[1].transform.position.x;
        sizeOf = childrens[1].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void loadChildrens() 
    {
        foreach(Transform item in gameObject.transform){
            childrens.Add(item.gameObject);
        }
    }

    private void FixedUpdate() 
    {
        diff = startPos - childrens[1].transform.position.x;
        if(diff >= sizeOf)
        {
                Destroy(childrens[0]);
                if (childrens.Count <= 3) 
                {
                    GameObject obj = Instantiate(bgPrefab, new Vector3(childrens[childrens.Count - 1].transform.position.x + sizeOf - 1, transform.position.y, transform.position.z), transform.rotation);
                    obj.transform.SetParent(gameObject.transform);
                }
                childrens.Clear();
                loadChildrens();
        }
    }
}
