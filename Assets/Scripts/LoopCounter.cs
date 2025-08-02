using TMPro;
using UnityEngine;



public class LoopCounter : MonoBehaviour
{
    [Header("Gates")]
    [SerializeField] int lastGateNum = 2;
    private int currentLoop = 0;
    private int currentLoopWhenLastGateReached = -1; // Used to track the loop count when the last gate is reached
    private int currentGate = 0;
    private int nextExpectedGate = 0;
    //private int previousGate = 99;

    [Header("Enemy Spawning")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject simpleEnemy;
    [SerializeField] GameObject projectileEnemy;
    [SerializeField] GameObject fastEnemy;
    [SerializeField] Transform bossSpawnPoint;
    [SerializeField] GameObject bossEnemy;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gateEnterSound;


    private bool goingForwards = false;

    [SerializeField] private TextMeshProUGUI loopCountText;


    public void OnGateEnter(int gateNumber, Collider2D collision)
    {
        //The player has entered the next gate in the sequence
        //previousGate = currentGate;
        currentGate = gateNumber;


        if (currentGate == nextExpectedGate) //player moving forwards
        {
            goingForwards = true;


            if (currentGate == lastGateNum)
                nextExpectedGate = 0;
            else
                nextExpectedGate++;
        }
        else // player moving backwards
        {
            goingForwards = false;

        }

        //Decide if we need to increase or decrease the loop count
        if (gateNumber == 0)
        {
            if (goingForwards)
                currentLoop++;

            UpdateUI();
            if(currentLoop > currentLoopWhenLastGateReached)
            {
                // If the player has reached a new loop, spawn enemies
                SpawnEnemies(currentLoop);
                audioSource.PlayOneShot(gateEnterSound); // Play the gate enter sound
            }
            currentLoopWhenLastGateReached = currentLoop; // Update the loop count when the last gate is reached
        }


    }

    private void UpdateUI()
    {
        loopCountText.SetText(currentLoop.ToString());
    }

    private void SpawnEnemies(int currentLoop)
    {
        //fixed number spawns
        if (currentLoop == 2)
        {
            //pick a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            //Spawn enemies at the spawn point

            GameObject Enemy = Instantiate(simpleEnemy, spawnPoint.position, spawnPoint.rotation);
            Enemy.transform.parent = transform;

        }

        else if (currentLoop == 5)
        {
            //spawn a projectile enemy at every spawn point
            foreach (Transform spawnPoint in spawnPoints)
            {
                GameObject Enemy = Instantiate(projectileEnemy, spawnPoint.position, spawnPoint.rotation);
                Enemy.transform.parent = transform;
            }

            //Spawn a fast enemy at a random spawn point
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject fastEnemyInstance = Instantiate(fastEnemy, randomSpawnPoint.position, randomSpawnPoint.rotation);
            fastEnemyInstance.transform.parent = transform;
        }
        else if (currentLoop == 6) // spawn a fast enemy at a random spawn point
        {
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject fastEnemyInstance = Instantiate(fastEnemy, randomSpawnPoint.position, randomSpawnPoint.rotation);
            fastEnemyInstance.transform.parent = transform;
        }


        else if (currentLoop == 7) // spawn a simple enemy at every spawn point
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                GameObject Enemy = Instantiate(simpleEnemy, spawnPoint.position, spawnPoint.rotation);
                Enemy.transform.parent = transform;
            }
        }
        else if (currentLoop == 8) // spawn a projectile and simple enemy at every spawn point
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                GameObject Enemy = Instantiate(projectileEnemy, spawnPoint.position, spawnPoint.rotation);
                Enemy.transform.parent = transform;
                GameObject Enemy2 = Instantiate(simpleEnemy, spawnPoint.position, spawnPoint.rotation);
                Enemy2.transform.parent = transform;
            }
        }

        else if (currentLoop == 9)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                int randomEnemyType = Random.Range(0, 2); // Randomly choose between simple and projectile enemy
                if(randomEnemyType == 0)
                {
                    GameObject Enemy = Instantiate(simpleEnemy, spawnPoint.position, spawnPoint.rotation);
                    Enemy.transform.parent = transform;
                }
                else if (randomEnemyType == 1)
                {
                    GameObject Enemy = Instantiate(projectileEnemy, spawnPoint.position, spawnPoint.rotation);
                    Enemy.transform.parent = transform;
                }
                else
                {
                    GameObject Enemy = Instantiate(fastEnemy, spawnPoint.position, spawnPoint.rotation);
                    Enemy.transform.parent = transform;
                }
                
            }
        }

        else if (currentLoop == 10) // spawn a boss enemy at the boss spawn point
        {
            GameObject Enemy = Instantiate(bossEnemy, bossSpawnPoint.position, bossSpawnPoint.rotation);
            Enemy.transform.parent = transform;
        }


        //constant spawns
        if (currentLoop % 2 == 0 && currentLoop != 2) // spawn 2 simple enemies any time the current loop is even except the first loop
        {
            Transform spawnPoint1 = spawnPoints[Random.Range(0, spawnPoints.Length / 2)];
            Transform spawnPoint2 = spawnPoints[Random.Range(spawnPoints.Length / 2, spawnPoints.Length)];

            //Spawn more enemies or different types based on the loop count
            GameObject Enemy1 = Instantiate(simpleEnemy, spawnPoint1.position, spawnPoint1.rotation);
            GameObject Enemy2 = Instantiate(simpleEnemy, spawnPoint2.position, spawnPoint2.rotation);

            Enemy1.transform.parent = transform;
            Enemy2.transform.parent = transform;
            // Add more enemy spawning logic here if needed

        }
        if (currentLoop % 3 == 0) // spawn a projectile enemy every 3 loops
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject Enemy = Instantiate(projectileEnemy, spawnPoint.position, spawnPoint.rotation);
            Enemy.transform.parent = transform;
        }

    }

}
