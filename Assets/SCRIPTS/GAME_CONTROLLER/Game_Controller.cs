using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
    [HideInInspector]   public static Game_Controller instance;
    private void Awake()
    {
        if(Game_Controller.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadByScene;
        DontDestroyOnLoad(gameObject);
    }

    [Header("SETTINGS")]
    [SerializeField]    public HealthBar healthBar;
    [HideInInspector]   private int targetHealth;
    [HideInInspector]   public bool dead;

    [Header("STORAGE")]
    [SerializeField]    public string[] generalData;
    [SerializeField]    public int currentHealth; //PLAYERD DATA - 0
    [SerializeField]    public int maxHealth; //PLAYERD DATA - 1
    [SerializeField]    public int bibleAmmo; //PLAYERD DATA - 2
    [SerializeField]    public int soulsCount; //PLAYERD DATA - 3
    [SerializeField]    public int currentWagon;

    private void Start ()
    {
        healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<HealthBar>();
        currentHealth = maxHealth;
        targetHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void SaveState() 
    {
        string playerData = "";
        playerData += currentHealth + "|";
        playerData += maxHealth + "|";
        playerData += bibleAmmo + "|";
        playerData += soulsCount;

        PlayerPrefs.SetString("PlayerData", playerData);
        
    }

    public void LoadState() 
    {
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            generalData = PlayerPrefs.GetString("PlayerData").Split('|');

            currentHealth = int.Parse(generalData[0]);
            maxHealth = int.Parse(generalData[1]);
            bibleAmmo = int.Parse(generalData[2]);
            soulsCount = int.Parse(generalData[3]);
        }
    }

    private void LoadByScene(Scene a, LoadSceneMode mode)
    {
        LoadState();
        healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<HealthBar>();
        healthBar.SetHealth(currentHealth);
        if (dead) 
        {
            currentHealth = maxHealth;
            targetHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            dead = false;
        }
    }

    public void GoToNextScene() 
    {
        CrossFade.instance.gameObject.GetComponent<Animator>().SetTrigger("FadeOut");
        SaveState();
    }

}
