using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void TurnGameObjectOff(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    public void TurnGameObjectOn(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}
