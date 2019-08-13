using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class AK47 : Weapon
{
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
    //what it does on equip
    public override void OnEquip(GameObject target)
    {
        base.OnEquip(target);
    }
    //what it does when on attack is called
    protected override void OnAttack()
    {
        base.OnAttack();
    }
    //void to call onequip that can be run by other scripts
    public void runEquip()
    {

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
            base.OnAttack();
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