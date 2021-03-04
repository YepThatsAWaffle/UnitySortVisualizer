using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectListController : MonoBehaviour
{
    public List<GameObject> gameObjects { get; private set; }
    public GameObject Prefab;

    public const int numBars = 250;
    public const float ypos = -4.3f;
    public const float worldXMin = -8.9f;
    public const float worldXMax = 8.9f;
    public const float worldWidth = worldXMax - worldXMin;
    public const float worldYMin = -5f;
    public const float worldYMax = 5f;
    public const float worldHeight = worldYMax - worldYMin;
    public const float barMaxHeight = worldHeight + worldYMin - ypos;
    public const float barWidth = worldWidth / numBars;
    public const float barHeightIncrement = barMaxHeight / numBars;

    private System.Random rand;

    public ObjectListController(GameObject prefab)
    {
        GameObject Prefab = prefab;
        rand = new System.Random();
        gameObjects = new List<GameObject>();
    }

    public void CreateUnsortedList(GameObject prefab)
    {
        var distinctBarHeights = new List<int>();
        for (int i = 0; i < numBars; i++)
        {
            distinctBarHeights.Add(i);
        }

        for (int i = 0; i < numBars; i++)
        {
            var randBarHeightIndex = rand.Next(distinctBarHeights.Count);
            var randDistinctBarHeight = distinctBarHeights[randBarHeightIndex] + 1;
            distinctBarHeights.RemoveAt(randBarHeightIndex);
            var barXPos = worldXMin + (i * barWidth) + barWidth / 2;

            var go = Instantiate(prefab, new Vector2(barXPos, ypos), Quaternion.identity);
            go.transform.localScale = new Vector3(barWidth, barHeightIncrement * randDistinctBarHeight, 1);
            gameObjects.Add(go);
        }
    }

    public void RepaintAll()
    {
        foreach(var go in gameObjects)
        {
            Repaint(go, Color.red);
        }
    }

    public void Repaint(GameObject go, Color color)
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            var child = go.transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material.color = color;
        }
    }

    public void DestroyObjects()
    {
        foreach (var go in gameObjects)
        {
            Destroy(go);
        }

        gameObjects.Clear();
    }
}
