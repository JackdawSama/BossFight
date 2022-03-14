using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    GameManager myManager;
    public GameObject targetPlayer;

    public SpriteRenderer myRenderer;

    private int index;

    public float movementSpeed;
    public Color idleColour;
    public Color attackColour;

    private bool idleState;
    private bool attack1;
    private bool attack2;

    public Transform[] bossAtkPos;
    private Transform bossNxtPos;

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
        index = Random.Range(0,bossAtkPos.Length);
        bossNxtPos = bossAtkPos[index];
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
                    StartCoroutine(BossIdleCoroutine());
                    index = Random.Range(0,bossAtkPos.Length);
                    bossNxtPos = bossAtkPos[index];
                    BossStates(State.MeeleCharge);
                    break;

                case State.MeeleCharge:
                    StartCoroutine(BossAttack1Coroutine());
                    index = Random.Range(0,bossAtkPos.Length);
                    bossNxtPos = bossAtkPos[index];
                    BossStates(State.Idle);
                    break;

                default:
                    //Debug.Log("No State available for this as yet");
                    break;
            }
        }
    }


    IEnumerator BossIdleCoroutine()
    {
        idleState = true;

        yield return new WaitForSeconds(3);

        transform.position = Vector3.MoveTowards(transform.position,targetPlayer.transform.position, movementSpeed);
        idleState = false;
    

        yield return StartCoroutine(BossAttack1Coroutine());
    }

    IEnumerator BossAttack1Coroutine()
    {
        attack1 = true;
        while(transform.position != bossAtkPos[index].transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position,bossAtkPos[index].transform.position, movementSpeed);
        }
        yield return new WaitForSeconds(1);

        myRenderer.color = attackColour;
        yield return new WaitForSeconds(0.5f);

        myRenderer.color = idleColour;
        yield return new WaitForSeconds(0.5f);

        transform.position = Vector3.MoveTowards(transform.position,targetPlayer.transform.position, 5);
        attack1 = false;

        yield return StartCoroutine(BossIdleCoroutine());
    }
}
