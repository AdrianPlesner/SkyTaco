using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundScript : MonoBehaviour
{
    [SerializeField] public Tile bg;
    [SerializeField] public Transform follow;
    private Tilemap m_Tilemap;
    private int m_MAXHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Tilemap = GetComponent<Tilemap>();
        m_MAXHeight = (int)follow.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        int newHeight = (int) follow.position.y + 20;
        if (newHeight > m_MAXHeight)
        {
            var tiles = getTiles(m_MAXHeight, newHeight);
            TileBase[] sprites = new TileBase[tiles.Length];
            for (int i = 0; i < sprites.Length; i++)
                sprites[i] = bg;
            m_Tilemap.SetTiles(tiles,sprites);
        }

        m_MAXHeight = newHeight;
    }

    private Vector3Int[] getTiles(int oldHeight, int newHeight)
    {
        var result = new List<Vector3Int>();
        for (int i = oldHeight; i < newHeight ; i++)
        {
            for (int j = -9; j < 10; j++)
            {
                result.Add(new Vector3Int(j, i, 0));
            }
            
        }
        return result.ToArray();
    }
}
