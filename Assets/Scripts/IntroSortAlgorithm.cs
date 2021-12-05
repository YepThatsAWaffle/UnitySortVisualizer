using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSortAlgorithm : SortAlgorithm
{
    int pivotIndex = 0;
    public override IEnumerator DoSort()
    {
        int depth = (int)(2 * Mathf.Floor(Mathf.Log(GetSize()) / Mathf.Log(2)));
        yield return StartCoroutine(IntroSort(0, GetSize() - 1, depth));
    }

    public IEnumerator IntroSort(int l, int r, int depth)
    {
        if(r - l > 16)
        {
            if(depth == 0)
            {
                yield return StartCoroutine(HeapSort(l, r));
                yield break;
            }

            depth--;
            int pivot = findPivot(l, l+(r-l)/2 + 1, r);
            Swap(pivot, r);
            yield return new WaitForSeconds(delay);
            yield return StartCoroutine(partition(l, r));

            yield return StartCoroutine(IntroSort(l, pivotIndex - 1, depth));
            yield return StartCoroutine(IntroSort(pivotIndex + 1, r, depth));
        }
        else
        {
            yield return StartCoroutine(InsertSort(l, r));
        }
    }

    public IEnumerator InsertSort(int l, int r)
    {
        for (int i = l; i <= r; i++)
        {
            int j = i - 1;
            Select(i);
            float temp = GetValue(i);
            while (j >= l && Compare(temp, GetValue(j)))
            {
                SelectCompare(j);
                SetValue(j + 1, GetValue(j));
                j--;
                yield return new WaitForSeconds(delay);
            }
            SetValue(j + 1, temp);
            yield return new WaitForSeconds(delay);
        }
    }

    public IEnumerator partition(int l, int r)
    {
        SelectPivot(r);
        float pivot = GetValue(r);
        int i = (l - 1);

        for (int j = l; j <= r; j++)
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

    public IEnumerator HeapSort(int l, int r)
    {
        for (int i = r / 2 - 1; i >= l; i--)
        {
            yield return StartCoroutine(heapify(r, i));
        }

        for (int i = r - 1; i > l; i--)
        {
            Swap(l, i);
            yield return StartCoroutine(heapify(i, l));
        }
    }
    private int findPivot(int a,
                      int b, int c)
    {
        var v1 = GetValue(a);
        var v2 = GetValue(b);
        var v3 = GetValue(c);

        float max = Mathf.Max(
                  Mathf.Max(v1,
                           v2), v3);
        float min = Mathf.Min(
                  Mathf.Min(v1,
                           v2), v3);
        if (max != v1 && min != v1)
            return a;
        if (max != v2 && min != v2) 
            return b;
        return c;
    }

    public IEnumerator heapify(int n, int i)
    {
        yield return new WaitForSeconds(delay);
        Select(i);
        int largest = i;
        int l = 2 * i + 1;
        int r = 2 * i + 2;

        if (l < n)
        {
            SelectCompare(l);
            if (Compare(largest, l))
            {
                largest = l;
            }
            yield return new WaitForSeconds(delay);
        }

        if (r < n)
        {
            SelectCompare(r);
            if (Compare(largest, r))
            {
                largest = r;
            }
            yield return new WaitForSeconds(delay);
        }

        if (largest != i)
        {
            Swap(largest, i);

            yield return StartCoroutine(heapify(n, largest));
        }
    }

}
