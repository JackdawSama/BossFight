using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameManager instance;

    public static GameManager FindInstance()
    {
        return instance;
    }
    void Awake() {
        //singleton to check for duplicates
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else if (instance == null) {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
