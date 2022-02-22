using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    Vector3 camPos;
    void Start()
    {
        camPos = new Vector3(player.position.x, transform.position.y, player.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        camPos.x = player.position.x;
        camPos.z = player.position.z;
        transform.position = camPos;
    }
}
