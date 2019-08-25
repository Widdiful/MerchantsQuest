using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rect
{
    int x1, x2, y1, y2;
    public rect(int x, int y, int h, int w)
    {
        x1 = x;
        y1 = y;
        x2 = x + w;
        y2 = y + h;
    }

    public Vector2Int center()
    {
        Vector2Int center = new Vector2Int();
        center.x = (x1 + x2)/2;
        center.y = (y1 + y2)/2;
        return center;
    }

    public bool intersect(rect other)
    {
        return( x1<= other.getX2() && x2 >=other.getX1() && y1 <= other.getY2() && y2 >= other.getY1() );
    }

    public int getX1()
    {
        return x1;
    }

    public int getX2()
    {
        return x2;
    }

    public int getY1()
    {
        return y1;
    }
    
    public int getY2()
    {
        return y2;
    }
}
