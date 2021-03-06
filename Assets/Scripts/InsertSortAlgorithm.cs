using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertSortAlgorithm : SortAlgorithm
{
    public override IEnumerator DoSort()
    {
        for (int i = 1; i < GetSize(); i++)
        {
            yield return StartCoroutine(SortStep(i));
        }
    }

    public IEnumerator SortStep(int step)
    {
        int j = step - 1;
        while (j >= 0 && Compare(j+1, j))
        {
            Select(j+1);
            Swap(j, j + 1);
            j--;
            yield return new WaitForSeconds(delay);
        }
    }
}
