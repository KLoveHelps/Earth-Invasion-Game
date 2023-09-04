using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    
    const string MasterVolume = "MasterVolume";
    

    public static AudioManager instance;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
  


}

