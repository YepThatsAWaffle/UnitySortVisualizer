using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortAlgorithmFactory : MonoBehaviour
{
    public enum SortType
    {
        Insertion = 0,
        Quick = 1,
        Selection = 2
    }
    public SortAlgorithm GetSortAlgorithm(int type)
    {
        SortAlgorithm sortAlgorithm = null;
        SortType sortType = (SortType)type;
        switch (sortType)
        {
            case SortType.Insertion:
                sortAlgorithm = gameObject.AddComponent<InsertSortAlgorithm>();
                break;
            case SortType.Quick:
                sortAlgorithm = gameObject.AddComponent<QuickSortAlgorithm>();
                break;
            case SortType.Selection:
                sortAlgorithm = gameObject.AddComponent<SelectionSortAlgorithm>();
                break;
            default:
                break;
        }
        return sortAlgorithm;
    }
}
