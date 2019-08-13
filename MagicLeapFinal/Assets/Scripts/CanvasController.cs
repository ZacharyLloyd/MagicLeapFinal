using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    //floats for the canvas that tracks amount killed and escaped
    public float DISTANCE = 2.5f;
    public float SPEED = 5.0f;
    //camera so it knows what to follow
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var step = SPEED * Time.deltaTime;
        var position = mainCamera.transform.position + mainCamera.transform.forward * DISTANCE;
        Quaternion rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        transform.position = Vector3.SlerpUnclamped(transform.position, position, step);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, step);
    }
}
