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
            Move(getSpeed(),0);
            m_Renderer.flipX = false;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            StopHorizontal();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(-getSpeed(),0);
            m_Renderer.flipX = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            StopHorizontal();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            Move(0, 8.5f);
        }

        var posis = m_Rigidbody2D.position;
        var hit = Physics2D.Raycast(new Vector2(posis.x,posis.y), -Vector2.up);
        jumping = hit.distance > 0.5f || hit.distance == 0.0f;
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
        //m_Rigidbody2D.velocity = velocity;
        m_Rigidbody2D.velocity = velocity;
        
    }
}