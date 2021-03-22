using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLogic : MonoBehaviour
{
    [SerializeField] Rock rockPrefab;
    [SerializeField] Ship shipPrefab;

    [SerializeField] Text scoreText;
    [SerializeField] Text livesText;
    int score;
    const int STARTING_SCORE = 0;
    int lives;
    const int STARTING_LIVES = 3;

    [SerializeField] GameObject clickToSpawn;
    [SerializeField] GameObject newRoundText;
    bool coroutineIsRunning = false;

    private Ship ship;

    public List<Rock> rocks = new List<Rock>();
    int rockCount = 4;
    int maxRocks = 8;
    int radiusOfRockSpawns = 4;

    void Start()
    {
        score = STARTING_SCORE;
        lives = STARTING_LIVES;
    }

    // Update is called once per frame
    void Update()
    {
        //spawn in ship upon click
        if (Input.GetMouseButtonDown(0) && ship == null)
        {
            ship = Instantiate(shipPrefab);
            updateLives(-1);
            clickToSpawn.SetActive(false);
        }

        if (!ship)
        {
            clickToSpawn.SetActive(true);
        }

        if (rocks.Count == 0 && ship != null)
        {
            StartCoroutine(delayRound(2.2f));
            //SpawnRocks(rockCount);
            //rockCount++;
        }

        if (lives <= 0 && ship == null)
        {
            newGame();
        }
    }

    void newGame()
    {
        //clear the rock list, lives = startinglives, score = startingscore, rockCount reset
        foreach (Rock rock in rocks)
        {
            Destroy(rock.gameObject);
        }
        rocks.Clear();
        lives = STARTING_LIVES;
        score = STARTING_SCORE;
        rockCount = 4;
    }

    void SpawnRocks(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnRocksInCircle(ship.transform.position, radiusOfRockSpawns);
        }
    }

    void SpawnRocksInCircle(Vector3 center, int radius)
    {
        float randomAngle = Random.Range(-360, 360);
        Vector3 rockPosition;
        rockPosition.x = center.x + radius * Mathf.Cos(randomAngle * Mathf.Deg2Rad); 
        rockPosition.y = center.y + radius * Mathf.Sin(randomAngle * Mathf.Deg2Rad);
        rockPosition.z = center.z;

        Rock rock = Instantiate(rockPrefab);
        rock.transform.position = rockPosition;
        rock.MainRef = this;

        rocks.Add(rock);
    }

    public void BreakRock(int rockCount, Vector3 position, float size)
    {
        for (int i = 0; i < rockCount; i++)
        {
            Rock rock = Instantiate(rockPrefab);
            rock.MainRef = this;
            rock.transform.position = position;
            rock.transform.localScale = new Vector3(size, size, size); //1
            rocks.Add(rock);
        }
    }

    public void updateScore(int change)
    {
        score += change;
        scoreText.text = "Score: " + score;
        
        //if score is a multiple of 10, we get another life!!!
        if (score % 10 == 0 && score != 0)
        {
            updateLives(1);
        }
    }

    public void updateLives(int change)
    {
        lives += change;
        livesText.text = "Lives: " + lives;
    }

    private IEnumerator delayRound (float time)
    {
        if (coroutineIsRunning)
            yield break;
        coroutineIsRunning = true;
        newRoundText.SetActive(true);
        yield return new WaitForSeconds(time);
        newRoundText.SetActive(false);

        SpawnRocks(rockCount);
        if (rockCount < maxRocks)
            rockCount++;

        coroutineIsRunning = false;
    }
}