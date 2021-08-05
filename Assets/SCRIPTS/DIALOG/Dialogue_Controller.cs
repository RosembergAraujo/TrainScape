using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue_Controller : MonoBehaviour
{

    [HideInInspector]   public static Dialogue_Controller instance;
    private void Awake()
    {
        if(Dialogue_Controller.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += StartingNewScene;
        DontDestroyOnLoad(gameObject);
    }


    [Header("VARS")]
    [SerializeField]    public Sprite[] profiles;
    [SerializeField]    public string[] actorsNameText;
    [SerializeField]    public int[] profileIndex;
    [SerializeField]    public Image profileObj;
    [SerializeField]    public Text speechText;
    [SerializeField]    public Text actorNameTextObj;
    [SerializeField]    public GameObject dialogueObj;

    [Header("Settings")]
    [SerializeField, Range(0,.5f)]    public float typingSpeed;
    [SerializeField]    public bool finishedDialogue = false;
    [SerializeField]    public string[] sentences;
    [SerializeField]    public int index = 0;
    public void StartingNewScene(Scene a, LoadSceneMode mode){Refresh();}
    public void Refresh() 
    {
        profileObj = GameObject.FindGameObjectWithTag("Profile").GetComponent<Image>();
        speechText = GameObject.FindGameObjectWithTag("Speech").GetComponent<Text>();
        actorNameTextObj = GameObject.FindGameObjectWithTag("AN").GetComponent<Text>();
        dialogueObj = GameObject.FindGameObjectWithTag("DialogueInCanvas");
        // dialogueObj.GetComponent<Animator>().SetBool("visible", false);
    }
    
    public void Speech(int[] profileIndexParam, string[] sentencesI, string[] actorsName)
    {
        Player_Controller.instance.canMove = false;
        Player_Controller.instance.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        speechText.text = "";
        sentences = sentencesI;
        profileIndex = profileIndexParam;
        actorsNameText = actorsName;
        profileObj.sprite = profiles[profileIndex[index]];
        actorNameTextObj.text = actorsName[index];
        dialogueObj.GetComponent<Animator>().SetBool("visible", true);
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence() 
    {
        foreach (char item in sentences[index].ToCharArray())
        {
            speechText.text += item;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence() 
    {
        if (speechText.text == sentences[index])
        {
            if(index < sentences.Length -1)
            {
                index++;
                speechText.text = "";
                profileObj.sprite = profiles[profileIndex[index]];
                actorNameTextObj.text = actorsNameText[index];
                StartCoroutine(TypeSentence());
                Dialogue.instance.funcCalled = false;
            }
            else //FINISHED DIALOGUE
            {
                FinishDialogue();
            }
        } 
        // else //IF HAS NOT FINISHED TO TYPE ALL SENTENCE
        // {
        //     profileObj.sprite = profiles[profileIndex[index]];
        //     actorNameTextObj.text = actorsNameText[index];
        //     speechText.text = sentences[index];
        //     Dialogue.instance.funcCalled = false;
        // }
    }

    public void FinishDialogue() 
    {
        speechText.text = "";
        index = 0;
        dialogueObj.GetComponent<Animator>().SetBool("visible", false);
        Player_Controller.instance.canMove = true;
        Dialogue.instance.alreadyFinished = true;
        Dialogue.instance.count = 0;
        Dialogue.instance = null;
    }

}
