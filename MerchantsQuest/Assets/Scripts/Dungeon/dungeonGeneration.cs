using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEngine.TilemapModule;
public class dungeonGeneration : MonoBehaviour
{
    //Tilemap localMap;
    public Tilemap tilemap;
    public TileBase tile;
    int xSize;
    int ySize;
    // Start is called before the first frame update
    void Start()
    {
        //localMap = GetComponent<Tilemap>();
        xSize = 10;
        ySize = 10;
        int[,] map = new int[xSize,ySize];
        
        for (int x = 0; x < map.GetUpperBound(0) ; x++) //Loop through the width of the map
        {
            for (int y = 0; y < map.GetUpperBound(1); y++) //Loop through the height of the map
            {
                if(Random.Range(1,10)>6)
                {
                    map[x,y] = 1;
                }
                else
                {
                    map[x,y]=0;
                }
                //if (map[x, y] == 1) // 1 = tile, 0 = no tile
                //{
                //    tilemap.SetTile(new Vector3Int(x, y, 0), tile); 
                //}
            }
        }


        for (int x = 0; x < map.GetUpperBound(0) ; x++) //Loop through the width of the map
        {
            for (int y = 0; y < map.GetUpperBound(1); y++) //Loop through the height of the map
            {
                if (map[x, y] == 1) // 1 = tile, 0 = no tile
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile); 
                }
            }
        }
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y  < ySize; y ++)
            {
                //TileBase tile; // Assign a tile asset to this.
                //tile.sprite = wallTile;
               // tilemap.SetTile(new Vector3Int(x,y,0), tile); // Or use SetTiles() for multiple tiles.
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
