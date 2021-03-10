using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortAlgorithmFactory : MonoBehaviour
{
    public enum SortType
    {
        Insertion = 0,
        Selection = 1,
        Bubble = 2,
        Quick = 3,
        Merge = 4,
    }
    public SortAlgorithm GetSortAlgorithm(int type)
    {
        SortAlgorithm sortAlgorithm = null;
        SortType sortType = (SortType)type;
        switch (sortType)
        {
            case SortType.Bubble:
                sortAlgorithm = gameObject.AddComponent<BubbleSortAlgorithm>();
                break;
            case SortType.Insertion:
                sortAlgorithm = gameObject.AddComponent<InsertSortAlgorithm>();
                break;
            case SortType.Quick:
                sortAlgorithm = gameObject.AddComponent<QuickSortAlgorithm>();
                break;
            case SortType.Selection:
                sortAlgorithm = gameObject.AddComponent<SelectionSortAlgorithm>();
                break;
            case SortType.Merge:
                sortAlgorithm = gameObject.AddComponent<MergeSortAlgorithm>();
                break;
            default:
                break;
        }
        return sortAlgorithm;
    }
}
