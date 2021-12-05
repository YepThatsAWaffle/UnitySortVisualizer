﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ObjectListController : ScriptableObject
{
    public List<GameObject> gameObjects { get; private set; }
    private GameObject[] SelectedObjects;
    public float ypos;
    public float worldXMin;
    public float worldXMax;
    public float worldWidth;
    public float worldYMin;
    public float worldYMax;
    public float worldHeight;
    public float barMaxHeight;
    int NumBars;
    float barWidth;
    float barHeightIncrement;

    private System.Random rand;

    public ObjectListController()
    {
        SelectedObjects = new GameObject[3];
        rand = new System.Random();
        gameObjects = new List<GameObject>();
        UpdateNumBars(256);
    }

    public void UpdateNumBars(int numBars)
    {
        NumBars = numBars;
    }

    public void ClearGameObjects()
    {
        if (gameObjects.Any())
        {
            DestroyObjects();
        }
    }

    private void GetBounds()
    {
        var lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        var upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        worldXMin = lowerLeft.x;
        worldXMax = upperRight.x;
        worldWidth = Mathf.Abs(worldXMax) + Mathf.Abs(worldXMin);
        worldYMin = lowerLeft.y;
        ypos = worldYMin + 1f;
        worldYMax = upperRight.y;
        worldHeight = Mathf.Abs(worldYMax);
        barMaxHeight = (worldHeight - ypos);
        barHeightIncrement = barMaxHeight / NumBars;
        barWidth = worldWidth / NumBars;
    }

    public void CreateUnsortedList(GameObject prefab, int listState, int direction)
    {
        GetBounds();
        ClearGameObjects();

        var distinctList = new DistinctRandomList(NumBars, rand, listState, direction);

        for (int i = 0; i < NumBars; i++)
        {
            var go = CreateGameObject(prefab, i, distinctList[i]);
            gameObjects.Add(go);
        }
    }

    private GameObject CreateGameObject(GameObject prefab, int xIdx, int yScale)
    {
        var barXPos = worldXMin + (xIdx * barWidth) + barWidth / 2;

        var go = Instantiate(prefab, new Vector2(barXPos, ypos), Quaternion.identity);
        go.transform.localScale = new Vector3(barWidth, barHeightIncrement * yScale, 1);
        return go;
    }

    public void RepaintAll()
    {
        foreach(var go in gameObjects)
        {
            PaintObject(go, Color.red);
        }
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
        DeselectAll();

        foreach (var go in gameObjects)
        {
            Destroy(go);
        }

        gameObjects.Clear();
    }

    public void Swap(int aIdx, int bIdx)
    {
        if (!gameObjects.Any())
        {
            return;
        }

        var a = GetObjectOfIndex(aIdx);
        var b = GetObjectOfIndex(bIdx);

        SwapBars(a, b);

        gameObjects[aIdx] = b;
        gameObjects[bIdx] = a;
    }

    private void SwapBars(GameObject a, GameObject b)
    {
        var tempX = a.transform.localPosition.x;
        a.transform.localPosition = new Vector3(b.transform.localPosition.x, a.transform.localPosition.y, a.transform.localPosition.z);
        b.transform.localPosition = new Vector3(tempX, a.transform.localPosition.y, a.transform.localPosition.z);
    }

    public void SelectObject(int index)
    {
        DeselectObject(1);
        SelectedObjects[1] = GetObjectOfIndex(index);
        PaintObject(SelectedObjects[1], Color.red);
    }

    public void SelectPivotObject(int index)
    {
        DeselectObject(0);
        SelectedObjects[0] = GetObjectOfIndex(index);
        PaintObject(SelectedObjects[0], Color.cyan);
    }

    public void SelectCompareObject(int index)
    {
        DeselectObject(2);
        SelectedObjects[2] = GetObjectOfIndex(index);
        PaintObject(SelectedObjects[2], Color.green);
    }

    public void DeselectObject(int type)
    {
        if (ObjectSelected(type))
        {
            PaintObject(SelectedObjects[type], Color.white);
            SelectedObjects[type] = null;
        }
    }

    public void DeselectAll()
    {
        for (int i = 0; i < SelectedObjects.Length; i++)
        {
            DeselectObject(i);
        }
    }

    private bool ObjectSelected(int type)
    {
        return SelectedObjects[type] != null;
    }

    public float GetValueofObjectAtIndex(int index)
    {
        return GetValueOfObject(GetObjectOfIndex(index));
    }

    public GameObject GetObjectOfIndex(int index)
    {
        return gameObjects[index];
    }

    public float GetValueOfObject(GameObject go)
    {
        return go.transform.localScale.y;
    }

    public void SetValueOfObjectAtIndex(int idx, float value)
    {
        SetObjectToValue(value ,gameObjects[idx]);
    }

    public void SetObjectToValue(float value, GameObject go)
    {
        go.transform.localScale = new Vector3(go.transform.localScale.x, value, go.transform.localScale.z);
    }

    public int GetSize()
    {
        return NumBars;
    }
}
