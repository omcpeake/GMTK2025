using TMPro;
using UnityEngine;



public class LoopCounter : MonoBehaviour
{
    [Header("Gates")]
    [SerializeField] int lastGateNum = 2;
    private int currentLoop = 0;
    private int currentGate = 0;
    private int nextExpectedGate = 0;
    //private int previousGate = 99;

    [Header("Enemy Spawning")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject simpleEnemy;
    [SerializeField] GameObject projectileEnemy;
    [SerializeField] Transform bossSpawnPoint;
    [SerializeField] GameObject bossEnemy;


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
            SpawnEnemies(currentLoop);
        }

        Debug.Log($"Player has entered gate {currentGate}. Next expected gate is {nextExpectedGate}.");

    }

    private void UpdateUI()
    {
        loopCountText.SetText(currentLoop.ToString());
    }

    private void SpawnEnemies(int currentLoop)
    {
        Debug.Log($"Spawning enemies for loop {currentLoop}.");
        //fixed number spawns
        if (currentLoop == 2)
        {
            //pick a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            //Spawn enemies at the spawn point

            GameObject Enemy = Instantiate(simpleEnemy, spawnPoint.position, spawnPoint.rotation);
            Enemy.transform.parent = transform;

        }







        //constant spawns
        if (currentLoop % 2 == 0 && currentLoop !=2) // spawn 2 simple enemies any time the current loop is even except the first loop
        {
            Transform spawnPoint1 = spawnPoints[Random.Range(0, spawnPoints.Length/2)];
            Transform spawnPoint2 = spawnPoints[Random.Range(spawnPoints.Length/2, spawnPoints.Length)];

            //Spawn more enemies or different types based on the loop count
            GameObject Enemy1 = Instantiate(simpleEnemy, spawnPoint1.position, spawnPoint1.rotation);
            GameObject Enemy2 = Instantiate(simpleEnemy, spawnPoint2.position, spawnPoint2.rotation);

            Enemy1.transform.parent = transform;
            Enemy2.transform.parent = transform;
            // Add more enemy spawning logic here if needed

        }

    }

}
