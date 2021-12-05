using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapSortAlgorithm : SortAlgorithm
{
    public override IEnumerator DoSort()
    {
        int n = GetSize();

        for (int i = n / 2 - 1; i >= 0; i--)
        {
            yield return StartCoroutine(heapify(n, i));
        }

        for(int i = n - 1; i > 0; i--)
        {
            Swap(0, i);
            yield return StartCoroutine(heapify(i, 0));
        }
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

        if(largest != i)
        {
            Swap(largest, i);

            yield return StartCoroutine(heapify(n, largest));
        }
    }
}
