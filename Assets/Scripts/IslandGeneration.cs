using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

/*
 goes on the world object
 generates the terrain of the world
*/

public class IslandGeneration : MonoBehaviour
{
    public NavMeshSurface surface;

    // the world
    int sizeX = 55;
    int sizeY = 55;
    Cell[,] grid; // array of cells will make up this grid (the world is represented by this grid)
    Vector3[,] blockGrid; // this will store position of the grass block
    // may need to change blockGrid to be array of cells so can set if grass block is occupied by trees or not

    // tiles/items that will be used spawned
    public GameObject grassTile;
    public GameObject dirtTile;
    public GameObject port;
    public GameObject gem;

    public GameObject player;

    public GameObject[] treePrefabs; //TRYINGOUTTTTTT PUT TREEESSS INNNN
    public GameObject[] naturePrefabs;  ///////
    public GameObject[] grassPrefabs;   ///////

    // animal game objects
    public GameObject goat;
    public GameObject alpaca;
    public GameObject chicken;

    // list of positions where land is empty (no trees, no gems)
    // List<Vector3> emptyLand = new List<Vector3>();

    public int seed;
    public float xOffSet;
    public float yOffSet;

    public float scale = 0.1f; // scale for the perlin noise
    public float terDetail;
    public float terHeight;
    public int waterHeight; // this is the highest level of water
    public float waterLevel = 0.4f; // any noise value over this number is not water and any noise value under this is water

    public float treeNoiseScale = .4f;  //TRYINGOUTTTTTT PUT TREEESSS INNNN
    public float treeDensity = .4f;   //TRYINGOUTTTTTT PUT TREEESSS INNNN

    public float natureNoiseScale = .8f;  ///////
    public float natureDensity = .3f;     ///////

    public float grassNoiseScale = .3f;   //////
    public float grassDensity = .3f;      /////

    public int gemsToSpawn = 5;

    private int sceneNumber;

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

        if (sceneNumber == 1) {  // Island 1
            GenerateTrees(.2f, 1.0f); ////////////
        }
        else if (sceneNumber == 2) {  // Island 2
            GenerateTrees(1.0f, 1.3f); ////////////
        }
        else if (sceneNumber == 3) {  // Island 3
            GenerateTrees(.8f, 1.0f); ////////////
        }
        else if (sceneNumber == 4) {  // Island 4
            GenerateTrees(1.7f, 1.9f); ////////////
        }

        GenerateNature(); ///////////
        GenerateGrass(); ////////////

        //SpawnPort();
        SpawnPlayer();
        SpawnGem();

        checkGrid();

        // spawn animals here?
        SpawnAnimal();
        surface.BuildNavMesh();
    }

    void SetSeedAndOffsets()
    {
        if(sceneNumber == 0) // Island 1
        {
            xOffSet = (float) -7100.375;
            yOffSet = (float) 9689.953;
            seed = 15;
        }
        else if (sceneNumber == 1) // Island 2
        {
            xOffSet = (float) -7100.375;
            yOffSet = (float) 9689.953;
            seed = 15;
        }
        else if (sceneNumber == 2) // Island 3
        {
            xOffSet = (float) -7100.375;
            yOffSet = (float) 9689.953;
            seed = 15;
        }
        else if (sceneNumber == 3) // Island 4
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

        blockGrid = new Vector3[sizeX, sizeY];

        // initialize a grid with cells
        grid = new Cell[sizeX, sizeY]; // fill by row first
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
                    GameObject grassBlock = Instantiate(grassTile, new Vector3(x*2, z, y*2), Quaternion.identity) as GameObject;

                    // buggy if cell is water, blockGrid[x,y] will be 0
                    blockGrid[x,y] = new Vector3((x*2)-0.5f, z+1.25f, (y*2)+1f);
                    cell.pos = new Vector3((x*2)-0.5f, z+1.25f, (y*2)+1f);

                    // fill in tiles below grass with dirt tiles
                    for (int h = z-1; h > 0; h--) {
                        Instantiate(dirtTile, new Vector3(x*2, h, y*2), Quaternion.identity);
                    }
                }
                cell.posX = x;
                cell.posY = y;
                grid[x, y] = cell;
            }
        }

    }

