using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ObjectListController : ScriptableObject
{
    public List<GameObject> gameObjects { get; private set; }
    public Dictionary<int, GameObject> objectMap;
    private GameObject SelectedObject;

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
        objectMap = new Dictionary<int, GameObject>();
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
        var distinctList = new DistinctRandomList(NumBars, rand);

        for (int i = 0; i < NumBars; i++)
        {
            var barXPos = worldXMin + (i * barWidth) + barWidth / 2;

            var go = Instantiate(prefab, new Vector2(barXPos, ypos), Quaternion.identity);
            go.transform.localScale = new Vector3(barWidth, barHeightIncrement * distinctList[i], 1);
            gameObjects.Add(go);
            objectMap.Add(go.GetInstanceID(), go);
        }
    }

    public void RepaintAll()
    {
        foreach(var go in gameObjects)
        {
            PaintObject(go, Color.red);
        }
    }

    public void PaintObjectAtId(int Id, Color color)
    {
        PaintObject(GetObjectOfId(Id), color);
    }
    public void PaintObjectAtIndex(int Index, Color color)
    {
        PaintObject(GetObjectOfIndex(Index), color);
    }

    public void PaintObject(GameObject go, Color color)
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
        objectMap.Clear();
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

        PaintObject(a, Color.red);
        PaintObject(b, Color.red);

        var tempX = a.transform.localPosition.x;
        a.transform.localPosition = new Vector3(b.transform.localPosition.x, a.transform.localPosition.y, a.transform.localPosition.z);
        b.transform.localPosition = new Vector3(tempX, a.transform.localPosition.y, a.transform.localPosition.z);
    }

    public void Swap(int aIdx, int bIdx)
    {
        if (!gameObjects.Any())
        {
            return;
        }

        var a = gameObjects[aIdx];
        var b = gameObjects[bIdx];
        gameObjects[aIdx] = b;
        gameObjects[bIdx] = a;

        var tempX = a.transform.localPosition.x;
        a.transform.localPosition = new Vector3(b.transform.localPosition.x, a.transform.localPosition.y, a.transform.localPosition.z);
        b.transform.localPosition = new Vector3(tempX, a.transform.localPosition.y, a.transform.localPosition.z);
    }

    public void SelectObject(int index)
    {
        SelectedObject = GetObjectOfIndex(index);
        PaintObject(GetObjectOfIndex(index), Color.red);
    }

    public void DeselectObject()
    {
        if (SelectedObject != null)
        {
            PaintObject(SelectedObject, Color.white);
        }
    }

    public float GetValueofObjectAtIndex(int index)
    {
        return GetValueOfObject(GetObjectOfIndex(index));
    }
    public float GetValueofObjectOfId(int id)
    {
        return GetValueOfObject(GetObjectOfId(id));
    }

    public GameObject GetObjectOfId(int id)
    {
        return objectMap[id];
    }

    public GameObject GetObjectOfIndex(int index)
    {
        return gameObjects[index];
    }

    public float GetValueOfObject(GameObject go)
    {
        return go.transform.localScale.y;
    }

    public void SetObjectToValue(float value, GameObject go)
    {
        go.transform.localPosition = new Vector3(value, go.transform.localPosition.y, go.transform.localPosition.z);
    }

    public int GetSize()
    {
        return NumBars;
    }
}
