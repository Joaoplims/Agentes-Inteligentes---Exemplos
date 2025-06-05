using UnityEngine;
using System;

public class CellularAutomaton : MonoBehaviour
{
    public int width = 50;
    public int height = 50;
    public int iterations = 5;
    public int birthLimit = 4;
    public int deathLimit = 3;
    public float initialFillProbability = 0.45f; // Chance de uma célula começar "viva"

    private int[,] grid;

    void Start()
    {
        GenerateCave();
        VisualizeGrid();
    }

    public void GenerateCave()
    {
        // Inicializa o grid aleatoriamente
        grid = new int[width, height];
        System.Random rand = new System.Random();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = (rand.NextDouble() < initialFillProbability) ? 1 : 0;
            }
        }

        // Aplica as iterações do autômato celular
        for (int i = 0; i < iterations; i++)
        {
            grid = SmoothCave(grid);
        }
    }

    private int[,] SmoothCave(int[,] oldGrid)
    {
        int[,] newGrid = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighborCount = CountAliveNeighbors(oldGrid, x, y);

                // Regras de nascimento/morte
                if (oldGrid[x, y] == 1)
                {
                    newGrid[x, y] = (neighborCount < deathLimit) ? 0 : 1;
                }
                else
                {
                    newGrid[x, y] = (neighborCount > birthLimit) ? 1 : 0;
                }
            }
        }

        return newGrid;
    }

    private int CountAliveNeighbors(int[,] grid, int x, int y)
    {
        int count = 0;

        for (int nx = -1; nx <= 1; nx++)
        {
            for (int ny = -1; ny <= 1; ny++)
            {
                if (nx == 0 && ny == 0) continue; // Ignora a própria célula

                int neighborX = x + nx;
                int neighborY = y + ny;

                // Verifica se está dentro dos limites do grid
                if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                {
                    count += grid[neighborX, neighborY];
                }
                else
                {
                    // Considera bordas como paredes (opcional)
                    count++;
                }
            }
        }

        return count;
    }

    private void VisualizeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tile.transform.position = new Vector3(x, y, 0);
                tile.GetComponent<Renderer>().material.color = (grid[x, y] == 1) ? Color.white : Color.black;
            }
        }
    }
}