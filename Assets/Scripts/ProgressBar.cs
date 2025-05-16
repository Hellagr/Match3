using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Transform spawnedObjectContainer;
    [SerializeField] int amountToDestroy = 3;
    [SerializeField] int conditionToLoose = 7;

    Dictionary<int, int> numericTypeAmount = new Dictionary<int, int>();

    void OnEnable()
    {
        playerInput.ObjectAddedToBar += PutObjectToBar;
    }

    private void PutObjectToBar(Vector2 point)
    {
        ManageObjects(point);

        void OnDisable()
        {
            playerInput.ObjectAddedToBar -= PutObjectToBar;
        }
    }

    private void ManageObjects(Vector2 point)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(point);

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Figure"))
        {
            var parent = hit.collider.gameObject.transform.parent;
            var targetedObject = parent.transform.parent;

            var rbOfTargetedObject = targetedObject.GetComponent<Rigidbody2D>();
            targetedObject.transform.SetParent(transform);
            targetedObject.transform.rotation = Quaternion.Euler(0, 0, 0);

            rbOfTargetedObject.bodyType = RigidbodyType2D.Kinematic;
            rbOfTargetedObject.gravityScale = 0;
            rbOfTargetedObject.linearVelocity = Vector2.zero;
            rbOfTargetedObject.angularVelocity = 0f;

            var typeOfFigure = targetedObject.GetComponent<TypeOfFigure>();
            int numericType = typeOfFigure.numericType;

            if (numericTypeAmount.TryGetValue(numericType, out int value))
            {
                int amount = value + 1;

                if (amount > 2)
                {
                    numericTypeAmount.Remove(numericType);

                    foreach (Transform childTransform in transform)
                    {
                        var numberOfType = childTransform.GetComponent<TypeOfFigure>().numericType;

                        if (numberOfType == numericType)
                        {
                            Destroy(childTransform.gameObject);
                        }
                    }
                }
                else
                {
                    numericTypeAmount[numericType] = amount;
                }
            }
            else
            {
                numericTypeAmount.Add(numericType, 1);
            }

            CheckGameResult(transform.childCount, spawnedObjectContainer.childCount);
        }
    }

    private void CheckGameResult(int objectsInBar, int objectsInContainer)
    {
        if (objectsInBar >= conditionToLoose)
        {
            Debug.Log("Game over");
        }

        if (objectsInContainer < 1)
        {
            Debug.Log("Level complete");
        }
    }
}