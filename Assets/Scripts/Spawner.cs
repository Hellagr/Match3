using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform spawnedObjects;
    [SerializeField] Generator generator;
    int nextOrder = 0;

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

            var newObj = Instantiate(objectToSpawn, transform.position, Quaternion.identity, spawnedObjects);
            newObj.SetActive(true);
            newObj.GetComponent<Rigidbody2D>().AddForceY(-20f, ForceMode2D.Impulse);

            var numeric1 = objectToSpawn.GetComponent<TypeOfFigure>().numericType;
            newObj.GetComponent<TypeOfFigure>().SetNumericType(numeric1);
            newObj.AddComponent<SortingGroup>().sortingOrder = nextOrder++;

            generator.Figures.Remove(objectToSpawn);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
