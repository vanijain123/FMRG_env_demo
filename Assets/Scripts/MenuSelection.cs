using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelection : MonoBehaviour
{

    //public GameObject pointer;
    public OVRPlayerController player;
    public float speed = 10.0f;
    public Transform redCube;
    public Transform greenCube;
    public Animator animator;

    public Transform room1_location;
    public Transform room2_location;
    public Transform room3_location;

    //public Light room1_light;
    //public Light room2_light;
    //public Light room3_light;

    public Animator room1_light_animation;
    public Animator room2_light_animation;
    public Animator room3_light_animation;

    private LineRenderer lineRenderer;
    private GameObject selection = null;
    private float step;
    private Vector3 leftPosition;
    private Vector3 leftCornerPosition;
    private Vector3 rightPosition;
    private Vector3 rightCornerPosition;
    private Vector3 centerPosition;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        leftPosition = new Vector3(0.45f, -1.25f, 2);
        centerPosition = new Vector3(2.0f, -1.25f, 0);
        rightPosition = new Vector3(0.45f, -1.25f, -2);
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.collider.gameObject.tag == "red" || hit.collider.gameObject.tag == "green")
            {
                MoveCubes(hit.collider.gameObject.tag);
            }
            else if (hit.collider.gameObject.tag == "room1" || hit.collider.gameObject.tag == "room2" || hit.collider.gameObject.tag == "room3")
            {
                if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)){
                    MoveToRoom(hit.collider.gameObject.tag);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "red" || collision.gameObject.tag == "green")
        {
            MoveCubes(collision.gameObject.tag);
        }

        else if (collision.gameObject.tag == "room1" || collision.gameObject.tag == "room2" || collision.gameObject.tag == "room3")
        {
            MoveToRoom(collision.gameObject.tag);
        }
    }

    private void MoveCubes(string color)
    {
        if (color == "green")
        {
            redCube.position = Vector3.MoveTowards(redCube.position, leftPosition, step);
            greenCube.position = Vector3.MoveTowards(greenCube.position, centerPosition, step);
        }

        if (color == "red")
        {
            redCube.position = Vector3.MoveTowards(redCube.position, centerPosition, step);
            greenCube.position = Vector3.MoveTowards(greenCube.position, rightPosition, step);
        }
    }

    private void MoveToRoom(string roomName)
    {
        if (roomName == "room1")
        {
            player.enabled = false;
            player.transform.position = room1_location.position;
            player.enabled = true;

            room1_light_animation.SetTrigger("room1_light_trigger");
        }

        if (roomName == "room2")
        {
            
            player.enabled = false;
            player.transform.position = room2_location.position;
            player.enabled = true;

            room2_light_animation.SetTrigger("room2_light_trigger");
        }

        if(roomName == "room3")
        {
            player.enabled = false;
            player.transform.position = room3_location.position;
            player.enabled = true;

            room3_light_animation.SetTrigger("room3_light_trigger");
        }
    }
}
