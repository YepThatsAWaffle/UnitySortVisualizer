using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortAlgorithmFactory : MonoBehaviour
{
    public SortAlgorithm GetSortAlgorithm()
    {
        SortAlgorithm sortAlgorithm = gameObject.AddComponent<InsertSortAlgorithm>();
        return sortAlgorithm;
    }
}
