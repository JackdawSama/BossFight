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
        index = Random.Range(0,bossAtkPos.Length);                      //randomises boss position index value
        bossNxtPos = bossAtkPos[index];                                 //sets which posiion to take before attacking
        BossStates(State.Idle);                                         //boots idle state
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
                case State.Idle:                                            //first idle to atack state of boss
                    StartCoroutine(BossIdleCoroutine());
                    break;

                case State.MeeleCharge:
                    StartCoroutine(BossAttack1Coroutine());                 //attack variation of the boss
                    index = Random.Range(0,bossAtkPos.Length);              //randomises index to switch position 
                    bossNxtPos = bossAtkPos[index];                         //Sets the next posiiton of the boss
                    break;

                default:
                    Debug.Log("No State available for this as yet");
                    break;
            }
        }
    }


    //Coroutine for the Boss' idle to atack state
    IEnumerator BossIdleCoroutine()
    {
        idleState = true;

        yield return new WaitForSeconds(3);

        Vector3 playerSnapshot = targetPlayer.transform.position;

        while(Vector3.Distance(transform.position,playerSnapshot) >= 0.5f)
        {
            Debug.Log("In Idle While Loop");
            float step = movementSpeed*Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,playerSnapshot, step);    //Boss object dashes towards player
            yield return null;
        }
        index = Random.Range(0,bossAtkPos.Length);
        bossNxtPos = bossAtkPos[index];
        idleState = false;
        yield return new WaitForSeconds(1.5f);                                                      // window to leave boss vulnerable for an attack from the player
        BossStates(State.MeeleCharge);                                                              //calls the next state so that the boss moves into anoher atack pattern
    }

    //Coroutine for the Boss' attack state
    //Boss object is supposed to reposition itself to one of the cardinal points(withoui teleporting) and should dash towards the player()again without teleporting
    IEnumerator BossAttack1Coroutine()
    {
        attack1 = true;

        float step = movementSpeed*Time.deltaTime;

        Debug.Log("Entered Attack While Loop)");

        while(transform.position != bossAtkPos[index].transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position,bossAtkPos[index].transform.position, step);   //Boss object moves towards on ofe the four cardinal points
                yield return null;
            }
        yield return new WaitForSeconds(1);

        myRenderer.color = attackColour;                        //changes colour to indicate attacking
        yield return new WaitForSeconds(0.5f);

        myRenderer.color = idleColour;                          //changes colour to indicate attacking
        yield return new WaitForSeconds(0.5f);

        Vector3 playerSnapshot = targetPlayer.transform.position;
        transform.position = Vector3.MoveTowards(transform.position,playerSnapshot, step);      //boss object dashes towards player
        index = Random.Range(0,bossAtkPos.Length);
        bossNxtPos = bossAtkPos[index];                        
        attack1 = false;

        BossStates(State.Idle);                                 //switches back to another state so that the attack pattern continues
    }
}
