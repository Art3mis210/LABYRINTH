using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : InputModule
{
    Vector2 myInput;
    public MobileInput()
    {
        myInput = new Vector2();
        InvertControls = false;
    }

    public override Vector2 GetInput()
    {
        if (InvertControls)
        {
            myInput.x = -Input.acceleration.x;
            myInput.y = -Input.acceleration.y;
            if (myInput.magnitude == 0 && ChangeControls)
            {
                ChangeControls = false;
                InvertControls = false;

            }
        }
        else
        {
            myInput.x = Input.acceleration.x;
            myInput.y = Input.acceleration.y;
            if (myInput.magnitude == 0 && ChangeControls)
            {
                ChangeControls = false;
                InvertControls = true;
            }
        }
        return myInput.normalized;
    }
}
