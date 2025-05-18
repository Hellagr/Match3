using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] int amountOfSameFigures = 3;
    [SerializeField] int amountOfTypesOfFigures = 7;
    [SerializeField] GameObject defaultPrefab;
    [SerializeField] float objectScallerMultiplier = 0.7f;

    private List<GameObject> figures = new();
    public List<GameObject> Figures
    {
        get
        {
            return figures;
        }
    }

    HashSet<string> existingTypes = new();

    void Awake()
    {
        GenerateFigures();
    }

    public void GenerateFigures()
    {

        for (int i = 0; i < amountOfTypesOfFigures; i++)
        {
            string key = GenerateUniqFigure(ref i);
            InitFigure(key);
        }
    }

    private string GenerateUniqFigure(ref int i)
    {
        StringBuilder numericalTypeOfFigure = new StringBuilder();

        for (int j = 0; j < defaultPrefab.transform.childCount; j++)
        {
            System.Random rnd = new System.Random();
            var randomChild = rnd.Next(0, defaultPrefab.transform.GetChild(j).childCount);

            numericalTypeOfFigure.Append(randomChild.ToString());
        }

        var key = numericalTypeOfFigure.ToString();

        if (!existingTypes.Contains(key))
        {
            existingTypes.Add(key);
        }
        else
        {
            i--;
            GenerateUniqFigure(ref i);
        }

        return key;
    }

    private void InitFigure(string key)
    {
        var figure = Instantiate(defaultPrefab, transform.position, Quaternion.identity, transform);

        for (int k = 0; k < figure.transform.childCount; k++)
        {
            GameObject parameterOfFigure = figure.transform.GetChild(k).gameObject;

            char ñharOfKey = key[k];
            int element = ñharOfKey - '0';

            GameObject typeOfParameter = parameterOfFigure.transform.GetChild(element).gameObject;

            typeOfParameter.SetActive(true);
        }

        figure.GetComponent<TypeOfFigure>().NumericType = int.Parse(key);
        figure.transform.localScale = Vector2.one * objectScallerMultiplier;
        figure.SetActive(false);

        for (int l = 0; l < amountOfSameFigures; l++)
        {
            figures.Add(figure);
        }
    }
}
