using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class control : MonoBehaviour
{
    public block block;
    public int score;
    private block[,] blocks = new block[4, 4];
    private Vector3[,] positions = new Vector3[4, 4];
    private int[] initialNumRange = new int[] {2, 4};
    private bool Up;
    private bool Down;
    private bool Left;
    private bool Right;
    private bool StartGame;
    private blockToSave blockToSave = new blockToSave();
    private const string filePath = "save.json";

    private void Start()
    {
        InitialPositions();
        LoadData();
    }

    private void Update()
    {
        Up = Input.GetButtonDown("Up");
        Down = Input.GetButtonDown("Down");
        Left = Input.GetButtonDown("Left");
        Right = Input.GetButtonDown("Right");
        StartGame = Input.GetButtonDown("Start");
        if (StartGame)
        {
            score = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blocks[i, j] != null)
                    {
                        Destroy(blocks[i, j].gameObject);
                        blocks[i, j] = null;
                    }
                }
            }

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
        blocks[x, y].gameObject.name = "block" + x + y;
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


    //判断是否胜利
    private bool IsVictory()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (blocks[i, j] != null && blocks[i, j].number == 2048)
                {
                    return true;
                }
            }
        }

        return false;
    }

    //判断是否失败
    private bool IsGameOver()
    {
        if (IsFull())
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blocks[i, j] != null)
                    {
                        if (i == 0 && blocks[i, j].number == blocks[i + 1, j].number)
                        {
                            return false;
                        }

                        if (i == 3 && blocks[i, j].number == blocks[i - 1, j].number)
                        {
                            return false;
                        }

                        if (j == 0 && blocks[i, j].number == blocks[i, j + 1].number)
                        {
                            return false;
                        }

                        if (j == 3 && blocks[i, j].number == blocks[i, j - 1].number)
                        {
                            return false;
                        }

                        if ((i == 1 || i == 2) && (j == 1 || j == 2))
                        {
                            if (blocks[i, j].number == blocks[i - 1, j].number ||
                                blocks[i, j].number == blocks[i + 1, j].number ||
                                blocks[i, j].number == blocks[i, j - 1].number ||
                                blocks[i, j].number == blocks[i, j + 1].number)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        return false;
    }

    private void Judge()
    {
        if (IsVictory())
        {
            Debug.Log("恭喜你，你赢了！");
        }
        else if (IsGameOver())
        {
            Debug.Log("很遗憾，你输了！");
        }

        Debug.Log("游戏分数：" + score.ToString());
        InsBlock();
        SaveData();
    }

    //判断移动位置
    private void MovePosition(int x, int y, Direction direction, out int newX, out int newY, out bool isSameNum)
    {
        if (x < 0 || x > 3 || y < 0 || y > 3)
        {
            throw new Exception("超出范围");
        }

        if (blocks[x, y] == null)
        {
            throw new Exception("本身为空");
        }

        newX = 0; //新的x坐标
        newY = 0; //新的y坐标
        isSameNum = false; //此方向方块是否相同
        switch (direction)
        {
            case Direction.Up:
                newX = x;
                for (int i = 0; i <= y; i++)
                {
                    if (y - i - 1 < 0)
                    {
                        newY = y - i;
                        break;
                    }

                    if (blocks[x, y - i - 1] != null)
                    {
                        newY = y - i;
                        if (blocks[x, y - i - 1].number == blocks[x, y].number)
                        {
                            isSameNum = true;
                        }

                        break;
                    }
                }

                break;
            case Direction.Down:
                newX = x;
                for (int i = 0; i <= y + 3; i++)
                {
                    if (y + i + 1 > 3)
                    {
                        newY = y + i;
                        break;
                    }

                    if (blocks[x, y + i + 1] != null)
                    {
                        newY = y + i;
                        if (blocks[x, y + i + 1].number == blocks[x, y].number)
                        {
                            isSameNum = true;
                        }

                        break;
                    }
                }

                break;
            case Direction.Left:
                newY = y;
                for (int i = 0; i <= x; i++)
                {
                    if (x - i - 1 < 0)
                    {
                        newX = x - i;
                        break;
                    }

                    if (blocks[x - i - 1, y] != null)
                    {
                        newX = x - i;
                        if (blocks[x - i - 1, y].number == blocks[x, y].number)
                        {
                            isSameNum = true;
                        }

                        break;
                    }
                }

                break;
            case Direction.Right:
                newY = y;
                for (int i = 0; i <= x + 3; i++)
                {
                    if (x + i + 1 > 3)
                    {
                        newX = x + i;
                        break;
                    }

                    if (blocks[x + i + 1, y] != null)
                    {
                        newX = x + i;
                        if (blocks[x + i + 1, y].number == blocks[x, y].number)
                        {
                            isSameNum = true;
                        }

                        break;
                    }
                }

                break;
        }
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

                    MovePosition(i, j, Direction.Up, out int newX, out int newY, out bool isSameNum);
                    if (newX != i || newY != j)
                    {
                        blocks[i, j].target = positions[newX, newY]; //实现动画
                        blocks[newX, newY] = Instantiate(blocks[i, j], positions[newX, newY], Quaternion.identity);
                        blocks[newX, newY].gameObject.name = "block" + newX + newY;
                        blocks[newX, newY].number = blocks[i, j].number;
                        Destroy(blocks[i, j].gameObject);
                        blocks[i, j] = null;
                    }

                    if (isSameNum)
                    {
                        blocks[newX, newY - 1].number *= 2;
                        Destroy(blocks[newX, newY].gameObject);
                        blocks[newX, newY] = null;
                        score++;
                    }
                }
            }

            Invoke(nameof(Judge), 0.5f);
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

                    MovePosition(i, j, Direction.Down, out int newX, out int newY, out bool isSameNum);
                    if (newX != i || newY != j)
                    {
                        blocks[i, j].target = positions[newX, newY]; //实现动画
                        blocks[newX, newY] = Instantiate(blocks[i, j], positions[newX, newY], Quaternion.identity);
                        blocks[newX, newY].gameObject.name = "block" + newX + newY;
                        blocks[newX, newY].number = blocks[i, j].number;
                        Destroy(blocks[i, j].gameObject);
                        blocks[i, j] = null;
                    }

                    if (isSameNum)
                    {
                        blocks[newX, newY + 1].number *= 2;
                        Destroy(blocks[newX, newY].gameObject);
                        blocks[newX, newY] = null;
                        score++;
                    }
                }
            }

            Invoke(nameof(Judge), 0.5f);
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

                    MovePosition(i, j, Direction.Left, out int newX, out int newY, out bool isSameNum);
                    if (newX != i || newY != j)
                    {
                        blocks[i, j].target = positions[newX, newY]; //实现动画
                        blocks[newX, newY] = Instantiate(blocks[i, j], positions[newX, newY], Quaternion.identity);
                        blocks[newX, newY].gameObject.name = "block" + newX + newY;
                        blocks[newX, newY].number = blocks[i, j].number;
                        Destroy(blocks[i, j].gameObject);
                        blocks[i, j] = null;
                    }

                    if (isSameNum)
                    {
                        blocks[newX - 1, newY].number *= 2;
                        Destroy(blocks[newX, newY].gameObject);
                        blocks[newX, newY] = null;
                        score++;
                    }
                }
            }

            Invoke(nameof(Judge), 0.5f);
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

                    MovePosition(i, j, Direction.Right, out int newX, out int newY, out bool isSameNum);
                    if (newX != i || newY != j)
                    {
                        blocks[i, j].target = positions[newX, newY]; //实现动画
                        blocks[newX, newY] = Instantiate(blocks[i, j], positions[newX, newY], Quaternion.identity);
                        blocks[newX, newY].gameObject.name = "block" + newX + newY;
                        blocks[newX, newY].number = blocks[i, j].number;
                        Destroy(blocks[i, j].gameObject);
                        blocks[i, j] = null;
                    }

                    if (isSameNum)
                    {
                        blocks[newX + 1, newY].number *= 2;
                        Destroy(blocks[newX, newY].gameObject);
                        blocks[newX, newY] = null;
                        score++;
                    }
                }
            }

            Invoke(nameof(Judge), 0.5f);
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

    private void SaveData()
    {
        // if (Up || Down || Left || Right)
        // {
        //     PlayerPrefs.SetInt("score", score);
        // }

        blockToSave.score = score;
        blockToSave.number.Clear();
        foreach (var block in blocks)
        {
            if (block == null)
            {
                blockToSave.number.Add(0);
                continue;
            }

            blockToSave.number.Add(block.number);
        }

        string json = JsonUtility.ToJson(blockToSave);

        File.WriteAllText(filePath, json);
    }

    private void LoadData()
    {
        // score = PlayerPrefs.GetInt("score");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            blockToSave = JsonUtility.FromJson<blockToSave>(json);
            score = blockToSave.score;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blockToSave.number[i * 4 + j] == 0)
                    {
                        continue;
                    }

                    blocks[i, j] = Instantiate(block, positions[i, j], Quaternion.identity);
                    blocks[i, j].number = blockToSave.number[i * 4 + j];
                    blocks[i, j].gameObject.name = "block" + i + j;
                }
            }
        }
    }
}