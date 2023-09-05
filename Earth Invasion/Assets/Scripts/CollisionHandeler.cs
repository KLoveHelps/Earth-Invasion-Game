using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class CollisionHandeler : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] float CodeDelay = 2f;

    [SerializeField] AudioClip win;
    [SerializeField] ParticleSystem winParticles;

    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem looseParticles;


    bool isTransitioning = false;
    bool CollisionDisabled = false;
    public GameObject GameOverUI;
    public GameObject WinUI;
    public GameObject ScoreUI;

    ScoreBoard scoreBoardScript;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scoreBoardScript = GameObject.FindGameObjectWithTag("ScoreVal").GetComponent<ScoreBoard>();

    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(this.name + "--Collided with--" + other.gameObject.name);
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



