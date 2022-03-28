using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public int health = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Moves the player character along a direction
        Move(KeyCode.W, 0, moveSpeed);
        Move(KeyCode.S, 0, -moveSpeed);
        Move(KeyCode.A, -moveSpeed, 0);
        Move(KeyCode.D, moveSpeed, 0);

    }

    //function to move the player character
    void Move(KeyCode key, float xMove, float yMove)
    {
        if(Input.GetKey(key))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(xMove, yMove, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if( other.gameObject.tag == "Enemy")
        if(health < 1)
        {
            Destroy(gameObject);
        }
        health--;

    }
}