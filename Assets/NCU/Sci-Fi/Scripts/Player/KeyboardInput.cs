using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : InputModule
{
    Vector2 myInput;
    public KeyboardInput()
    {
        myInput = new Vector2();
    }

    public override Vector2 GetInput()
    {
        if (InvertControls)
        {
            myInput.x = -Input.GetAxis("Horizontal");
            myInput.y = -Input.GetAxis("Vertical");
            if (myInput.magnitude == 0 && ChangeControls)
            {
                ChangeControls = false;
                InvertControls = false;
            }
        }
        else
        {
            myInput.x = Input.GetAxis("Horizontal");
            myInput.y = Input.GetAxis("Vertical");
            if (myInput.magnitude == 0 && ChangeControls)
            {
                ChangeControls = false;
                InvertControls = true;

            }
        }
        return myInput.normalized;
    }
}
