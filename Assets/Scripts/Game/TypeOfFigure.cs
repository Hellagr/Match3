using UnityEngine;

public class TypeOfFigure : MonoBehaviour
{
    private int numericType = -1;
    public int NumericType
    {
        get { return numericType; }
        set
        {
            if (numericType != -1)
            {
                return;
            }
            numericType = value;
        }
    }
}