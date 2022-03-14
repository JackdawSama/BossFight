using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    GameManager myManager;

    public SpriteRenderer myRenderer;

    public float attackSpeed;
    public Color idleColour;
    public Color attackColour;

    private bool attack1;
    private bool attack2;

    public Transform[] bossAtkPos;
    public Vector2 bossCurrentPos;
    public enum State 
    {
        Idle,
        MeeleCharge,
        MeeleCharge2,
    }
    State currentState;
    // Start is called before the first frame update
    void Start()
    {
        myManager = GameManager.FindInstance();

        BossStates(State.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BossStates(State newState) {
        {
            currentState = newState;
            switch(newState)
            {
                case State.Idle:
                break;

                case State.MeeleCharge:
                break;

                case State.MeeleCharge2:
                break;

                default:
                Debug.Log("No State available for this as yet");
                break;
            }
        }
    }

    IEnumerator BossAttack1Coroutine()
    {
        attack1 = true;
        

        yield return null;
    }
}
