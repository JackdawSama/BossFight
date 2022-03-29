using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioSource audioSource;
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Wall")                             //checks if the tag for the object collided with is the enemy or the wall
        {
            audioSource.Play();                  //plays an audio clip when the bullet collides with the enemy
            Destroy(gameObject);                                        //deletes the bullet after the bullet collides with the enemy
        }    
    }
}
