using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public abstract class Weapon : MonoBehaviour
{
    //public variables for weapon and bullet prefab
    public GameObject weapon;
    public Transform shootPoint;
    // variables for bullet speed and prefab
    public float speedToShoot;
    public GameObject bullet;
    //flaots for bullet to be input on instantiate
    public float damage;
    public float speed;
    public float timeToDestroy;
    private float timeToNextFire;
    //bullet made to change variables
    private GameObject instantiatedBullet;
    //audio source and clip for when firing
    public AudioSource sfxAudio;
    public AudioClip shoot;
    //private controller for the magic leap controller
    private MLInputController _controller;
    // Use this for initialization
    void Start()
    {
        //sets the weapon as this objecy
        weapon = this.gameObject;
        //magic leap beginning functions
        MLInput.Start();
        MLInput.OnControllerButtonDown += OnButtonDown;
        MLInput.OnControllerButtonUp += OnButtonUp;
        _controller = MLInput.GetController(MLInput.Hand.Left);
    }

    // Update is called once per frame
    void Update()
    {
        CheckControl();
        //if get mosue button pressed or magic leap press will run the on attack void
        if (Input.GetKey(KeyCode.Mouse0))
        {
            OnAttack();
        }
    }
    //base on equip void
    public virtual void OnEquip(GameObject target)
    {

    }
    //void for when attacking
    protected virtual void OnAttack()
    {
        //checks to see if correct amount of time has passed from previous fire to shoot
        if (Time.time > timeToNextFire)
        {
            //keeps track of when the gun can fire next
            timeToNextFire = Time.time + speedToShoot;
            //creates the bullet and sets the bullet's speed, damage and how long till it is destroyed
            instantiatedBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            instantiatedBullet.GetComponent<Bullet>().damage = damage;
            instantiatedBullet.GetComponent<Bullet>().speed = speed;
            instantiatedBullet.GetComponent<Bullet>().delay = timeToDestroy;
            //plays the shoot clip one time
            sfxAudio.PlayOneShot(shoot, sfxAudio.volume);
        }
    }

    void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= OnButtonDown;
        MLInput.OnControllerButtonUp -= OnButtonUp;
        MLInput.Stop();
    }
    void CheckControl()
    {
        if (_controller.TriggerValue > 0.2f)
        {
            return;
        }
        else if (_controller.Touch1PosAndForce.z > 0.0f)
        {
            OnAttack();
            float X = _controller.Touch1PosAndForce.x;
            float Y = _controller.Touch1PosAndForce.y;
            Vector3 forward = Vector3.Normalize(Vector3.ProjectOnPlane(transform.forward, Vector3.up));
            Vector3 right = Vector3.Normalize(Vector3.ProjectOnPlane(transform.right, Vector3.up));
            Vector3 force = Vector3.Normalize((X * right) + (Y * forward));
        }
    }
    void OnButtonDown(byte controller_id, MLInputControllerButton button)
    {
        if ((button == MLInputControllerButton.Bumper))
        {
            OnAttack();
        }
    }

    void OnButtonUp(byte controller_id, MLInputControllerButton button)
    {
        if (button == MLInputControllerButton.HomeTap)
        {
            return;
        }
    }
}
