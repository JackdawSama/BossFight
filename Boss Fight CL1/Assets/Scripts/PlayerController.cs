using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public int health = 4;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullheart;
    public Sprite noheart;

    public Rigidbody2D rb;
    Vector2 mousePos;
    public Camera cam;

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

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullheart;
            }
            else
            {
                hearts[i].sprite = noheart;
            }
            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

        }
    }

    private void FixedUpdate() 
    {
        Vector2 lookDir = mousePos - rb.position;               //vector to find the direction the mouse is pointing
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;        //mathf function to find the angle and set the Z-axis rotation
        rb.rotation = angle;
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