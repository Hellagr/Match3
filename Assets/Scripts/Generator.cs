using System;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] int amountOfSameFigures = 3;
    [SerializeField] int amountOfTypesOfFigures = 7;
    [SerializeField] GameObject defaultPrefab;

    private List<GameObject> figures = new();
    public List<GameObject> Figures
    {
        get
        {
            return figures;
        }
    }

    void Awake()
    {
        GenerateFigures();
    }

    public void GenerateFigures()
    {

        for (int i = 0; i < amountOfTypesOfFigures; i++)
        {
            var figure = Instantiate(defaultPrefab, transform.position, Quaternion.identity);

            foreach (Transform transformChild in figure.transform)
            {
                System.Random rnd = new System.Random();
                var randomChild = rnd.Next(0, transformChild.childCount);

                GameObject child = transformChild.GetChild(randomChild).gameObject;
                child.SetActive(true);
            }

            figure.SetActive(false);

            //problem with the same figures
            for (int j = 0; j < amountOfSameFigures; j++)
            {
                figures.Add(figure);
            }
        }
    }
}
