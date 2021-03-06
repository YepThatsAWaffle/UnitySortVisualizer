using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSortAlgorithm : SortAlgorithm
{
    private int pivotIndex;

    private void Start()
    {
        pivotIndex = 0;
    }
    public override IEnumerator DoSort()
    {
        yield return StartCoroutine(QuickSort(0, GetSize()-1));
    }

    public IEnumerator QuickSort(int l, int r)
    {
        if(l<r)
        {
            yield return StartCoroutine(partition(l, r));
            yield return QuickSort(l, pivotIndex - 1);
            yield return QuickSort(pivotIndex + 1, r);
        }
    }

    public IEnumerator partition(int l, int r)
    {
        SelectPivot(r);
        float pivot = GetValue(r);
        int i = (l - 1);

        for(int j = l; j <= r; j++)
        {
            Select(j);
            if (Compare(j, pivot))
            {
                i++;
                SelectCompare(i);
                Swap(j, i);
            }
            yield return new WaitForSeconds(delay);
        }
        Swap(i + 1, r);
        yield return new WaitForSeconds(delay);
        pivotIndex = i + 1;
    }
}
