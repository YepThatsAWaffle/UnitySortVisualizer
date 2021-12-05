using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimSortAlgorithm : SortAlgorithm
{
    const int RUN = 32;
    public override IEnumerator DoSort()
    {
        yield return StartCoroutine(sort(GetSize()));
    }

    public IEnumerator sort(int n)
    {
        for(int i = 0; i < n; i+=RUN)
        {
            yield return StartCoroutine(InsertSort(i, Mathf.Min(i + RUN - 1, n - 1)));
        }

        for(int size = RUN; size < n; size = 2*size)
        {
            for (int l = 0; l < n; l += 2*size)
            {
                int m = l + size - 1;
                int r = Mathf.Min(l + 2 * size - 1, n - 1);

                yield return StartCoroutine(merge(l, m, r));
            }
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

    public IEnumerator merge(int l, int m, int r)
    {
        int n1 = m - l + 1;
        int n2 = r - m;

        if(n2 <= 0)
        {
            yield break;
        }

        float[] L = new float[n1];
        float[] R = new float[n2];

        for (int a = 0; a < n1; a++)
        {
            L[a] = GetValue(l + a);
        }
        for (int b = 0; b < n2; b++)
        {
            R[b] = GetValue(m + 1 + b);
        }

        int i = 0, j = 0, k = l;

        while (i < n1 && j < n2)
        {
            Select(k);
            if (Compare(L[i], R[j]))
            {
                SetValue(k, L[i]);
                i++;
            }
            else
            {
                SetValue(k, R[j]);
                j++;
            }
            k++;
            yield return new WaitForSeconds(delay);
        }

        while (i < n1)
        {
            Select(k);
            SetValue(k, L[i]);
            k++;
            i++;
            yield return new WaitForSeconds(delay);
        }

        while (j < n2)
        {
            Select(k);
            SetValue(k, R[j]);
            k++;
            j++;
            yield return new WaitForSeconds(delay);
        }
    }
}