/////////TRYINGOUTTTTTT PUT TREEESSS INNNN
    void GenerateTrees(float minScale, float maxScale) {
            float[,] noiseMap = new float[sizeX, sizeY];
            (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
            for(int y = 0; y < sizeY; y++) {
                for(int x = 0; x < sizeX; x++) {
                    float noiseValue = Mathf.PerlinNoise(x * treeNoiseScale + xOffset, y * treeNoiseScale + yOffset);
                    noiseMap[x, y] = noiseValue;
                }
            }
            for(int y = 0; y < sizeY; y++) {
                for(int x = 0; x < sizeX; x++) {
                    Cell cell = grid[x, y];
                    if(!cell.isWater) {
                        float v = Random.Range(0f, treeDensity);
                        if(noiseMap[x, y] < v) {
                            GameObject prefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
                            GameObject tree = Instantiate(prefab, transform);
                            tree.transform.position = new Vector3(x*2-1, cell.height+1, y*2+1); ////////////////////////
                            tree.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
                            // tree.transform.localScale = Vector3.one * Random.Range(.2f, 1.0f);
                            tree.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
                            cell.isObstacle = true;
                        }
                    }
                }
            }
      }

    void GenerateNature() {
        float[,] noiseMap = new float[sizeX, sizeY];
            (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
                for(int y = 0; y < sizeY; y++) {
                    for(int x = 0; x < sizeX; x++) {
                        float noiseValue = Mathf.PerlinNoise(x * natureNoiseScale + xOffset, y * natureNoiseScale + yOffset);
                        noiseMap[x, y] = noiseValue;
                    }
                  }
        for(int y = 0; y < sizeY; y++) {
            for(int x = 0; x < sizeX; x++) {
                Cell cell = grid[x, y];
                if(!cell.isWater) {
                    float v = Random.Range(0f, natureDensity);
                    if(noiseMap[x, y] < v) {
                        GameObject prefab = naturePrefabs[Random.Range(0, naturePrefabs.Length)];
                        GameObject nature = Instantiate(prefab, transform);
                        nature.transform.position = new Vector3(x*2-1, cell.height+1, y*2+1); //////////////////////
                        nature.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
                        nature.transform.localScale = Vector3.one * Random.Range(.5f, 1.1f);
                    }
                }
            }
        }
    }

    void GenerateGrass() {
            float[,] noiseMap = new float[sizeX, sizeY];
            (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
            for(int y = 0; y < sizeY; y++) {
                for(int x = 0; x < sizeX; x++) {
                    float noiseValue = Mathf.PerlinNoise(x * grassNoiseScale + xOffset, y * grassNoiseScale + yOffset);
                    noiseMap[x, y] = noiseValue;
                }
            }
            for(int y = 0; y < sizeY; y++) {
                for(int x = 0; x < sizeX; x++) {
                    Cell cell = grid[x, y];
                    if(!cell.isWater) {
                        float v = Random.Range(0f, grassDensity);
                        if(noiseMap[x, y] < v) {
                            GameObject prefab = grassPrefabs[Random.Range(0, grassPrefabs.Length)];
                            GameObject grass = Instantiate(prefab, transform);
                            grass.transform.position = new Vector3(x*2-1, cell.height+1, y*2+1);  //////////////////////
                        }
                    }
                }
            }
      }
///////////// end TRYINGOUTTTTTT PUT TREEESSS INNNN
    //}


    void SpawnPort()
    {
        if(sceneNumber == 0) // Island 1
        {
            Instantiate(port, new Vector3(10, 2, 99), Quaternion.identity);
            Instantiate(port, new Vector3(12, 2, 99), Quaternion.identity);
            Instantiate(port, new Vector3(14, 2, 99), Quaternion.identity);
            Instantiate(port, new Vector3(16, 2, 99), Quaternion.identity);
        }
        if(sceneNumber == 1) // Island 2 (needs some changes)
        {
            Instantiate(port, new Vector3(10, 2, 99), Quaternion.identity);
            Instantiate(port, new Vector3(12, 2, 99), Quaternion.identity);
            Instantiate(port, new Vector3(14, 2, 99), Quaternion.identity);
            Instantiate(port, new Vector3(16, 2, 99), Quaternion.identity);
        }
        else
        {
            Instantiate(port, new Vector3(10, 10, 100), Quaternion.identity);
        }
    }

    void SpawnPlayer()
    {
        // if(sceneNumber == 0)
        if(sceneNumber >= 0)
        {
            Vector3 spawnPos = landRegion();
            player.transform.position = spawnPos;
            //player.transform.position = new Vector3(30, 20, 99);
            // new Vector3(30, 20, 99); //(10, 4, 99)

            Vector3 goatPos = landRegion();
            //goat.transform.position = goatPos;
            //goat.GetComponent<NavMeshAgent>().Warp(goatPos);
        }
        else
        {
            player.transform.position = new Vector3(0, 0, 0);
        }
        player.SetActive(true);
    }

    Vector3 landRegion() // get random land position
    {

        List<Vector3> land = new List<Vector3>(); // store xy position of land cell

        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Vector3 blockPos = blockGrid[x, y];
                // trying to use grid
                Cell freeCell = grid[x,y];

                // if (blockPos != Vector3.zero ) // 0 means water
                // {
                //     land.Add(blockPos);
                // }

                if (!freeCell.isObstacle && !freeCell.isWater)
                {
                    land.Add(freeCell.pos);
                }

            }
        }

        int randomLand = Random.Range(0, land.Count);
        return land[randomLand];
    }

    // not done :p
    bool checkHasObstacle() {
        return false;
    }

    void SpawnGem()
    {
        Vector3 playerPos = player.transform.position;

        Vector3 gemSpawnPos = new Vector3(playerPos.x+8, playerPos.y, playerPos.z);

        // TO DO: spawn first gem near player
        for (int c = 0; c < gemsToSpawn; c++)
        {
            Vector3 spawnPos = landRegion();
            //Debug.Log(spawnPos);
            GameObject gemToPlace = Instantiate(gem, spawnPos, Quaternion.identity);

        }
    }

    // specify what kind of animal to spawn here
    void SpawnAnimal(){
        // int randX = Random.Range(0, sizeX-1);
        // int randY = Random.Range(0, sizeY-1);
        Vector3 playerPos = player.transform.position;
        Vector3 animalSpawnPos = new Vector3(playerPos.x+8, playerPos.y + 50, playerPos.z);

        for (int c = 0; c < 3; c++){
            Vector3 spawnPos = landRegion();
            GameObject animalToPlace = Instantiate(alpaca, spawnPos, Quaternion.identity);
            //animalToPlace.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
        }

        for (int c = 0; c < 3; c++){
            Vector3 spawnPos = landRegion();
            GameObject animalToPlace = Instantiate(chicken, spawnPos, Quaternion.identity);
            //animalToPlace.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
        }
    }


    void checkGrid() // FOR DEBUGGING GRID ONLY
    {

        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Cell cell_test = grid[x, y];
                Vector3 cell_pos = cell_test.pos;
                //Debug.Log("x: " + cell_pos.x + " y: " + cell_pos.y + " z: " + cell_pos.z );
                //Debug.Log(cell.)
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(GameObject.FindGameObjectsWithTag("Player")[0].transform.position);
    }
}
