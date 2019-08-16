using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rotate : MonoBehaviour
{
    //getting the transformation for easier access later in the script
    private Transform tf;
    //the speed at which the object should rotate
    public float rotateSpeed;
    //the transform of a given object
    public Transform target;
    //bool for whether the object spins or looks at target, set to true but can be changed manually in the editor
    public bool lookAtTarget = true;
    //distance for when to destroy the zombie due to hitting target
    public float destroyDistance;
    //float for the zombie health
    public float health;
    //audio clip for the zombie moan along with audio source to play from
    public AudioClip moan;
    public AudioSource audioPlayer;
    //the controller for future reference
    public Controller CC;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        //gets the objects transform for ease of use
        tf = GetComponent<Transform>();
        //finds and gets the controller script from the controller object
        CC = GameObject.Find("Controller").GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        //if bool is false do inside the brackets
        if (lookAtTarget == false)
        {
            //calls the spin void to run on update
            Spin();
        }
        //if bool is true do inside the brackets
        if (lookAtTarget == true)
        {
            //rotate the attached game object to look at the target(in the case of the model the camera)
            transform.LookAt(target);
        }
        //if the distance between the object and the target is less then the distance for destroying the object
        //will destroy the game object this script is attacked to along with adding a counter to controller zombies escaped
        //and then having the controller set the escaped text to the new number
        if (Vector3.Distance(tf.position, target.position) <= destroyDistance)
        {
            CC.zombieEscaped = CC.zombieEscaped + 1;
            CC.damageToTake = damage;
            CC.SetZombiesEscaped();
            CC.SetPlayerHealth();
            Destroy(gameObject);
        }
    }

    //void for when spin is called
    void Spin()
    {
        //rotates the obejct using the transform rotate
        //and rotating it 0 for x and z and the rotate speed time the time passed for y
        tf.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    //void that alllows the zombie to take damage
    public void TakeDamage(float damage)
    {
        //plays the zombie moan clip once when hit
        audioPlayer.PlayOneShot(moan, audioPlayer.volume);
        //has the health become current health - damage done
        health = health - damage;
        //if health is less than or equal to 0
        if (health <= 0)
        {
            //adds one to the counter for killed in controller and runs the set void
            CC.zombieKilled = CC.zombieKilled + 1;
            CC.SetZombiesKilled();
            //destroys the zombies game object
            Destroy(gameObject);
        }
    }
}