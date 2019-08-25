using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEngine.TilemapModule;
public class dungeonGeneration : MonoBehaviour
{
    enum tileType
    {
        wall, floor,
    }


    public Tilemap tilemap;
    public TileBase tile;
    public List<TileBase> tileList;
    int xSize;
    int ySize;

    int nRooms;

    public int maxRoomSize, minRoomSize;
    // Start is called before the first frame update
    void Start()
    {
        //localMap = GetComponent<Tilemap>();
        xSize = 100;
        ySize = 100;

        nRooms = 10;
        int[,] map = new int[xSize,ySize];
        
        for (int x = 0; x < map.GetUpperBound(0) ; x++) //Loop through the width of the map
        {
            for (int y = 0; y < map.GetUpperBound(1); y++) //Loop through the height of the map
            {
                //if(Random.Range(1,10)>6)
                //{
                //    map[x,y] = 1;
                //}
                //else
                //{
                //    map[x,y]=0;
                //}
                // fill map with 0's so it's all dirt 
                map[x,y] = (int)tileType.wall;
            }
        }

        List<rect> roomList = new List<rect>();
        int numRooms = 0;
        for (int i = 0; i < nRooms; i++)
        {
            int w = Random.Range(minRoomSize, maxRoomSize);
            int h = Random.Range(minRoomSize, maxRoomSize);

            int x = Random.Range(1, xSize - w - 1);
            int y = Random.Range(1, ySize - h - 1);

            rect newRoom = new rect(x,y,h,w);

            if(numRooms != 0)
            {
                for (int j = 0; j < roomList.Count; j++)
                {
                    if(newRoom.intersect(roomList[j]))
                    {
                        break;
                    }
                }
            }

            createRoom(newRoom, map);
            Vector2Int newCenter = newRoom.center();

            if(numRooms == 0)
            {

            }
            else
            {
                Vector2Int lastCenter = roomList[numRooms-1].center();

                if(Random.Range(0,2)>1)
                {
                    createHTunnel(lastCenter.x, newCenter.x, lastCenter.y, map);
                    createVTunnel(lastCenter.y, newCenter.y, newCenter.x, map);
                }
                else
                {
                    createVTunnel(lastCenter.y, newCenter.y, lastCenter.x, map);
                    createHTunnel(lastCenter.x, newCenter.x, newCenter.y, map);
                }
            }

            roomList.Add(newRoom);
            numRooms++;
        }

        setTileMap(map);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void setTileMap(int[,] map)
    {
        // set tilemap based on map.
        for (int x = 0; x < map.GetUpperBound(0) ; x++) //Loop through the width of the map
        {
            for (int y = 0; y < map.GetUpperBound(1); y++) //Loop through the height of the map
            {
                if (map[x, y] == (int)tileType.wall) // 1 = tile, 0 = no tile
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileList[0]); 
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileList[1]);
                }
            }
        }
    }

    int[,] createRoom(rect room, int[,] map)
    {
        for (int x = room.getX1(); x < room.getX2(); x++)
        {
            for (int y = room.getY1(); y < room.getY2(); y++)
            {
                map[x,y] = (int)tileType.floor;
            }
        }

        return map;
    }


    int[,] createHTunnel(int x1, int x2, int y, int[,] map)
    {
        // 
        
        for (int x = min(x1, x2); x < max(x1, x2)+1; x++)
        {
            map[x,y] = (int)tileType.floor;
        }

        return map;
    }

    int[,] createVTunnel(int y1, int y2, int x, int[,] map)
    {
        // 
        
        for (int y = min(y1, y2); y < max(y1, y2)+1; y++)
        {
            map[x,y] = (int)tileType.floor;
        }

        return map;
    }

    int min(int x1,int x2)
    {
	    if(x1 > x2)
		    return x2;
	    else
		    return x1;
    }
    int max(int x1,int x2)
    {
	    if(x1 < x2)
		    return x2;
	    else
		    return x1;
    }

}
