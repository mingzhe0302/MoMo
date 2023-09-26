using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInput
{
    public static Vector2 playerInputVector;
    public static Vector2 cursorPosVector;

    public static Vector2 getCursorPosToMouse()
    {
        cursorPosVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return cursorPosVector;
    }

    public static Vector2 getPlayerInputVector()
    {
        playerInputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        return playerInputVector;
    }

    public static bool weaponSwitch()
    {
        return Input.GetButtonDown("WeaponSwitch");
    }

    public static bool isFiring()
    {
        return (Input.GetButton("Fire1"));
    }
}
