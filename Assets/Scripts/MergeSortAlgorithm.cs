using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSortAlgorithm : SortAlgorithm
{
    public override IEnumerator DoSort()
    {
        yield return StartCoroutine(sort(0, GetSize() - 1));
    }

    public IEnumerator sort(int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;
            yield return StartCoroutine(sort(l, m));
            yield return StartCoroutine(sort(m + 1, r));
            yield return StartCoroutine(merge(l,m,r));
        }
    }

    public IEnumerator merge(int l, int m, int r)
    {
        int n1 = m - l + 1;
        int n2 = r - m;

        float[] L = new float[n1];
        float[] R = new float[n2];

        for(int a = 0; a < n1; a++)
        {
            L[a] = GetValue(l + a);
        }
        for(int b = 0; b < n2; b++)
        {
            R[b] = GetValue(m + 1 + b);
        }

        int i = 0, j = 0, k = l;

        while(i < n1 && j < n2)
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
