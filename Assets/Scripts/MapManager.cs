using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    public Transform Block;

    bool[][] grid = null;
    public int gridWidthWorld;
    public int gridHeightWorld;
    public int gridWidthArea;
    public int gridHeightArea;
    public int startWidth;
    public int startHeight;

    public PoolsManager Pool;
    public Player player;
    public Player player2;

    Vector3[] startPos;

    void Start()
    {
        if (grid == null)
        {
            grid = new bool[gridWidthWorld][];
            for (int i = 0; i < gridWidthWorld; i++)
            {
                grid[i] = new bool[gridHeightWorld];
            }
        }

        generateWorldInit();

        startPos = new Vector3[2];
        startPos[0] = player.transform.position;
        startPos[1] = player2.transform.position;
    }

    void Update()
    {
        int indX = (int)(player.transform.position.x / gridWidthArea);
        int indY = (int)(player.transform.position.y / gridHeightArea);
        float localX = player.transform.position.x - indX * gridWidthArea;
        float localY = player.transform.position.y - indY * gridHeightArea;
        if (localX < gridWidthArea * 0.5f)
        {
            if (grid[indX - 1][indY])
                generateWorld(indX - 1, indY);
            if (localY < gridHeightArea * 0.5f)
            {
                if (grid[indX][indY - 1])
                    generateWorld(indX, indY - 1);
                if (grid[indX - 1][indY - 1])
                    generateWorld(indX - 1, indY - 1);
            }
            else if (localY > gridHeightArea * 0.5f)
            {
                if (grid[indX][indY + 1])
                    generateWorld(indX, indY + 1);
                if (grid[indX - 1][indY + 1])
                    generateWorld(indX - 1, indY + 1);
            }
        }
        else if (localX > gridWidthArea * 0.5f)
        {
            if (grid[indX + 1][indY])
                generateWorld(indX + 1, indY);
            if (localY < gridHeightArea * 0.5f)
            {
                if (grid[indX][indY - 1])
                    generateWorld(indX, indY - 1);
                if (grid[indX + 1][indY - 1])
                    generateWorld(indX + 1, indY - 1);
            }
            else if (localY > gridHeightArea * 0.5f)
            {
                if (grid[indX][indY + 1])
                    generateWorld(indX, indY + 1);
                if (grid[indX + 1][indY + 1])
                    generateWorld(indX + 1, indY + 1);
            }
        }
    }

    void generateWorldInit()
    {
        for (int i = 0; i < gridWidthWorld; i++)
        {
            for (int j = 0; j < gridHeightWorld; j++)
            {
                grid[i][j] = true;
            }
        }
        grid[gridWidthWorld / 2][gridHeightWorld / 2] = false;

        int minX = gridWidthArea / 2 - startWidth / 2;
        int maxX = gridWidthArea / 2 + startWidth / 2;
        int minY = gridHeightArea / 2 - startHeight / 2;
        int maxY = gridHeightArea / 2 + startHeight / 2;
        float offsetX = (gridWidthWorld / 2) * gridWidthArea + 0.5f;
        float offsetY = (gridHeightWorld / 2) * gridHeightArea + 0.5f;
        for (int i = 0; i < gridWidthArea; i++)
        {
            for (int j = 0; j < gridHeightArea; j++)
            {
                if (i < minX || i >= maxX || j < minY || j >= maxY)
                    Pool.Spawn(Block, new Vector3(i + offsetX , j + offsetY, 0f), Quaternion.identity);
            }
        }
    }

    void generateWorld(int x, int y)
    {
        grid[x][y] = false;

        float offsetX = x * gridWidthArea + 0.5f;
        float offsetY = y * gridHeightArea + 0.5f;
        for (int i = 0; i < gridWidthArea; i++)
        {
            for (int j = 0; j < gridHeightArea; j++)
            {
                Pool.Spawn(Block, new Vector3(i + offsetX , j + offsetY, 0f), Quaternion.identity);
            }
        }
    }

    public void reset()
    {
        Pool.DespawnAll();
        Player temp = player;
        player = player2;
        player2 = temp;
        player.transform.position = startPos[player.index];
        player2.transform.position = startPos[player.index];
        player.hasDrill = true;
        player.hasDrill = false;
        generateWorldInit();
    }
}
