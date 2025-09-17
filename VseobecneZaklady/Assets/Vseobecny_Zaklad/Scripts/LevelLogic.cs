using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelLogic : MonoBehaviour
{
    public string brickTag;
    public UnityEvent OnDestroyedAllBricks;

    int bricksCount = 0;

    private void Start()
    {
        List<GameObject> bricksList = new List<GameObject>();

        GameObject.FindGameObjectsWithTag(brickTag, bricksList);

        bricksCount = bricksList.Count;

        foreach (GameObject brick in bricksList) 
        {
            HealthWithVisuals brickHealth = brick.GetComponent<HealthWithVisuals>();

            brickHealth.OnDeath.AddListener(MinusOneBrick);
        }
    }

    private void MinusOneBrick()
    {
        bricksCount--;
        if (bricksCount <= 0)
        {
            OnDestroyedAllBricks.Invoke();
        }
    }
}
