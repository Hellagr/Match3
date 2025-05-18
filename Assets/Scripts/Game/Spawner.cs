using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] Generator generator;

    GameObject spawnedObjects;
    int nextOrder = 0;

    void Start()
    {
        spawnedObjects = GameObject.FindWithTag("SpawnedObjectsPlace");
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        while (generator.Figures.Count > 0)
        {
            System.Random randomObject = new System.Random();
            var objectToSpawn = generator.Figures[randomObject.Next(0, generator.Figures.Count)];

            System.Random randomSpawnPoint = new System.Random();
            var pointToSpawn = spawnPoints[randomObject.Next(0, spawnPoints.Count)];

            var newObj = Instantiate(objectToSpawn, pointToSpawn.position, Quaternion.identity, spawnedObjects.transform);
            newObj.SetActive(true);
            newObj.GetComponent<Rigidbody2D>().AddForceY(-10f, ForceMode2D.Impulse);

            var numeric1 = objectToSpawn.GetComponent<TypeOfFigure>().NumericType;
            newObj.GetComponent<TypeOfFigure>().NumericType = numeric1;
            newObj.AddComponent<SortingGroup>().sortingOrder = nextOrder++;

            generator.Figures.Remove(objectToSpawn);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
