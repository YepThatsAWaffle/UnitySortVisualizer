using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortHandler : MonoBehaviour
{
    public IEnumerator DoSort(ObjectListController _controller)
    {
        var sortFactory = gameObject.AddComponent<SortAlgorithmFactory>();
        var sortAlgorithm = sortFactory.GetSortAlgorithm();
        sortAlgorithm.Setup(_controller);
        yield return StartCoroutine(sortAlgorithm.DoSort());
        Debug.Log("Sorting Finished! Destroying sort components");
        Destroy(sortFactory);
        Destroy(sortAlgorithm);
    }
}
