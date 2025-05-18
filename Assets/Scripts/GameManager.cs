using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject progressBar;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Transform spawnedObjectContainer;
    [SerializeField] ParticleSystem destroyEffect;
    [SerializeField] int amountToDestroy = 3;
    [SerializeField] int conditionToLoose = 7;
    [SerializeField] float scaleSpeed = 2f;

    Dictionary<int, int> numericTypeAmount = new Dictionary<int, int>();

    void OnEnable()
    {
        playerInput.ObjectAddedToBar += PutObjectToBar;
    }

    private void PutObjectToBar(Vector2 point)
    {
        ManageObjects(point);
    }

    private void ManageObjects(Vector2 point)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(point);

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null && hit.collider.enabled == true && hit.collider.gameObject.layer == LayerMask.NameToLayer("Figure"))
        {
            hit.collider.enabled = false;

            Transform targetedObject = FindParent(hit);

            Vector2 lossyScaleObject = new Vector2(targetedObject.lossyScale.x / progressBar.transform.lossyScale.x, targetedObject.lossyScale.y / progressBar.transform.lossyScale.y);

            Vector2 scaleObjectsForBar = lossyScaleObject * 0.9f;

            StartCoroutine(FadeOut(targetedObject, scaleObjectsForBar));
        }
    }

    private IEnumerator FadeOut(Transform parent, Vector2 scaleObjectsForBar)
    {
        var t = 0f;

        while (parent.localScale.x > 0.1 && parent.localScale.y > 0.1)
        {
            t += Time.deltaTime;

            parent.localScale = Vector2.Lerp(parent.localScale, Vector2.zero, t * scaleSpeed);

            yield return new WaitForFixedUpdate();
        }

        parent.localScale = Vector2.zero;

        PutObjectOnBar(parent);

        StartCoroutine(FadeIn(parent, scaleObjectsForBar));
        CaptureObject(parent);
    }

    private IEnumerator FadeIn(Transform parent, Vector2 ScaleObjectsForBar)
    {
        var t = 0f;

        while (parent.localScale.x < ScaleObjectsForBar.x && parent.localScale.y < ScaleObjectsForBar.y)
        {
            t += Time.deltaTime;

            parent.localScale = Vector2.Lerp(parent.localScale, ScaleObjectsForBar, t * scaleSpeed);

            yield return new WaitForFixedUpdate();
        }

        RecalculateObjectsOnBar(parent);

        CheckGameResult(progressBar.transform.childCount, spawnedObjectContainer.childCount);
    }

    private static Transform FindParent(RaycastHit2D hit)
    {
        var parent = hit.collider.gameObject.transform.parent;
        var targetedObject = parent.transform.parent;
        return targetedObject;
    }

    private void PutObjectOnBar(Transform targetedObject)
    {
        targetedObject.transform.SetParent(progressBar.transform);
        targetedObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void RecalculateObjectsOnBar(Transform targetedObject)
    {
        var figureComponent = targetedObject.GetComponent<TypeOfFigure>();
        int numericType = figureComponent.NumericType;

        if (numericTypeAmount.TryGetValue(numericType, out int value))
        {
            int amount = value + 1;

            if (amount >= amountToDestroy)
            {
                numericTypeAmount.Remove(numericType);

                foreach (Transform childTransform in progressBar.transform)
                {
                    var numberOfType = childTransform.GetComponent<TypeOfFigure>().NumericType;

                    if (numberOfType == numericType)
                    {
                        Instantiate(destroyEffect, childTransform.transform.position, Quaternion.identity);
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
    }

    private static void CaptureObject(Transform targetedObject)
    {
        var rbOfTargetedObject = targetedObject.GetComponent<Rigidbody2D>();
        rbOfTargetedObject.bodyType = RigidbodyType2D.Kinematic;
        rbOfTargetedObject.gravityScale = 0;
        rbOfTargetedObject.linearVelocity = Vector2.zero;
        rbOfTargetedObject.angularVelocity = 0f;
    }

    private void CheckGameResult(int objectsInBar, int objectsInContainer)
    {
        if (objectsInBar >= conditionToLoose)
        {
            loseScreen.SetActive(true);
            spawnedObjectContainer.gameObject.SetActive(false);
            gameObject.SetActive(false);

            foreach (Transform child in progressBar.transform)
            {
                Destroy(child.gameObject);
            }
        }

        if (objectsInContainer < 1)
        {
            winScreen.SetActive(true);
        }
    }
    void OnDisable()
    {
        playerInput.ObjectAddedToBar -= PutObjectToBar;
    }
}
