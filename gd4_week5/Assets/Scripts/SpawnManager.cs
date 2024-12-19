using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float spawnRange = 20;
    public GameObject enemyPrefab;
    public Transform enemyParent;
    public Transform island;
    public float spawnDistanceFromThePivotOfIsland;

    [SerializeField] int enemyWave = 1;

    int enemyCount;
    public GameObject repelPowerUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        enemyCount = FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None).Length;
        //enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemyCount == 0)
        {
            spawnCubes(enemyWave*enemyWave);
            //increase the number of enemies to spawn
            enemyWave++;
            //spawn the new wave
            spawnEnemy(enemyWave);

            if( enemyWave % 3 == 0)
            {
                spawnPowerUp();
            }
        }
    }

    void spawnPowerUp()
    {
        Instantiate(repelPowerUp, spawnPointRelative(), island.rotation);
    }

    void spawnCubes(int cubesToSpawn)
    {
        for(int i = 0; i<cubesToSpawn;i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = transform;
            cube.transform.position = spawnPointRelative() + new Vector3(0,10,0);
            cube.AddComponent<BoxCollider>();
            cube.AddComponent<Rigidbody>();
            cube.GetComponent<Rigidbody>().mass = 0.1f;
            cube.transform.tag = "Enemy";
        }
        
    }
    void spawnEnemy(int enemiesToSpawn)
    {
        for(int i = 0; i< enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, spawnPointRelative(), island.rotation, enemyParent);
        }
    }

    Vector3 spawnPosition()
    {
        float spawnPositionX = Random.Range(-spawnRange, spawnRange);
        float spawnPositionZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomSpawnPosition = new Vector3(spawnPositionX, 0, spawnPositionZ);
        return randomSpawnPosition;
    }
    
    Vector3 spawnPointRelative()
    {
        
        float spawnPositionX = Random.Range(-spawnRange, spawnRange);
        float spawnPositionZ = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnX = island.right * spawnPositionX;
        Vector3 spawnY = island.up * spawnDistanceFromThePivotOfIsland;
        Vector3 spawnZ = island.forward * spawnPositionZ;

        Vector3 randomSpawnPosition = spawnX + spawnY +spawnZ;
        return randomSpawnPosition;

    }
}
