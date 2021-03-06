using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistinctRandomList
{
    public List<int> distinctList;
    System.Random rand;

    public int this[int key]
    {
        get => distinctList[key];
    }

    public DistinctRandomList(int size, System.Random _rand)
    {
        rand = _rand;
        distinctList = new List<int>();
        GenerateList(size);
    }

    private void GenerateList(int size)
    {
        distinctList.Clear();
        var distinctSortedList = new List<int>();
        for (int i = 0; i < size; i++)
        {
            distinctSortedList.Add(i + 1);
        }

        for (int j = 0; j < size; j++)
        {
            int randomIndex = rand.Next(distinctSortedList.Count);
            var randomValue = distinctSortedList[randomIndex];
            distinctSortedList.RemoveAt(randomIndex);
            distinctList.Add(randomValue);
        }
    }
}
