using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject[] players;
    public float dist;
    public float minZoom;
    [Range(0, 100)]
    public float zoomOffsetY, zoomOffsetX, zoomOffset;
    public float x, y;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void FixedUpdate()
    {
        Camera cam = GetComponent<Camera>();

        transform.position = ((players[0].transform.position - 
            players[1].transform.position) * 0.5f) + players[1].transform.position;
        transform.position = new Vector3
            (transform.position.x, transform.position.y, -10);
        dist = Vector3.Distance(players[0].transform.position, 
            players[1].transform.position);
        y = Mathf.Abs(players[0].transform.position.y -
            players[1].transform.position.y);
        x = Mathf.Abs(players[0].transform.position.x -
            players[1].transform.position.x);

        if(y > x)
        {
            cam.orthographicSize = Mathf.Clamp(x * zoomOffsetY / 100, minZoom, 50);
             
        }
        else if(x > y)
        {
            cam.orthographicSize = Mathf.Clamp(x * zoomOffsetX / 100, minZoom, 50);
        }
        else
        {
            cam.orthographicSize = Mathf.Clamp(dist * 
                zoomOffset / 100, minZoom, 50);
        }

        //transform.position = new Vector3(
        //    Mathf.Lerp(minimum, maximum, Time.time), 0, 0);
    }
}
