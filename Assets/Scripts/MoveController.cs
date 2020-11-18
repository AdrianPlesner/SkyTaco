using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MoveController : MonoBehaviour
{
    [SerializeField]    
    public CompositeCollider2D groundCollider2D;
    
    private Rigidbody2D m_Rigidbody2D;
    private SpriteRenderer m_Renderer;
    private Animator m_Animator;
    private bool jumping = false;
    private Vector3Int lastpos;

    public void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
    }
    

    public void Update()
    {
        
        if (Input.GetKey(KeyCode.RightArrow) )
        {
            if(m_Rigidbody2D.position.x < 9)
                Move(getSpeed(),0);
            else 
                StopHorizontal();
            m_Renderer.flipX = false;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            StopHorizontal();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(m_Rigidbody2D.position.x > -9)
                Move(-getSpeed(),0);
            else
                StopHorizontal();
            m_Renderer.flipX = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            StopHorizontal();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            Move(0, 10f);
        }

        jumping = Math.Abs( m_Rigidbody2D.velocity.y) > 0.1;
        m_Animator.SetFloat("Speed", Math.Abs(m_Rigidbody2D.velocity.x));
        m_Animator.SetBool("Jumping",jumping);
    }

    private void StopHorizontal()
    {
        m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
    }

    private int getSpeed()
    {
        return Input.GetKey(KeyCode.LeftShift) ? 10 : 5;
    }

    private void Move(float x, float y)
    {
        Vector2 newVelocity = new Vector2();
        var velocity = m_Rigidbody2D.velocity;
        if(velocity.x > -10 && velocity.x < 10)
            newVelocity.x = x == 0 ? velocity.x : x;
        newVelocity.y = y == 0 ? velocity.y : y;
        velocity = newVelocity;
        m_Rigidbody2D.velocity = velocity;
        
    }

    
}