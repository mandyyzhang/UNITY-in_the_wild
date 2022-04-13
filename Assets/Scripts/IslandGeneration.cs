using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 goes on the world object
 generates the terrain of the world 
 */

public class IslandGeneration : MonoBehaviour
{
    // the world 
    int sizeX = 100;
    int sizeY = 100;
    Cell[,] grid; // array of cells will make up this grid (the world is represented by this grid)

    // tiles/items that will be used spawned 
    public GameObject grassTile;
    public GameObject port;

    public GameObject player;

    int seed;
    float xOffSet;
    float yOffSet;

    public float scale = 0.1f; // scale for the perlin noise 
    public float terDetail;
    public float terHeight;
    public int waterHeight; // this is the highest level of water 
    public float waterLevel = 0.4f; // any noise value over this number is not water and any noise value under this is water

    int sceneNumber;

    void Awake()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        SetSeedAndOffsets(); // set the seeds and offsets for each island (so we can get the same map each time for each island) 
        GenerateTerrain();
        SpawnPort();
        SpawnPlayer();
    }

    void SetSeedAndOffsets()
    {
        if(sceneNumber == 0) // Island 1 
        {
            xOffSet = (float) -7100.375;
            yOffSet = (float) 9689.953;
            seed = 15;
        }
        else {
            //random offsets
            //by adding random offsets to the noise map, we will generate a new map every time
            xOffSet = Random.Range(-10000f, 10000f);
            yOffSet = Random.Range(-10000f, 10000f);
            
            Debug.Log(xOffSet);
            Debug.Log(yOffSet);
            Debug.Log(seed);

        }
    }

    void GenerateTerrain()
    {
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
        

    }

    void SpawnPort()
    {
        if(sceneNumber == 0) // Island 1 
        {
            port = Instantiate(port, new Vector3(10, 2, 99), Quaternion.identity);
            port = Instantiate(port, new Vector3(12, 2, 99), Quaternion.identity);
            port = Instantiate(port, new Vector3(14, 2, 99), Quaternion.identity);
            port = Instantiate(port, new Vector3(16, 2, 99), Quaternion.identity);
        }
        else
        {
            port = Instantiate(port, new Vector3(10, 10, 100), Quaternion.identity);
        }
    }

    void SpawnPlayer()
    {
        if(sceneNumber == 0)
        {
            player.transform.position = new Vector3(10, 4, 99);
        }
        else
        {
            player.transform.position = new Vector3(0, 0, 0);
        }
        player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(GameObject.FindGameObjectsWithTag("Player")[0].transform.position);
    }
}
