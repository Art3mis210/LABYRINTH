using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    Vector3 camPos;
    void Start()
    {
        camPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        camPos.z = player.position.z - 60;
        transform.position = camPos;
        /*camPos.x = player.position.x;
        camPos.z = player.position.z;
        transform.position = camPos;*/
    }
}
