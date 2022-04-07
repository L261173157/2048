using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public GameObject block;
    private block[,] blocks = new block[4, 4];
    private Vector3[,] positions = new Vector3[4, 4];
    private bool Up;
    private bool Down;
    private bool Left;
    private bool Right;

    void Start()
    {
        
        initialPositions();

    }


    void Update()
    {
        Up = Input.GetButtonDown("Up");
        Down = Input.GetButtonDown("Down");
        Left = Input.GetButtonDown("Left");
        Right = Input.GetButtonDown("Right");
        if (Up)
        {
            InsBlock();
        }
    }
    //生成新的方块
    void InsBlock()
    {
        int x = UnityEngine.Random.Range(0, 4);
        int y = UnityEngine.Random.Range(0, 4);
        while (blocks[x, y] != null)
        {
            x = UnityEngine.Random.Range(0, 4);
            y = UnityEngine.Random.Range(0, 4);
        }
        Instantiate(block, positions[x, y], Quaternion.identity);
        blocks[x, y] = GameObject.Find("block(Clone)").GetComponent<block>();
        // blocks[x, y].number = UnityEngine.Random.Range(2, 5);

    }

    void MoveBlock()
    {

    }

    //初始化所有位置
    void initialPositions()
    {
        positions[0, 0] = new Vector3(-1.8f, 1.8f, 0);
        positions[0, 1] = new Vector3(-0.6f, 1.8f, 0);
        positions[0, 2] = new Vector3(0.6f, 1.8f, 0);
        positions[0, 3] = new Vector3(1.8f, 1.8f, 0);
        positions[1, 0] = new Vector3(-1.8f, 0.6f, 0);
        positions[1, 1] = new Vector3(-0.6f, 0.6f, 0);
        positions[1, 2] = new Vector3(0.6f, 0.6f, 0);
        positions[1, 3] = new Vector3(1.8f, 0.6f, 0);
        positions[2, 0] = new Vector3(-1.8f, -0.6f, 0);
        positions[2, 1] = new Vector3(-0.6f, -0.6f, 0);
        positions[2, 2] = new Vector3(0.6f, -0.6f, 0);
        positions[2, 3] = new Vector3(1.8f, -0.6f, 0);
        positions[3, 0] = new Vector3(-1.8f, -1.8f, 0);
        positions[3, 1] = new Vector3(-0.6f, -1.8f, 0);
        positions[3, 2] = new Vector3(0.6f, -1.8f, 0);
        positions[3, 3] = new Vector3(1.8f, -1.8f, 0);
    }
}
