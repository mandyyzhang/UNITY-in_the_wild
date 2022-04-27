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
    public GameObject apple;

    public GameObject player;

    public GameObject[] treePrefabs; //TRYINGOUTTTTTT PUT TREEESSS INNNN
    public GameObject[] naturePrefabs;  ///////
    public GameObject[] grassPrefabs;   ///////

    // animal game objects
    public GameObject chicken;

    // list of positions where land is empty (no trees, no gems)
    // List<Vector3> emptyLand = new List<Vector3>();

    int seed;
    float xOffSet;
    float yOffSet;

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

        GenerateTrees(); ////////////
        GenerateNature(); ///////////
        GenerateGrass(); ////////////

        SpawnPort();
        SpawnPlayer();
        SpawnGem();

        // spawn animals here?
        // SpawnAnimal()
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
    void GenerateTrees() {
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
                            tree.transform.localScale = Vector3.one * Random.Range(.2f, 1.0f);
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
            Vector3 spawnPos = landRegion(grid);
            player.transform.position = spawnPos;
            // new Vector3(30, 20, 99); //(10, 4, 99)
        }
        else
        {
            player.transform.position = new Vector3(0, 0, 0);
        }
        player.SetActive(true);
    }

    Vector3 landRegion(Cell[,] grid) // get random land position
    {

        List<Vector3> land = new List<Vector3>(); // store xy position of land cell
        
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Vector3 blockPos = blockGrid[x, y];
                if (blockPos != Vector3.zero ) // 0 means water
                {
                    land.Add(blockPos);
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

        GameObject appleToPlace = Instantiate(apple, gemSpawnPos, Quaternion.identity);

        // this sets the itemType
        // can also set from the inspector
        appleToPlace.GetComponent<ItemType>().itemType = "apple";

        // TO DO: spawn first gem near player
        for (int c = 0; c < 5; c++)
        {
            Vector3 spawnPos = landRegion(grid);
            GameObject gemToPlace = Instantiate(gem, spawnPos, Quaternion.identity);
            // this sets the itemType
            gemToPlace.GetComponent<ItemType>().itemType = "glass shard";
            Debug.Log(spawnPos);
        }
    }

    // specify what kind of animal to spawn here
    void SpawnAnimal(GameObject animal)
    {
        int randX = Random.Range(0, sizeX-1);
        int randY = Random.Range(0, sizeY-1);



    }

    void checkGrid() // FOR DEBUGGING BLOCK GRID ONLY
    {

        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Vector3 block = blockGrid[x, y];
                Debug.Log("x: " + block.x + " y: " + block.y + " z: " + block.z );
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
