using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int gridX = 20;
    public static int gridY = 20;
    public static int gridZ = 20;
    // public static int fullGridX = 20;
    // public static int fullGridY = 20;
    // public static int fullGridZ = 20;
    public static Transform[, ,] grid = new Transform[gridX,gridY,gridZ];

    void Start() {
        SpawnNextTetris();
    }
    public static bool IsFullRow(int y)
    {
        for(int x = 8; x < 12; ++x)
        {
            for(int z = 8; z < 12; ++z)
            {
                if(grid[x,y,z] == null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static void DeleteCube(int y)
    {
        for(int x = 8; x < 12; ++x)
        {
            for(int z = 8; z < 12; ++z)
            {
                Destroy(grid[x,y,z].gameObject);
                grid[x,y,z] = null;
            }
        }
    }

    public static void MoveRawDown(int y)
    {
        for(int x = 8; x < 12; ++x)
        {
            for(int z = 8; z < 12; ++z)
            {
                if(grid[x,y,z] != null)
                {
                    grid[x,y -1,z] = grid[x,y,z];
                    grid[x,y,z] = null;
                    grid[x,y-1,z].position += new Vector3(0,-1,0);

                }
            }
        }
    }

    public static void MoveAllRowsDown(int y)
    {
        for(int i = y; i < gridY; i++)
        {
            MoveRawDown(i);
        }
    }

    public static void DeleteRaw()
    {
        for(int y = 0; y < gridY; y++)
        {
            if(IsFullRow(y))
            {
                DeleteCube(y);
                MoveAllRowsDown(y+1);
                --y;
            }

        }
    }

    
    public static bool CheckIsInsideGrid(Vector3 pos)
    {
        return ((int)pos.x >= 0 
            && (int)pos.x < gridX
            && (int)pos.z >= 0 
            && (int)pos.z < gridZ
            && (int)pos.y >= 0);
    }

    public static bool CheckIsInsideRightGrid(Vector3 pos)
    {
        return ((int)pos.x >= 0 
            && (int)pos.z >= 0 
            && (int)pos.z < gridZ
            && (int)pos.y >= 0
            && (int)pos.y < gridY);
    }

    public static bool CheckIsInsideLeftGrid(Vector3 pos)
    {
        return ((int)pos.x < gridX 
            && (int)pos.z >= 0 
            && (int)pos.z < gridZ
            && (int)pos.y >= 0
            && (int)pos.y < gridY);
    }

    public void SpawnNextTetris()
    {
        GameObject nextTetris = (GameObject)Instantiate(Resources.Load(GetRandomTetris(), typeof(GameObject)), new Vector3(10,20,10), Quaternion.identity);
        
    }

    string GetRandomTetris()
    {
        int randomTetris = Random.Range(1,9);
        string randomTetrisName = "Prefabs/FullI-shape";
        switch(randomTetris)
        {
            case 1:
                randomTetrisName = "Prefabs/FullI-shape";
                break;
            case 2:
                randomTetrisName = "Prefabs/L-shape";
                break;
            case 3:
                randomTetrisName = "Prefabs/O-shape";
                break;
            case 4:
                randomTetrisName = "Prefabs/S-shape";
                break;
            case 5:
                randomTetrisName = "Prefabs/ShortI-shape";
                break;
            case 6:
                randomTetrisName = "Prefabs/T-shape";
                break;
            case 7:
                randomTetrisName = "Prefabs/V-shape";
                break;
            case 8:
                randomTetrisName = "Prefabs/HalfI-shape";
                break;
        }
        return randomTetrisName;
    }

    public static Vector3 Round(Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
    }

    public static void UpdateGrid(TetrisControl tetris)
    {
        for(int y = 0; y < gridY; ++y)
        {
            for(int x = 0; x < gridX; ++x)
            {
                for(int z = 0; z < gridZ; ++z)
                {
                    if(grid[x,y,z] != null)
                    {
                        if(grid[x,y,z].parent == tetris.transform)
                        {
                            grid[x,y,z] = null;
                        }
                    }

                }
            }
        }
        foreach(Transform cube in tetris.transform)
        {
            Vector3 pos = Round(cube.position);
            //Debug.Log(pos + "POSSS");
            //Debug.Log(cube+"CUBEE");
            if(pos.y < gridY)
            {
                grid[(int)pos.x, (int)pos.y, (int)pos.z] = cube;
                //Debug.Log(grid[(int)pos.x, (int)pos.y, (int)pos.z]);
            }
        }


    }

    public static void UpdateGridRight(RightTetrisControl tetris)
    {
        for(int y = 0; y < gridY; ++y)
        {
            for(int x = 0; x < gridX; ++x)
            {
                for(int z = 0; z < gridZ; ++z)
                {
                    if(grid[x,y,z] != null)
                    {
                        if(grid[x,y,z].parent == tetris.transform)
                        {
                            grid[x,y,z] = null;
                        }
                    }

                }
            }
        }
        foreach(Transform cube in tetris.transform)
        {
            Vector3 pos = Round(cube.position);
            //Debug.Log(pos + "POSSS");
            //Debug.Log(cube+"CUBEE");
            if(pos.y < gridY)
            {
                grid[(int)pos.x, (int)pos.y, (int)pos.z] = cube;
                //Debug.Log(grid[(int)pos.x, (int)pos.y, (int)pos.z]);
            }
        }


    }

    public static void UpdateGridLeft(LeftTetrisControl tetris)
    {
        for(int y = 0; y < gridY; ++y)
        {
            for(int x = 0; x < gridX; ++x)
            {
                for(int z = 0; z < gridZ; ++z)
                {
                    if(grid[x,y,z] != null)
                    {
                        if(grid[x,y,z].parent == tetris.transform)
                        {
                            grid[x,y,z] = null;
                        }
                    }

                }
            }
        }
        foreach(Transform cube in tetris.transform)
        {
            Vector3 pos = Round(cube.position);
            //Debug.Log(pos + "POSSS");
            //Debug.Log(cube+"CUBEE");
            if(pos.y < gridY)
            {
                grid[(int)pos.x, (int)pos.y, (int)pos.z] = cube;
                //Debug.Log(grid[(int)pos.x, (int)pos.y, (int)pos.z]);
            }
        }


    }

    public static Transform GetTransformAtGridPosition(Vector3 pos)
    {
        if(pos.y > gridY -1)
        {
            //Debug.Log("###############");
            return null;
        }
        
        else
        {
            return grid[(int)pos.x, (int)pos.y, (int)pos.z];
        }

    }
}
