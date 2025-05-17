using UnityEngine;

public class TypeOfFigure : MonoBehaviour
{
    private int numericType = 0;
    public int NumericType
    {
        get
        {
            return numericType;
        }
        set
        {
            numericType = value;
        }
    }
}