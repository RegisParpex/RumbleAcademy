using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {

    // Button
	public static bool Player1_A()
    {
        return Input.GetButton("Player1_A");
    }
    public static bool Player1_B()
    {
        return Input.GetButton("Player1_B");
    }
    public static bool Player1_X()
    {
        return Input.GetButton("Player1_X");
    }
    public static bool Player1_Y()
    {
        return Input.GetButton("Player1_Y");
    }

    public static bool Player2_A()
    {
        return Input.GetButton("Player2_A");
    }
    public static bool Player2_B()
    {
        return Input.GetButton("Player2_B");
    }
    public static bool Player2_X()
    {
        return Input.GetButton("Player2_X");
    }
    public static bool Player2_Y()
    {
        return Input.GetButton("Player2_Y");
    }

    // Axis
    public static float Player1_JoystickLeftX()
    {
        float r = 0.0f;
        r += Input.GetAxis("Player1_JoystickLeftX");
        return Mathf.Clamp(r, -1.0f, -1.0f);
    }
    public static float Player1_JoystickLeftY()
    {
        float r = 0.0f;
        r += Input.GetAxis("Player1_JoystickLeftY");
        return Mathf.Clamp(r, -1.0f, -1.0f);
    }

    public static Vector3 Player_JoystickLeft()
    {
        return new Vector3(Player1_JoystickLeftX(), 0, Player1_JoystickLeftY());
    }
}
