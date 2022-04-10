using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGeneration : MonoBehaviour
{
    // size of the world
    public int sizeX = 50;
    public int sizeY = 20;
    
    Cell[,] grid; // array of cells will make up this grid (the world is represented by this grid)

    // the tiles that will be used 
    public GameObject grassTile; 

    public int seed;
    public float xOffSet;
    public float yOffSet;
    public float scale = 0.1f; // scale for the perlin noise 
    public float terDetail;
    public float terHeight;
    public int waterHeight; // this is the highest level of water 
    public float waterLevel = 0.4f; // any noise value over this number is not water and any noise value under this is water

    // Start is called before the first frame update
    void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        //random offsets
        //by adding random offsets to the noise map, we will generate a new map every time
        //float xOffSet = Random.Range(-10000f, 10000f);
        //float yOffSet = Random.Range(-10000f, 10000f);

        xOffSet = Random.Range(-10000f, 10000f);
        yOffSet = Random.Range(-10000f, 10000f);

        // create a noise map
        //noise map will tell us what's water and what's not water
        float[,] noiseMap = new float[sizeX, sizeY];
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffSet, y * scale + yOffSet);
                noiseMap[x, y] = noiseValue;
            }
        }

        // initialize a grid with cells 
        grid = new Cell[sizeX, sizeY];
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Cell cell = new Cell();
                float noiseValue = noiseMap[x, y];
                int z = (int)(Mathf.PerlinNoise((x / 2 + seed) / terDetail, (y / 2 + seed) / terDetail) * terHeight); // generate some height
                if (noiseValue < waterLevel && z < waterHeight)
                {
                    cell.isWater = true;
                    cell.height = 0; 
                }
                else
                {
                    cell.isWater = false;
                    cell.height = z; 
                    grassTile = Instantiate(grassTile, new Vector3(x, z, y), Quaternion.identity);
                }
                grid[x, y] = cell;
            }
        }

        /*
        // now add some height 
        seed = (int)Random.Range(-10000f, 10000f);
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                if(grid[x,y].isWater == false)
                {
                    
                    GenerateHeight(x, y, z);
                }
            }
        }
        */

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
