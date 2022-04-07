using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class block : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    private Sprite[] sprites;
    public float speed;
    public int number;
    private int[] initialNumRange = new int[] { 2, 4 };

    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("image");
        SpriteRenderer = GetComponent<SpriteRenderer>();
        number = initialNumRange[Random.Range(0, initialNumRange.Length)];
    }

    void Update()
    {
        SetSprite();
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
    void MoveToTarget(Vector3 target)
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
}
