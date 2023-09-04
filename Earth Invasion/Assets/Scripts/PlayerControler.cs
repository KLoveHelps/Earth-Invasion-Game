using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    AudioSource audioSource;
    [Header("General Input settings")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction Atack;
    [SerializeField] InputAction pause;
    [Header("Movement Speed")]
    [Tooltip("How fast ShipMoves within Moveable Area")]
    [SerializeField] float ControlSpeed = 5f;

    [Header("Moveable Area")]
    [SerializeField] float xRange = 3f;
    [SerializeField] float yRange = 1.5f;
    [Header("position off input tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = -10f;
    [Header("Rotaion off input tuning")]
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float controlPitchFactor = -25f;

    [Header("Laser Guns")]
    [Tooltip("add Player Lazer Guns here")]
    [SerializeField] GameObject[] lasers;
    [SerializeField] AudioClip lazers; 
    [SerializeField] float CodeDelay = 0.5f;

    bool isloading = false; 


    float xThrow, yThrow;

    public bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        movement.Enable();
        Atack.Enable();
        pause.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        Atack.Disable();
        pause.Disable();
    }
 
    // Update is called once per frame
    void Update()
    {
        PauseMenu();
        ProcessTranslation();
        ProcessRotation();
        ProcessFireing();
    }
    public void PauseMenu()
    {
        if (isloading == true) 
        {
            return;
        }
        else if (pause.ReadValue<float>() > .5)
        {
            
            if (GameIsPaused)
            {
                Resume();
                
            }
            else
            {
                Pause();
                
            }
        }
        
    }

   public void Resume()
    {
        isloading = true;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Invoke("stoptranstion", CodeDelay);
    }

    void Pause()
    {
        isloading = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Invoke("stoptranstion", CodeDelay);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float Pitch = pitchDueToPosition + pitchDueToControlThrow;
        float Yaw = transform.localPosition.x * positionYawFactor;
        float Roll = xThrow * controlRollFactor;


        transform.localRotation = Quaternion.Euler(Pitch, Yaw, Roll );
    }


    private void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * ControlSpeed;
        float rawXpos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXpos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * ControlSpeed;
        float rawYpos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYpos, -yRange, yRange);


        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFireing()
    {
        if (Atack.ReadValue<float>() > 0.5)
        {
            SetLasersActive(true);
            audioSource.PlayOneShot(lazers);
        }
        else
        {
            SetLasersActive(false);
        }

    }

    private void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
    void stoptranstion()
    {
       
            isloading = false;
     

    }

}
