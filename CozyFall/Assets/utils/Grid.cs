using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<TgridObject>
{
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }
    private int width;
    private int height;
    private Vector3 originPos;
    private float cellSize;
    private TgridObject[,] gridArray;

    public Grid(int width, int height, float cellSize, Vector3 originPos, Func<Grid<TgridObject>, int, int, TgridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;

        gridArray = new TgridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x , y);
            }
        }
        for (int x = 0; x < gridArray.GetLength(0); x++) 
        { 
            for(int y = 0; y< gridArray.GetLength(1); y++) 
            { 
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x,y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+ 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

       
    }
    
    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x,y) * cellSize + originPos;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPos).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPos).y / cellSize);
    }

    public void SetGridObject(int x, int y, TgridObject value)
    {
        if (x>= 0 && y>=0 && x< width && y < height) 
        {
            gridArray[x, y] = value;
            if(OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void SetGridObject(Vector3 worldPosition,TgridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public TgridObject GetGridObject(int x, int y) 
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TgridObject);
        }
    }

    public TgridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
}
