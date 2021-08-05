using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [Header("DATA ====================================")] 
    [SerializeField]    public string[] sentences;
    [SerializeField]    public string[] actorsName;
    [SerializeField]    public int[] profilesPerSpeech;
    [SerializeField]    public bool[] sides;
    [SerializeField]    public string[] funcCall;
    [HideInInspector]    public int actualIndex = 0;

    [Header("CONFIGS ====================================")]
    [SerializeField]    public List<string> funcCallNames;
    [SerializeField]    public List<string> indexes;
    [SerializeField]    public bool alreadyFinished = false;
    [SerializeField]    public bool funcCalled = false;
    [SerializeField]    public int count = 0;
    [HideInInspector]   public static Dialogue instance;


    private void Start() 
    {
        funcCallNames.Clear();
        indexes.Clear();
        bool even = true;
        if (funcCall.Length > 0) 
        {
            for (int i = 0; i < funcCall.Length; i++)
            {
                if(even) indexes.Add(funcCall[i]);
                else funcCallNames.Add(funcCall[i]);
                even = !even;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player" && collider.isTrigger && !alreadyFinished)
        {
            instance = this;
            Dialogue_Controller.instance.index = 0;
            Dialogue_Controller.instance.Speech(profilesPerSpeech,sentences,actorsName);
        }
    }

    private void FixedUpdate() 
    {
        if (Dialogue.instance == this && funcCall.Length > 0) verifyFuncCall();
    }
    public void verifyFuncCall() 
    {
        actualIndex = Dialogue_Controller.instance.index;

        if (int.Parse(indexes[count]) == actualIndex && !funcCalled)
        {
            invokeFunc(funcCallNames[count]);
            if (count+1 < indexes.Count) count ++;
        }
    }
    private void invokeFunc(string s) 
    {
        Invoke(s, 0);
    }

    private void nextScene()
    {
        Game_Controller.instance.GoToNextScene();
        funcCalled = true;
    }

    private void triggerMinos() 
    {
        GetComponent<Trigger_Minos>().Trigger();
        funcCalled = true;
    }

    private void triggerStartWagon3()
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Mud_Slime_Spawner");
        foreach (var item in spawns)
        {
            item.GetComponent<Mud_Slime_Intantiate>().spawning = true;
        }
    }

    private void BossFight () 
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
