using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Moves the player character along a direction
        Move(KeyCode.W, 0, moveSpeed);
        Move(KeyCode.S, 0, -moveSpeed);
        Move(KeyCode.A, -moveSpeed, 0);
        Move(KeyCode.D, moveSpeed, 0);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);             //uses the main camera for mouse input and follow


        //creaes a UI for a health system
        if(health > numOfHearts)                        //checks if the health is more than the max health and if it is then sets the value of health to be equal to max health
        {
            health = numOfHearts;
        }


        //loop to check if the character health has been reduced and based on the hits taken adjusts the sprite image to indicate the number of hearts left
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
        if(Input.GetKeyDown(key))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(xMove, yMove, 0);
        }
    }

    //Functionto check for player collision with enemy and reduces the health of the player, in case of health going to zero the player character is deleted and the scene switches to the death camera 
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Enemy")
        if(health < 1)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Death");
        }
        else 
        {
            health--;
        }

    }
}