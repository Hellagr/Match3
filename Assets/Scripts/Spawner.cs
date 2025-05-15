using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform spawnedObjects;
    [SerializeField] Generator generator;

    void Start()
    {
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        while (generator.Figures.Count > 0)
        {
            System.Random rand = new System.Random();
            var objectToSpawn = generator.Figures[rand.Next(0, generator.Figures.Count)];

            var newobj = Instantiate(objectToSpawn, transform.position, Quaternion.identity, spawnedObjects);
            newobj.SetActive(true);
            generator.Figures.Remove(objectToSpawn);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
