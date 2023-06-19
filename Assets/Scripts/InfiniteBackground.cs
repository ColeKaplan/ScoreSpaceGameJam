using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public GameObject bushPrefab;
    public GameObject bigRockPrefab;
    public GameObject smallRockPrefab;
    public GameObject sandPrefab;
    public GameObject sproutPrefab;

    public float bkgWidth = 20; 
    public float spawnDistance = 5f;
    public float despawnDistance = 10f; 

    private Transform characterTransform;
    private float lastSpawnPosition;
    private bool firstFlag = true; 

    private void Start()
    {
        characterTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lastSpawnPosition = characterTransform.position.x;
    }

    private void Update()
    {
        float characterPosition = characterTransform.position.x;
        //Debug.Log(characterPosition-lastSpawnPosition);

        if (characterPosition - lastSpawnPosition > spawnDistance)
        {
            Debug.Log("creating");
            SpawnBackground();
            Debug.Log("its been create");
            lastSpawnPosition = characterPosition;
            
        }
        RemoveBackground(characterPosition);
    }

    private void SpawnBackground()
    {
        GameObject newBackground = Instantiate(backgroundPrefab, transform);
        newBackground.transform.position = new Vector3(lastSpawnPosition + spawnDistance + bkgWidth, 0f, 0f);
    }

    private void RemoveBackground(float characterPosition)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform background = transform.GetChild(i);
            if (background.position.x < characterPosition - despawnDistance)
            {
                Destroy(background.gameObject);
            }
        }
    }
}