using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    
    private Collider2D m_Collider2D;

    private Rigidbody2D m_Rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        m_Collider2D = GetComponent<PolygonCollider2D>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetDirection = ((Vector2)target.position - m_Rigidbody2D.position).normalized;

        m_Rigidbody2D.velocity = targetDirection;



    }
}
