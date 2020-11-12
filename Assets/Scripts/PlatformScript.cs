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
    [SerializeField] public Transform cam;
    [SerializeField] public Transform player;
    
    private Tilemap m_Tilemap;

    private Random m_Random;
    // Start is called before the first frame update
    void Start()
    {
        m_Tilemap = GetComponent<Tilemap>();
        InvokeRepeating(nameof(NewTile), 2f,2f);
    }

    // Update is called once per frame
    void Update()
    {


    }

    void NewTile()
    {
        var position = cam.position;
        int x = (int)Random.Range(position.x - 9, position.x + 9);
        int y = (int)Random.Range(position.y - 5, position.y + 5);
        Vector3Int pos = new Vector3Int(x,y,0);
        if (m_Tilemap.GetTile(pos) == null && !NearToPlayer(pos))
            m_Tilemap.SetTile(pos, normalTile);
        else
            NewTile();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            var pos = m_Tilemap.WorldToCell(other.collider.transform.position);
            pos.y -= 1;
            var tile = m_Tilemap.GetTile(pos);
            if (tile != null )
            {
                if (tile.Equals(normalTile))
                {
                    m_Tilemap.SetTile(pos,wornTile);
                }
                else if (tile.Equals(wornTile))
                {
                    m_Tilemap.SetTile(pos, dyingTile);
                }
                else
                {
                    m_Tilemap.SetTile(pos, null);
                }
            }
        }
    }

    private bool NearToPlayer(Vector3Int pos)
    {
        var position = player.position;
        Vector3Int playerPos = new Vector3Int((int)position.x,(int)position.y,0);
        return Math.Sqrt( Math.Pow(pos.x - playerPos.x, 2) + Math.Pow(pos.y - playerPos.y, 2) ) < 1.5;
    }
}
