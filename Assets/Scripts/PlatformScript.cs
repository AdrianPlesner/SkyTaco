using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class PlatformScript : MonoBehaviour
{
    [SerializeField] public Tile normalTile;
    [SerializeField] public Tile wornTile;
    [SerializeField] public Tile dyingTile;
    [SerializeField] public Transform player;
    
    private Tilemap m_Tilemap;
    private Vector3Int? m_LastPos;
    private bool m_DestroyOldTile = false;

    private Random m_Random;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Tilemap = GetComponent<Tilemap>();
        InvokeRepeating(nameof(NewTile), 2f,2f);
    }

    private void NewTile()
    {
        while (true)
        {
            var position = player.position;
            int x = (int) Random.Range(-9, 9);
            int y = (int) Random.Range(position.y - 3, position.y + 3);
            Vector3Int pos = new Vector3Int(x, y, 0);
            if (m_Tilemap.GetTile(pos) == null && !NearToPlayer(pos))
                m_Tilemap.SetTile(pos, normalTile);
            else
                continue;
            break;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Player") && other.rigidbody.velocity.y < 0.5)
        {

            Vector3Int newPos = m_Tilemap.WorldToCell(other.contacts[other.contactCount / 2].point);
            newPos.y -= 1;
            if (newPos != m_LastPos)
            {
                if (m_DestroyOldTile)
                {
                    if (m_LastPos != null) m_Tilemap.SetTile((Vector3Int) m_LastPos, null);
                    m_DestroyOldTile = false;
                }
                var currentTile = m_Tilemap.GetTile(newPos);
                if (currentTile != null)
                {
                    if (currentTile.Equals(normalTile))
                    {
                        m_Tilemap.SetTile(newPos, wornTile);
                    }
                    else if (currentTile.Equals(wornTile))
                    {
                        m_Tilemap.SetTile(newPos, dyingTile);
                    }
                    else
                    {
                        m_DestroyOldTile = true;
                    }
                   
                }

                m_LastPos = newPos;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            if (m_DestroyOldTile)
            {
                if (m_LastPos != null) m_Tilemap.SetTile((Vector3Int) m_LastPos, null);
                m_DestroyOldTile = false;
            }
            m_LastPos = null;
            
        }
        
    }

    private bool NearToPlayer(Vector3Int pos)
    {
        var position = player.position;
        Vector3Int playerPos = new Vector3Int((int)position.x,(int)position.y,0);
        return Math.Sqrt( Math.Pow(pos.x - playerPos.x, 2) + Math.Pow(pos.y - playerPos.y, 2) ) < 1.5;
        
    }
}
