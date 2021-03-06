using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SortAlgorithm : MonoBehaviour, ISortAlgorithm
{
    private ObjectListController controller;
    public SortStats stats;

    private void Start()
    {
    }

    public void Setup(ObjectListController _controller)
    {
        controller = _controller;
        stats = new SortStats();
        stats.size = controller.GetSize();
    }

    public float GetValue(int index) 
    {
        stats.arrayAccesses++;
        return controller.GetValueofObjectAtIndex(index);
    }

    public bool Compare(int a, int b)
    {
        stats.compares++;
        return CompareValue(GetValue(a), GetValue(b));
    }

    public bool Compare(float a, int b)
    {
        stats.compares++;
        return CompareValue(a, GetValue(b));
    }

    public bool Compare(int a, float b)
    {
        stats.compares++;
        return CompareValue(GetValue(a), b);
    }

    public bool CompareValue(float floatA, float floatB)
    {
        return floatA < floatB;
    }

    public void Swap(int a, int b)
    {
        Debug.Log($"Swapping {a} with {b}");
        stats.swaps++;
        controller.Swap(a, b);
    }

    public void Select(int index)
    {
        controller.SelectObject(index);
    }

    public void Deselect()
    {
        controller.DeselectObject();
    }

    public abstract IEnumerator DoSort();
}
