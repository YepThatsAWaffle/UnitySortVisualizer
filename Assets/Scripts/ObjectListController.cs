using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ObjectListController : ScriptableObject
{
    public List<GameObject> gameObjects { get; private set; }

    public const float ypos = -4.3f;
    public const float worldXMin = -8.9f;
    public const float worldXMax = 8.9f;
    public const float worldWidth = worldXMax - worldXMin;
    public const float worldYMin = -5f;
    public const float worldYMax = 5f;
    public const float worldHeight = worldYMax - worldYMin;
    public float barMaxHeight = worldHeight + worldYMin - ypos;
    int NumBars;
    float barWidth;
    float barHeightIncrement;

    private System.Random rand;

    public ObjectListController()
    {
        rand = new System.Random();
        gameObjects = new List<GameObject>();
        UpdateNumBars(250);
    }

    public void UpdateNumBars(int numBars)
    {
        NumBars = numBars;
        barWidth = worldWidth / NumBars;
        barHeightIncrement = barMaxHeight / NumBars;
}

    public void ClearGameObjects()
    {
        if (gameObjects.Any())
        {
            DestroyObjects();
        }
    }

    public void CreateUnsortedList(GameObject prefab)
    {
        ClearGameObjects();
        var distinctBarHeights = new List<int>();
        for (int i = 0; i < NumBars; i++)
        {
            distinctBarHeights.Add(i);
        }

        for (int i = 0; i < NumBars; i++)
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


    public void SwapRandom()
    {
        if(!gameObjects.Any())
        {
            return;
        }

        int aIdx = rand.Next(NumBars);
        int bIdx = rand.Next(NumBars);
        var a = gameObjects[aIdx];
        var b = gameObjects[bIdx];
        gameObjects[aIdx] = b;
        gameObjects[bIdx] = a;

        Repaint(a, Color.red);
        Repaint(b, Color.red);

        var tempX = a.transform.localPosition.x;
        a.transform.localPosition = new Vector3(b.transform.localPosition.x, a.transform.localPosition.y, a.transform.localPosition.z);
        b.transform.localPosition = new Vector3(tempX, a.transform.localPosition.y, a.transform.localPosition.z);
    }
}
