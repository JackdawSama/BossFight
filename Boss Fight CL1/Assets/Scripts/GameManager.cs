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

    public GameObject bossEnemy;

    public enum State 
    {
        Idle,
        Prep,
        FireatPlayer,
        MeeleCharge
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
