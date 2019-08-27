using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEngine.TilemapModule;
public class dungeonGeneration : MonoBehaviour
{
    enum tileType
    {
        wall, floor, stairs, chest,
    }


    public Tilemap tilemap;
    public Tilemap backgroundTiles;
    public TileBase tile;
    public List<TileBase> tileList;
    public List<Sprite> floorSprites;
    public GameObject stairsPrefab;
    public GameObject chestPrefab;
    int xSize;
    int ySize;

    int nRooms;

    public int maxRoomSize, minRoomSize;

    public Transform playerLocation;
    int currentFloorNumber;
    int[,] map;
    const float gridOffset = 0.5f;
    const float chestChance = 0;
    // Start is called before the first frame update
    void Start()
    {
        //localMap = GetComponent<Tilemap>();
        currentFloorNumber = 1;

        xSize = 100;
        ySize = 100;

        nRooms = 20;
        map = new int[xSize,ySize];
        
        //generateMap(map);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incrementFloorCount()
    {
        // do floor number increase and start floor transition steps
        currentFloorNumber++;
        foreach (Transform child in transform) {
             GameObject.Destroy(child.gameObject);
         }

        generateMap();
    }


    public void generateMap()
    {
        for (int x = 0; x < map.GetUpperBound(0) ; x++) //Loop through the width of the map
        {
            for (int y = 0; y < map.GetUpperBound(1); y++) //Loop through the height of the map
            {
                // fill map with 0's so it's all dirt 
                map[x,y] = (int)tileType.wall;
            }
        }
        tilemap.ClearAllTiles();

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

            map = createRoom(newRoom, map);
            Vector2Int newCenter = newRoom.center();
            
            if(numRooms == 0)
            {
                playerLocation.position = new Vector3(newCenter.x + gridOffset, newCenter.y + gridOffset, 0);
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
            map = spawnChest(newCenter, map);
            roomList.Add(newRoom);
            numRooms++;
        }
        Vector2Int stairPos = roomList[numRooms-1].center();
        map[stairPos.x, stairPos.y] = (int)tileType.stairs;
        setTileMap(map);
    }


    void setTileMap(int[,] map)
    {
        // set tilemap based on map.
        for (int x = 0; x < map.GetUpperBound(0) ; x++) //Loop through the width of the map
        {
            for (int y = 0; y < map.GetUpperBound(1); y++) //Loop through the height of the map
            {

                switch (map[x,y])
                {
                    case (int)tileType.wall:
                        tilemap.SetTile(new Vector3Int(x, y, 0), tileList[0]); 
                        break;
                    case (int)tileType.floor:
                        Tile tempTile = ScriptableObject.CreateInstance<Tile>();
                        tempTile.sprite = floorSprites[Random.Range(0,floorSprites.Count)];  
                        backgroundTiles.SetTile(new Vector3Int(x, y, 0), tempTile);
                        break;
                    case (int)tileType.stairs:                        
                        GameObject stair = Instantiate(stairsPrefab, new Vector3(x + gridOffset, y + gridOffset, 0), Quaternion.identity);
                        stair.transform.parent = gameObject.transform;
                        stair.GetComponent<stairs>().source = this;
                        break;
                    case (int)tileType.chest:
                        GameObject chest = Instantiate(chestPrefab, new Vector3(x + gridOffset, y + gridOffset, 0), Quaternion.identity);
                        chest.transform.parent = gameObject.transform;

                        Tile tempTile2 = ScriptableObject.CreateInstance<Tile>();
                        tempTile2.sprite = floorSprites[Random.Range(0,floorSprites.Count)];  
                        backgroundTiles.SetTile(new Vector3Int(x, y, 0), tempTile2);
                        
                        break;
                    default:
                        break;
                }
            }
        }
    }


    int[,] spawnChest(Vector2Int chestPos, int[,] map)
    {
        if(Random.Range(0,100)>chestChance)
        {
            map[chestPos.x, chestPos.y] = (int)tileType.chest;
        }
        return map;
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
        for (int x = min(x1, x2); x < max(x1, x2)+1; x++)
        {
            for(int yAlter= max(1, y-1); yAlter < min(y+2, ySize-1); yAlter++)
            {
                if(map[x, yAlter] != (int)tileType.chest)
                    map[x, yAlter] = (int)tileType.floor;
            }
        }

        return map;
    }

    int[,] createVTunnel(int y1, int y2, int x, int[,] map)
    {
        for (int y = min(y1, y2); y < max(y1, y2)+1; y++)
        {   
            for(int xAlter= max(1, x-1); xAlter < min(x+2, xSize-1); xAlter++)
            {
                if(map[xAlter, y] != (int)tileType.chest)
                    map[xAlter, y] = (int)tileType.floor;
            }
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

    public int getFloorNumber()
    {
        return currentFloorNumber;
    }

    public void setFloorNumber(int floorNumber)
    {
        currentFloorNumber = floorNumber;
    }

}
