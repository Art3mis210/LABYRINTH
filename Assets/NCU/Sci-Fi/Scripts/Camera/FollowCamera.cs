using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    RaycastHit hit;
    Vector3 camPos;
    Vector3 startPos;
    void Start()
    {
        camPos = transform.position;
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        camPos.z = player.position.z - 60;
        transform.position = camPos;
    }
}
