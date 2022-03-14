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


    //Coroutine for the Boss' idle time.
    IEnumerator BossIdleCoroutine()
    {
        idleState = true;

        yield return new WaitForSeconds(3);
        
        transform.position = Vector3.MoveTowards(transform.position,targetPlayer.transform.position, movementSpeed);    //Boss object dashes towards player
        index = Random.Range(0,bossAtkPos.Length);
        bossNxtPos = bossAtkPos[index];
        idleState = false;
    

        yield return StartCoroutine(BossAttack1Coroutine());                                                            //Makes sure that the coroutine loops
    }

    //Coroutine for the Boss' attack state
    //Boss object is supposed to reposition itself to one of the cardinal points(withoui teleporting) and should dash towards the player()again without teleporting
    IEnumerator BossAttack1Coroutine()
    {
        attack1 = true;
        while(transform.position != bossAtkPos[index].transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position,bossAtkPos[index].transform.position, movementSpeed);   //Boss object moves towards on ofe the four cardinal points
        }
        yield return new WaitForSeconds(1);

        myRenderer.color = attackColour;                        //changes colour to indicate attacking
        yield return new WaitForSeconds(0.5f);

        myRenderer.color = idleColour;                          //changes colour to indicate attacking
        yield return new WaitForSeconds(0.5f);

        transform.position = Vector3.MoveTowards(transform.position,targetPlayer.transform.position, movementSpeed);            //boss object dashes towards player
        index = Random.Range(0,bossAtkPos.Length);
        bossNxtPos = bossAtkPos[index];                        
        attack1 = false;

        yield return StartCoroutine(BossIdleCoroutine());
    }
}
