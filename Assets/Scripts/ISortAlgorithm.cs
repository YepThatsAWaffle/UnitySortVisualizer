using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISortAlgorithm
{
    IEnumerator DoSort();
    void Setup(ObjectListController _controller, float _delay, SortStats _stats);
}
