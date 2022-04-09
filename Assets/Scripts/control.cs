using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class control : MonoBehaviour
{
    public block block;
    private block[,] blocks = new block[4, 4];
    private Vector3[,] positions = new Vector3[4, 4];
    private int[] initialNumRange = new int[] { 2, 4 };
    private bool Up;
    private bool Down;
    private bool Left;
    private bool Right;
    private bool Test;

    void Start()
    {

        InitialPositions();

    }


    void Update()
    {
        Up = Input.GetButtonDown("Up");
        Down = Input.GetButtonDown("Down");
        Left = Input.GetButtonDown("Left");
        Right = Input.GetButtonDown("Right");
        Test = Input.GetButtonDown("Test");
        if (Test)
        {
            InsBlock();
        }
        MoveBlock();

    }

    //生成新的方块
    private void InsBlock()
    {
        if (IsFull())
        {
            return;
        }
        int x = UnityEngine.Random.Range(0, 4);
        int y = UnityEngine.Random.Range(0, 4);
        while (blocks[x, y] != null)
        {
            x = UnityEngine.Random.Range(0, 4);
            y = UnityEngine.Random.Range(0, 4);
        }
        blocks[x, y] = Instantiate(block, positions[x, y], Quaternion.identity);
        blocks[x,y].gameObject.name = "block" + x + y;
        blocks[x, y].number = initialNumRange[UnityEngine.Random.Range(0, initialNumRange.Length)];
    }
    //判断是否满了
    private bool IsFull()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (blocks[i, j] == null)
                {
                    return false;
                }
            }
        }
        return true;
    }
    //判断是否可以移动
    private bool AroundIsNull(int x, int y, Direction direction)
    {
        if (x < 0 || x > 3 || y < 0 || y > 3)
        {
            throw new Exception("超出范围");
        }
        if (blocks[x, y] == null)
        {
            throw new Exception("本身为空");
        }
        switch (direction)
        {
            case Direction.Up:
                if (y > 0 && blocks[x, y - 1] == null)
                {
                    return true;
                }
                if (y == 0)
                    return false;
                break;
            case Direction.Down:
                if (y < 3 && blocks[x, y + 1] == null)
                {
                    return true;
                }
                if (y == 3)
                    return false;
                break;
            case Direction.Left:
                if (x > 0 && blocks[x - 1, y] == null)
                {
                    return true;
                }
                if (x == 0)
                    return false;
                break;
            case Direction.Right:
                if (x < 3 && blocks[x + 1, y] == null)
                {
                    return true;
                }
                if (x == 3)
                    return false;
                break;
        }
        return false;
    }
    //移动方块
    private void MoveBlock()
    {
        if (Up)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blocks[i, j] == null)
                    {
                        continue;
                    }
                    if (AroundIsNull(i, j, Direction.Up))
                    {
                        blocks[i, j].target = positions[i, j - 1];
                        blocks[i, j - 1] = Instantiate(blocks[i, j], positions[i, j - 1], Quaternion.identity);
                        blocks[i, j - 1].gameObject.name = "block" + i + (j - 1);
                        blocks[i, j - 1].number = blocks[i, j].number;
                        //blocks[i, j-1] = blocks[i, j].Clone() as block;
                        Destroy(blocks[i, j].gameObject);

                    }
                }
            }
        }
        if (Down)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    if (blocks[i, j] == null)
                    {
                        continue;
                    }
                    if (AroundIsNull(i, j, Direction.Down))
                    {
                        blocks[i, j].target = positions[i, j + 1];
                        blocks[i, j + 1] = Instantiate(blocks[i, j], positions[i, j + 1], Quaternion.identity);
                        blocks[i, j + 1].gameObject.name = "block" + i + (j + 1);
                        blocks[i, j + 1].number = blocks[i, j].number;
                        //blocks[i, j + 1] = blocks[i, j].Clone() as block;
                        Destroy(blocks[i, j].gameObject);

                    }
                }
            }
        }
        if (Left)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blocks[i, j] == null)
                    {
                        continue;
                    }
                    if (AroundIsNull(i, j, Direction.Left))
                    {
                        blocks[i, j].target = positions[i - 1, j];
                        blocks[i - 1, j] = Instantiate(blocks[i, j], positions[i - 1, j], Quaternion.identity);
                        blocks[i - 1, j].gameObject.name = "block" + (i - 1) + j;
                        blocks[i - 1, j].number = blocks[i, j].number;
                        //blocks[i - 1, j] = blocks[i, j].Clone() as block;
                        Destroy(blocks[i, j].gameObject);

                    }
                }
            }
        }
        if (Right)
        {
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blocks[i, j] == null)
                    {
                        continue;
                    }
                    if (AroundIsNull(i, j, Direction.Right))
                    {
                        blocks[i, j].target = positions[i + 1, j];
                        blocks[i + 1, j] = Instantiate(blocks[i, j], positions[i + 1, j], Quaternion.identity);
                        blocks[i + 1, j].gameObject.name = "block" + (i + 1) + j;
                        blocks[i + 1, j].number = blocks[i, j].number;
                        //blocks[i + 1, j] = blocks[i, j].Clone() as block;
                        Destroy(blocks[i, j].gameObject);

                    }
                }
            }
        }

    }

    //初始化所有位置
    private void InitialPositions()
    {
        positions[0, 0] = new Vector3(-1.8f, 1.8f, 0);
        positions[1, 0] = new Vector3(-0.6f, 1.8f, 0);
        positions[2, 0] = new Vector3(0.6f, 1.8f, 0);
        positions[3, 0] = new Vector3(1.8f, 1.8f, 0);
        positions[0, 1] = new Vector3(-1.8f, 0.6f, 0);
        positions[1, 1] = new Vector3(-0.6f, 0.6f, 0);
        positions[2, 1] = new Vector3(0.6f, 0.6f, 0);
        positions[3, 1] = new Vector3(1.8f, 0.6f, 0);
        positions[0, 2] = new Vector3(-1.8f, -0.6f, 0);
        positions[1, 2] = new Vector3(-0.6f, -0.6f, 0);
        positions[2, 2] = new Vector3(0.6f, -0.6f, 0);
        positions[3, 2] = new Vector3(1.8f, -0.6f, 0);
        positions[0, 3] = new Vector3(-1.8f, -1.8f, 0);
        positions[1, 3] = new Vector3(-0.6f, -1.8f, 0);
        positions[2, 3] = new Vector3(0.6f, -1.8f, 0);
        positions[3, 3] = new Vector3(1.8f, -1.8f, 0);
    }
}
