using UnityEngine;

public class TypeOfFigure : MonoBehaviour
{
    public int numericType { get; private set; } = 0;

    public void SetNumericType(int number)
    {
        numericType = number;
    }
}
