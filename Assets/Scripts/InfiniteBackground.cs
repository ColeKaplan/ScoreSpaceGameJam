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
    private GameObject[] foliageArr;
    private ArrayList spawedFoliage;

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
        foliageArr = new GameObject[] {bushPrefab, bigRockPrefab, smallRockPrefab, sandPrefab, sproutPrefab};
        spawedFoliage = new ArrayList();
    }

    private void Update()
    {
        float characterPosition = characterTransform.position.x;
        //Debug.Log(characterPosition-lastSpawnPosition);

        if (characterPosition - lastSpawnPosition > spawnDistance)
        {
            SpawnBackground();
            SpawnFoliage();
            SpawnFoliage();
            SpawnFoliage();
            lastSpawnPosition = characterPosition;
            
        }
        RemoveBackground(characterPosition);
        RemoveFoliage(characterPosition);
    }

    private void SpawnFoliage()
    {
        int randomIndex = Random.Range(0, foliageArr.Length);
        
        GameObject foliageInstance = Instantiate(foliageArr[randomIndex], transform);
        float randY = Random.Range(-7.0f, 7.0f);
        foliageInstance.transform.position = new Vector3(lastSpawnPosition + spawnDistance + bkgWidth, randY, 0f);
        spawedFoliage.Add(foliageInstance);
        
    }

    private void RemoveFoliage(float characterPosition)
    {
        for (int i = spawedFoliage.Count - 1; i >= 0; i--)
        {
            GameObject foliage = (GameObject)spawedFoliage[i];
            if (foliage.transform.position.x < characterPosition - despawnDistance)
            {
                spawedFoliage.RemoveAt(i);
                Destroy(foliage.gameObject);
            }
        }
        spawedFoliage.TrimToSize();
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