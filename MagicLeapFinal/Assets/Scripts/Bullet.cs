using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    //private variables
    [HideInInspector] public Transform tf;
    //public bullet speed and timers
    public float speed;
    public float delay;
    private float countdown;
    public float damage;
    //public float damageDone;

    // Use this for initialization
    void Start () {
        //sets the variables to funtions
        tf = GetComponent<Transform>();
        //sets the two time keepers to the same time so that it can be reset when needed
        countdown = delay;
    }
	
	// Update is called once per frame
	void Update () {
        //calls the move funtino from the script
        Move();
        //starts the timer
        countdown -= Time.deltaTime;
        //if the timer hits zero it will destroy the game objects
        if (countdown <= 0)
        {
            Destroy(gameObject);
        }
    }

    //destroys the bullet and damages the object hits
	public void OnCollisionEnter (Collision collision)
    {
        //creats a var for hit and makes it the gameobject ran into
        var hit = collision.gameObject;
        //gets the health component from the hit object if there is one
        var health = hit.GetComponent<Rotate>();
        //if there is a health variable it will run the function
        if (health != null)
        {
            //deals the ammount of damage to the hit objects health
            health.TakeDamage(damage);
        }
        //destroys the bullet when dones
        Destroy(gameObject);
}
    //has the bullet move in the direction it is facing when fired at a set speed.
    public void Move()
    {
        //move the bullet forward at a constant speed
        tf.position += tf.forward * speed * Time.deltaTime;
    }
}