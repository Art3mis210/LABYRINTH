using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(Screen.width, Screen.height,true);
    }
    public void TurnGameObjectOff(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    public void TurnGameObjectOn(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}
