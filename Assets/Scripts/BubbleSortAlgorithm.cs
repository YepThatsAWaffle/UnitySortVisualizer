using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSortAlgorithm : SortAlgorithm
{
    public override IEnumerator DoSort()
    {
        for (int i = 1; i < GetSize(); i++)
        {
            yield return StartCoroutine(SortStep(i));
        }
    }

    public IEnumerator SortStep(int i)
    {
        for(int j = 0; j < GetSize() -i - 1; j++)
        {
            Select(j);
            SelectCompare(j + 1);
            if(Compare(j + 1, j))
            {
                Swap(j, j + 1);
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
