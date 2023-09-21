using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnManager : MonoBehaviour
{

    public GameObject[] possibleBlocks;
    List<Transform> possibleSpawnPoints;
    List<Transform> spawnPointsTaken;

    int spawnsAvailable;

    // Start is called before the first frame update
    void Start()
    {
        possibleBlocks = Resources.LoadAll<GameObject>("Prefabs/Blocks");

        possibleSpawnPoints = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            possibleSpawnPoints.Add(transform.GetChild(i));
        }

        spawnsAvailable = possibleSpawnPoints.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnsAvailable > 0)
        {
            SpawnBlock();
            spawnsAvailable--;
        }
        //spawnsAvailable = possibleSpawnPoints.Count;
    }

    public void SpawnNewBlock()
    {
        spawnsAvailable++;
    }

    void SpawnBlock()
    {
        GameObject newBlock = Instantiate(possibleBlocks[Random.Range(0, possibleBlocks.Length - 1)]);
        foreach(Transform spawn in possibleSpawnPoints)
        {
            if(spawn.gameObject.activeSelf)
            {
                newBlock.transform.position = spawn.transform.position;
                spawn.gameObject.SetActive(false);
                break;
            }
        }
    }
}
