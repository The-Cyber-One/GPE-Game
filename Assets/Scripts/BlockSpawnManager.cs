using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnManager : Singleton<BlockSpawnManager>, IContainer
{

    public GameObject[] possibleBlocks;
    List<Transform> possibleSpawnPoints;
    public Dictionary<Interactable, Vector2> blockAndSpawn = new Dictionary<Interactable, Vector2>();

    //Don't adjust this!
    [HideInInspector]
    public Vector2 spawnSize = new Vector2(0.35f, 0.35f);

    public Transform Content => transform;

    // Start is called before the first frame update
    void Start()
    {
        possibleBlocks = Resources.LoadAll<GameObject>("Blocks");
        possibleSpawnPoints = new List<Transform>();
        blockAndSpawn = new Dictionary<Interactable, Vector2>();

        for (int i = 0; i < transform.childCount; i++)
        {
            possibleSpawnPoints.Add(transform.GetChild(i));
        }

        foreach (Transform spawn in possibleSpawnPoints)
        {
            SpawnBlock(spawn.position);
            spawn.gameObject.SetActive(false);
        }
    }

    void SpawnBlock(Vector2 spawn)
    {
        GameObject newBlock = Instantiate(possibleBlocks[Random.Range(0, possibleBlocks.Length - 1)]);
        newBlock.GetComponent<BlockSegment>().AddToContainer(this, spawn);

    }

    public void AddInteractable(Interactable interactable, Vector2 worldPosition)
    {
        interactable.transform.position = worldPosition;
        interactable.transform.localScale = spawnSize;
        blockAndSpawn.Add(interactable, worldPosition);
    }

    public void RemoveInteractable(Interactable interactable)
    {
        SpawnBlock(blockAndSpawn[interactable]);
    }
}
