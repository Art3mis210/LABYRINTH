using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputModule : MonoBehaviour
{
    public abstract Vector2 GetInput();
    public static bool InvertControls;
    public static bool ChangeControls;
}
