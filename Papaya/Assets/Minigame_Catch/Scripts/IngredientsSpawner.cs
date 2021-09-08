using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FoodClass
{
    public string name;
    public GameObject[] items;

    public GameObject GetRandomItem()
    {
        return items[Random.Range(0, items.Length-1)];
    }
}


[System.Serializable]
public class FoodSpawnerParams
{
    public string foodClassName;
    public float minSpawnTime;
    public float maxSpawnTime;

    float timer;
    float nextSpawnTime;
    public void ChooseNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
    public void Reset()
    {
        timer = 0.0f;
        ChooseNextSpawnTime();
    }

    public bool Step(float dt)
    {
        timer += dt;
        if (timer > nextSpawnTime)
        {
            Reset();
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class LevelParams
{
    public string levelName;
    public int activationScore;
    public float foodMoveSpeed = 0.1f;
    public FoodSpawnerParams[] spawnerParams;
}

[System.Serializable]
public class CatchGameParams
{
    public FoodClass[] foodClasses;
    public LevelParams[] levels;
    public int pointsPerFood;
}


public class IngredientsSpawner : MonoBehaviour
{
    public float leftExtent;
    public float rightExtent;
    public CatchGameParams gameParams;

    int currentLevelIndex;

    Dictionary<FoodSpawnerParams, float> timers;
    Dictionary<string, FoodClass> foodClasses;
    float spawnTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetCurrentLevel(0);

        //create foodclasses dict for easy access later
        foodClasses = new Dictionary<string, FoodClass>();
        foreach (FoodClass fc in gameParams.foodClasses)
        {
            foodClasses[fc.name] = fc;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RunCurrentLevel(Time.deltaTime);
    }

    //See if we need to change levels based on the score.
    public void AdviseScore(int score)
    {
        //Is there a next level?
        if (currentLevelIndex +1 < gameParams.levels.Length)
        {
            //Is our score good enough?
            LevelParams nextLevel = gameParams.levels[currentLevelIndex+1];
            if (score > nextLevel.activationScore)
            {
                SetCurrentLevel(currentLevelIndex+1);
            }
        }
    }

    LevelParams GetCurrentLevelParams()
    {
        if (currentLevelIndex < gameParams.levels.Length)
        {
            return gameParams.levels[currentLevelIndex];
        }
        return gameParams.levels[gameParams.levels.Length-1]; //return last level
    }

    void SetCurrentLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        LevelParams lp = GetCurrentLevelParams();
        foreach (FoodSpawnerParams fsp in lp.spawnerParams)
        {
            fsp.Reset();
        }
        Debug.Log("Started " + lp.levelName);
    }

    void RunCurrentLevel(float dt)
    {
        LevelParams lp = GetCurrentLevelParams();
        for (int i = 0; i < lp.spawnerParams.Length; i++)
        {
            FoodSpawnerParams fsp = lp.spawnerParams[i];
            if (fsp.Step(dt))
            {
                GameObject g = GetSpawnObject(fsp);
                GameObject spawned = Instantiate(g, GetSpawnPos(), Quaternion.identity);
                //See if we can set the movement speed of objects.
                Move m = spawned.GetComponent<Move>();
                if (m != null)
                {
                    m.moveSpeed = lp.foodMoveSpeed;
                }
            }
        }
    }

    GameObject GetSpawnObject(FoodSpawnerParams fsp)
    {
        GameObject objToSpawn = null;
        if (foodClasses.ContainsKey(fsp.foodClassName))
        {
            objToSpawn = foodClasses[fsp.foodClassName].GetRandomItem();            
        }
        else
        {
            Debug.Log("No food class called " + fsp.foodClassName);
        }
        return objToSpawn;
    }

    Vector3 GetSpawnPos()
    {
        float randval = Random.Range(-leftExtent, rightExtent);
        return transform.position + (Vector3.right * randval); 
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + Vector3.right * rightExtent, new Vector3(1.0f, 2.0f, 0.0f));
        Gizmos.DrawWireCube(transform.position - (Vector3.right * leftExtent), new Vector3(1.0f, 2.0f, 0.0f));
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * rightExtent);
        Gizmos.DrawLine(transform.position, transform.position - (Vector3.right * leftExtent));
        
        
    }
}
