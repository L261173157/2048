using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class block : MonoBehaviour, ICloneable
{
    private SpriteRenderer SpriteRenderer;
    private Sprite[] sprites;
    //移动速度
    public float speed;
    //方块的数字
    public int number;
    //方块的目标位置
    public Vector3 target;
    private int[] initialNumRange = new int[] { 2, 4 };

    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("image");
        SpriteRenderer = GetComponent<SpriteRenderer>();
        //number = initialNumRange[UnityEngine.Random.Range(0, initialNumRange.Length)];
        target = transform.position;
    }

    void Update()
    {
        SetSprite();
        MoveToTarget(target);
    }
    //设置图形数字
    private void SetSprite()
    {
        switch (number)
        {
            case 2:
                SpriteRenderer.sprite = sprites[0];
                break;
            case 4:
                SpriteRenderer.sprite = sprites[1];
                break;
            case 8:
                SpriteRenderer.sprite = sprites[2];
                break;
            case 16:
                SpriteRenderer.sprite = sprites[3];
                break;
            case 32:
                SpriteRenderer.sprite = sprites[4];
                break;
            case 64:
                SpriteRenderer.sprite = sprites[5];
                break;
            case 128:
                SpriteRenderer.sprite = sprites[6];
                break;
            case 256:
                SpriteRenderer.sprite = sprites[7];
                break;
            case 512:
                SpriteRenderer.sprite = sprites[8];
                break;
            case 1024:
                SpriteRenderer.sprite = sprites[9];
                break;
            case 2048:
                SpriteRenderer.sprite = sprites[10];
                break;
            case 4096:
                SpriteRenderer.sprite = sprites[11];
                break;
            case 8192:
                SpriteRenderer.sprite = sprites[12];
                break;
            case 16384:
                SpriteRenderer.sprite = sprites[13];
                break;
            case 32768:
                SpriteRenderer.sprite = sprites[14];
                break;
            case 65536:
                SpriteRenderer.sprite = sprites[15];
                break;
            case 131072:
                SpriteRenderer.sprite = sprites[16];
                break;
            default:
                break;
        }

    }
    //移动到目标位置
    private void MoveToTarget(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    //按指定方向移动
    private void MoveBlock(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                transform.position += Vector3.up * speed * Time.deltaTime;
                break;
            case Direction.Down:
                transform.position += Vector3.down * speed * Time.deltaTime;
                break;
            case Direction.Left:
                transform.position += Vector3.left * speed * Time.deltaTime;
                break;
            case Direction.Right:
                transform.position += Vector3.right * speed * Time.deltaTime;
                break;
            default:
                break;
        }
    }
    //深复制
    public object Clone()
    {
        using (Stream objectStream = new MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(objectStream, this);
            objectStream.Seek(0, SeekOrigin.Begin);
            return formatter.Deserialize(objectStream) as block;
        }
        //return this.MemberwiseClone();浅复制
    }
}
