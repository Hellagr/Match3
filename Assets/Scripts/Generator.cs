using System.Collections.Generic;
using System.Text;
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

    HashSet<StringBuilder> existingTypes = new();

    void Awake()
    {
        GenerateFigures();
    }

    public void GenerateFigures()
    {

        for (int i = 0; i < amountOfTypesOfFigures; i++)
        {
            var figure = Instantiate(defaultPrefab, transform.position, Quaternion.identity);

            StringBuilder numericalTypeOfFigure = new StringBuilder();

            foreach (Transform transformChild in figure.transform)
            {
                System.Random rnd = new System.Random();
                var randomChild = rnd.Next(0, transformChild.childCount);

                numericalTypeOfFigure.Append(randomChild.ToString());

                GameObject child = transformChild.GetChild(randomChild).gameObject;
                child.SetActive(true);
            }

            //questionable
            if (!existingTypes.Contains(numericalTypeOfFigure))
            {
                existingTypes.Add(numericalTypeOfFigure);
                figure.GetComponent<TypeOfFigure>().SetNumericType(int.Parse(numericalTypeOfFigure.ToString()));
            }
            else
            {
                i--;
                continue;
            }

            figure.SetActive(false);

            for (int j = 0; j < amountOfSameFigures; j++)
            {
                figures.Add(figure);
            }
        }
    }
}
