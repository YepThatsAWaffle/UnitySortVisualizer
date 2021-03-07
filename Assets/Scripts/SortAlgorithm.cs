using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SortAlgorithm : MonoBehaviour, ISortAlgorithm
{
    public enum SortDirection
    {
        ascending,
        descending
    }
    private ObjectListController controller;
    public float delay;
    SortStats stats;
    SortDirection direction;

    private void Start()
    {
    }

    public void Setup(ObjectListController _controller, float _delay, SortStats _stats, int _direction)
    {
        delay = _delay;
        controller = _controller;
        stats = _stats;
        stats.Size = controller.GetSize();
        direction = (SortDirection)_direction;
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
        if (direction == SortDirection.ascending)
        { 
            return floatA < floatB;
        }
        else
        {
            return floatB < floatA;
        }
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
