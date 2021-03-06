using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SortAlgorithmFactory;

public class SortHandler : MonoBehaviour
{
    SortAlgorithmFactory sortFactory;
    SortAlgorithm sortAlgorithm;
    enum SortState
    {
        Idle,
        Sorting
    }
    private SortState state;

    private void Start()
    {
        state = SortState.Idle;
        sortFactory = gameObject.AddComponent<SortAlgorithmFactory>();
    }

    public IEnumerator DoSort(ObjectListController _controller, int type, float delay, SortStats stats)
    {
        sortAlgorithm = sortFactory.GetSortAlgorithm(type);
        sortAlgorithm.Setup(_controller, delay, stats);
        state = SortState.Sorting;
        yield return StartCoroutine(sortAlgorithm.DoSort());

        if (state == SortState.Sorting)
        { 
            Destroy(sortAlgorithm);
        }
        state = SortState.Idle;
    }

    public void UpdateDelay(float delay)
    {
        if(state == SortState.Sorting)
        {
            sortAlgorithm.UpdateDelay(delay);
        }
    }

    public void AbortSort()
    {
        if(state == SortState.Sorting)
        {
            Destroy(sortAlgorithm);
        }

        state = SortState.Idle;
    }
}
