using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    public Sprite[] sprites;
    public float speed = 10f;
    private Rigidbody2D rigidbody2D;
    private bool Up;
    private bool Down;
    private bool Left;
    private bool Right;
    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("image");
        SpriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        //SpriteRenderer.sprite=sprites[Random.Range(0,sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        Up = Input.GetButtonDown("Up");
        Down = Input.GetButtonDown("Down");
        Left = Input.GetButtonDown("Left");
        Right = Input.GetButtonDown("Right");
    }
    void FixedUpdate()
    {
        MoveBlock();
    }

    public void SetSprite(int index)
    {
        SpriteRenderer.sprite = sprites[index];
    }

    void MoveBlock()
    {
        if (Up)
        {
            rigidbody2D.velocity=new Vector2(0,speed);
        }
        if (Down)
        {
            rigidbody2D.velocity=new Vector2(0,-speed);
        }
        if (Left)
        {
            rigidbody2D.velocity=new Vector2(-speed,0);
        }
        if (Right)
        {
            rigidbody2D.velocity=new Vector2(speed,0);
        }
    }
}
