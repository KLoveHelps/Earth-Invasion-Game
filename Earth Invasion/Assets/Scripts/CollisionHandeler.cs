using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class CollisionHandeler : MonoBehaviour
{
    AudioSource audioSource;
    
    [SerializeField] float CodeDelay = 2f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;

    [SerializeField] AudioClip win;
    [SerializeField] ParticleSystem winParticles;
    [SerializeField] int PlayerhitPoints = 25;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem looseParticles;

    
    bool isTransitioning = false;
    bool CollisionDisabled = false;
    public GameObject GameOverUI;
    public GameObject WinUI;
    public GameObject ScoreUI;
   
    ScoreBoard scoreBoard;
    GameObject parentGameObject;
    ScoreBoard scoreBoardScript;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scoreBoardScript = GameObject.FindGameObjectWithTag("ScoreVal").GetComponent<ScoreBoard>();
        
    }

    void OnCollisionEnter(Collision other)
    {
        ProcessHitPlayer();
        if (PlayerhitPoints < 1)
        {
            killPlayer();
        }
        else
        {
            Debug.Log(this.name + "--Collided with--" + other.gameObject.name);
        }

    }
    void ProcessHitPlayer()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        PlayerhitPoints--;
    }
    void killPlayer()
    {
        audioSource.PlayOneShot(death);
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
        private void OnTriggerEnter(Collider other)
    {
        if (isTransitioning || CollisionDisabled)
        { 
            return; 
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("this thing is Friendly");
                break;
            case "Win":
                CheckScore();
                break;
            

            default:
                StartGameOverSequence();
                break;
        }
    }

    void CheckScore()
    {
        if (scoreBoardScript.score <= 0)
        {
            StartGameOverSequence();
        }
        else
        {
            StartWinSequence();
        }

    }    
    void StartWinSequence()
    {
        isTransitioning = true;
        winParticles.Play();
        audioSource.PlayOneShot(win);
        WinUI.SetActive(true);
        GetComponent<PlayerControler>().enabled = false;
        
    }
    void StartGameOverSequence()
    {
        isTransitioning = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        
        looseParticles.Play();
        audioSource.PlayOneShot(death);
        GameOverUI.SetActive(true);
        Invoke("turnoffplayerinput", CodeDelay);
        


        
    }
 

    void turnoffplayerinput()
    {
        GetComponent<PlayerControler>().enabled = false;
        Time.timeScale = 0f;
    }
}



