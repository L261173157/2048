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
    public Sprite[] sprites;
    public float speed;


    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("image");
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        MoveToTarget(new Vector3(0, 0, 0));
    }

    public void SetSprite(int index)
    {
        SpriteRenderer.sprite = sprites[index];
    }

    void MoveToTarget(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    void MoveBlock(Direction direction)
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
