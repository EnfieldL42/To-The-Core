using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class UndergroundSpawner : MonoBehaviour
{
    Vector3 ChunkCenter;
    public Transform player;
    private Dictionary<string, int> tileHealths;
    public List<TileBase> tiles;
    Tilemap tileMap;
    //public TilemapRenderer renderer;
    public int gridX, gridY;
    // Start is called before the first frame update
    void Start()
    {
        tileMap = GetComponent<Tilemap>();
        //renderer = GetComponent<TilemapRenderer>();
        ChunkCenter = player.transform.position;
        Chunk(-gridX, gridY);
        Chunk(0, gridY);
        Chunk(gridX, gridY);
        Chunk(-gridX, 0);        
        Chunk(gridX, 0);
        Chunk(-gridX, -gridY);
        Chunk(0, -gridY);
        Chunk(gridX, -gridY);
    }


    public float scale = 30f;
    public float offset = 2f;
    public float fillThreshold = 0.8f;
    public float rockThreshold = 0.8f;
    public bool IsRock;
    void Chunk(float Gridx, float Gridy)
    {        
        tileHealths = new Dictionary<string, int>();
        for (int y = (int)(ChunkCenter.y+Gridy)-gridY/2; y < (int)(ChunkCenter.y+Gridy) + gridY / 2; y++)
        {
            for (int x = (int)(ChunkCenter.x+Gridx) - gridX / 2; x < (int)(ChunkCenter.x+Gridx) + gridX / 2; x++)
            {
                float sample = Mathf.PerlinNoise(x / scale + offset, y / scale + offset);
                float samplerock = Mathf.PerlinNoise(x / scale + offset * 50f, y / scale + offset * 50f);
                bool generateTilerock = samplerock > rockThreshold ? true : false;
                bool generateTile = sample > fillThreshold ? true : false;
                if (generateTile&!IsRock)
                {
                    tileMap.SetTile(new Vector3Int(x, y, 0), tiles[0]);
                    tileHealths[x.ToString() + y.ToString()] = 5;
                }
                // don't generate tile
                else
                {
                    tileMap.SetTile(new Vector3Int(x, y, 0), null);
                    tileHealths[x.ToString() + y.ToString()] = -1;
                }
                if (!generateTilerock&IsRock)
                {
                    tileMap.SetTile(new Vector3Int(x, y, 0), tiles[1]);
                }

            }

        }
        
        }
   
    void ClearChunk(float Gridx, float Gridy)
    {
        tileHealths = new Dictionary<string, int>();
        for (int y = (int)(ChunkCenter.y + Gridy) - gridY / 2; y < (int)(ChunkCenter.y + Gridy) + gridY / 2; y++)
        {
            for (int x = (int)(ChunkCenter.x + Gridx) - gridX / 2; x < (int)(ChunkCenter.x + Gridx) + gridX / 2; x++)
            {
                                tileMap.SetTile(new Vector3Int(x, y, 0), null);
                    tileHealths[x.ToString() + y.ToString()] = -1;
                                       }
        }
    }

    public float regenThreshold = 0.1f;
    // Update is called once per frame
    void Update()
    {
        
        if((player.transform.position.x > ChunkCenter.x + (gridX)))
        {
            Chunk(gridX*2, gridY);
            Chunk(gridX*2, 0);
            Chunk(gridX*2, -gridY);
            ClearChunk(-gridX, gridY);
            ClearChunk(-gridX, 0);
            ClearChunk(-gridX, -gridY);
           
            ChunkCenter.x = player.transform.position.x;
        }
        if ((player.transform.position.x < ChunkCenter.x - (gridX)))
        {
            Chunk(-gridX*2, gridY);
            Chunk(-gridX * 2, 0);
            Chunk(-gridX * 2, -gridY);
            ClearChunk(gridX, gridY);
            ClearChunk(gridX, 0);
            ClearChunk(gridX, -gridY);
         
            ChunkCenter.x = player.transform.position.x;
        }
        if ((player.transform.position.y < ChunkCenter.y - (gridY)))
        {
            Chunk(gridX, -gridY * 2);
            Chunk(0, -gridY * 2);
            Chunk(-gridX, -gridY * 2);
            ClearChunk(gridX, gridY);
            ClearChunk(0, gridY);
            ClearChunk(-gridX, gridY);
          
            ChunkCenter.y = player.transform.position.y;
        }
        if ((player.transform.position.y > ChunkCenter.y + (gridY)))
        {
            Chunk(gridX, gridY * 2);
            Chunk(0, gridY * 2);
            Chunk(-gridX, gridY * 2);
            ClearChunk(gridX, -gridY);
            ClearChunk(0, -gridY);
            ClearChunk(-gridX, -gridY);
       
            ChunkCenter.y = player.transform.position.y;
        }




    }
}
