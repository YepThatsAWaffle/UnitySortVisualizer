using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSortAlgorithm : SortAlgorithm
{
    public override IEnumerator DoSort()
    {
        for (int i = 0; i < GetSize(); i++)
        {
            yield return StartCoroutine(SortStep(i));
        }
    }

    public IEnumerator SortStep(int step)
    {
        int min_idx = step;
        Select(min_idx);

        for (int i = step; i < GetSize(); i++)
        {
            SelectCompare(i);
            Select(min_idx);
            if (Compare(i, min_idx))
            {
                min_idx = i;
            }
            yield return new WaitForSeconds(delay);
        }

        Swap(min_idx, step);
    }
}
