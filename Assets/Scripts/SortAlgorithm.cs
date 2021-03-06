using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SortAlgorithm : MonoBehaviour, ISortAlgorithm
{
    private ObjectListController controller;
    public float delay;
    SortStats stats;

    private void Start()
    {
    }

    public void Setup(ObjectListController _controller, float _delay, SortStats _stats)
    {
        delay = _delay;
        controller = _controller;
        stats = _stats;
        stats.Size = controller.GetSize();
    }

    public float GetValue(int index) 
    {
        stats.Accesses++;
        return controller.GetValueofObjectAtIndex(index);
    }

    public bool Compare(int a, int b)
    {
        stats.Compares++;
        return CompareValue(GetValue(a), GetValue(b));
    }

    public bool Compare(float a, int b)
    {
        stats.Compares++;
        return CompareValue(a, GetValue(b));
    }

    public bool Compare(int a, float b)
    {
        stats.Compares++;
        return CompareValue(GetValue(a), b);
    }

    public bool CompareValue(float floatA, float floatB)
    {
        return floatA < floatB;
    }

    public void Swap(int a, int b)
    {
        stats.Swaps++;
        controller.Swap(a, b);
    }

    public void Select(int index)
    {
        controller.SelectObject(index);
    }

    public void SelectPivot(int index)
    {
        controller.SelectPivotObject(index);
    }

    public void SelectCompare(int index)
    {
        controller.SelectCompareObject(index);
    }

    public void UpdateDelay(float _delay)
    {
        delay = _delay;
    }

    public int GetSize()
    {
        return stats.Size;
    }

    public abstract IEnumerator DoSort();
}
