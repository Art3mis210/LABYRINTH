using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyCamera : MonoBehaviour
{
    private GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MobileInput.ChangeControls = false;
        MobileInput.InvertControls = false;
    }
    void Update()
    {
        if (Camera.current != null)
        {
            foreach (Transform child in transform.GetComponentInChildren<Transform>())
            {
                if (child != transform && child.gameObject != Camera.current.transform.gameObject)
                {
                    if (Vector3.Distance(Player.transform.position, child.transform.position) < Vector3.Distance(Player.transform.position, Camera.current.transform.position))
                    {
                        Camera.current.enabled = false;
                        child.GetComponent<Camera>().enabled = true;
                        if(MobileInput.ChangeControls == true)
                            MobileInput.ChangeControls = false;
                        else
                            MobileInput.ChangeControls = true;
                    }
                }
            }
        }
        
    }
}
