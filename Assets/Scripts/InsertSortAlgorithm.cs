using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertSortAlgorithm : SortAlgorithm
{
    public override IEnumerator DoSort()
    {
        for (int i = 1; i < stats.size; i++)
        {
            Debug.Log($"SwapStep {i}");
            yield return StartCoroutine(SortStep(i));
        }
    }

    public IEnumerator SortStep(int step)
    {
        //Debug.Log($"InsideSwapStep {step}");
        int j = step - 1;
        while (j >= 0 && Compare(j+1, j))
        {
            Select(j+1);
            Swap(j, j + 1);
            j--;
            yield return new WaitForSeconds(.001f);
            Deselect();
        }
    }
}
