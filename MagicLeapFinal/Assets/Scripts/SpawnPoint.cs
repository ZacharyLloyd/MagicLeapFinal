using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //variables used spawn the selected object
    public float respawnTime;
    public GameObject objectToSpawn;
    private GameObject spawnedObject;
    private float maxSpawnTime = 15;
    private Transform tf;
    public Vector3 gizmoSize;
    public bool spawnConstant;
    public Transform target;
    //audio clip for the zombie moan along with the audio source ti okay from
    public AudioClip moan;
    public AudioSource audioPlayer;
    // Use this for initialization
    void Start()
    {
        //get the object transform
        tf = GetComponent<Transform>();
        //spawn on start
        Spawn();
        //set the spawn timer using a random number generator
        respawnTime = Random.Range(1, maxSpawnTime);
    }
    // Update is called once per frame
    void Update()
    {
        //if spawnConstant is true do the following
        if (spawnConstant == true)
        {
            respawnTime -= Time.deltaTime;
            if (respawnTime <= 0)
            {
                Spawn();
            }
        }
        //if spawnConstant is false do the following
        else if (spawnConstant == false)
        {
            //check to see if spawned object still exists -- if so, do nothing...
            if (spawnedObject != null)
            {
                return;
            }
            //if does not exist start counting down on the countdown timer
            respawnTime -= Time.deltaTime;
            //if it's true, then run the spawn void
            if (respawnTime <= 0)
            {
                Spawn();
            }
        }
    }
    //void for spawning
    public void Spawn()
    {
        //create my object
        spawnedObject = Instantiate(objectToSpawn, tf.position, tf.rotation);
        //get the rotate script from the object
        Rotate pawnRotate = spawnedObject.GetComponent<Rotate>();
        //set the target transfrom to move towards to the target transform of this script
        pawnRotate.target = target;
        pawnRotate.moan = moan;
        pawnRotate.audioPlayer = audioPlayer;

        //reset respawn timer using a random number generator
        respawnTime = Random.Range(1, maxSpawnTime);
    }
    //draw a gizmo so you can see the object in the editor
    public void OnDrawGizmos()
    {
        //transform the matrix to a local world matrix
        Gizmos.matrix = transform.localToWorldMatrix;
        //set the color to green
        Gizmos.color = Color.green;
        //center the gizmo to the objects 0, y/2, 0 position
        Vector3 centerPosition = new Vector3(0, gizmoSize.y / 2, 0);
        //draw a cube at the position using the size of the gizmo provided
        Gizmos.DrawCube(centerPosition, gizmoSize);
        //draw a ray and point it towards the direction the object is facing
        Gizmos.DrawRay(new Ray(Vector3.zero, Vector3.forward));
        //Gizmos.DrawIcon
    }
}
