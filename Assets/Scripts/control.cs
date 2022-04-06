using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public block[] blocks;
    private bool Up;
    private bool Down;
    private bool Left;
    private bool Right;

    void Start()
    {
        Up = Input.GetButtonDown("Up");
        Down = Input.GetButtonDown("Down");
        Left = Input.GetButtonDown("Left");
        Right = Input.GetButtonDown("Right");
    }


    void Update()
    {

    }
}
