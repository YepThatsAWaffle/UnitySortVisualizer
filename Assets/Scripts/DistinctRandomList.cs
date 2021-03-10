using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SortAlgorithm;
public class DistinctRandomList
{
    enum ListState
    {
        Random = 0,
        Sorted = 1,
        Reversed = 2,
        MostlySorted = 3,
        MostlyUnsorted = 4
    }

    public List<int> distinctList;
    System.Random rand;

    public int this[int key]
    {
        get => distinctList[key];
    }

    public DistinctRandomList(int size, System.Random _rand, int listState, int direction)
    { 
        rand = _rand;
        distinctList = new List<int>();
        Init(size, listState, direction);
    }

    void Init(int size, int listState, int direction)
    {
        int from;
        int to;

        if ((SortDirection)direction == SortDirection.ascending)
        {
            from = 0;
            to = size;
        }
        else
        {
            to = 0;
            from = size;
        }

        GenerateList(from, to, listState);
    }

    private void GenerateList(int from, int to, int listState)
    {
        distinctList.Clear();
        switch ((ListState)listState)
        {
            case ListState.Random:
                GenerateRandomList(from, to, 100);
                break;
            case ListState.MostlyUnsorted:
                GenerateRandomList(from, to, 80);
                break;
            case ListState.MostlySorted:
                GenerateRandomList(from, to, 55);
                break;
            case ListState.Sorted:
                GenerateRandomList(from, to, 0);
                break;
            case ListState.Reversed:
                GenerateRandomList(to, from, 0);
                break;
            default:
                return;
        }
    }

    private void GenerateRandomList(int from, int to, int Randomness)
    {
        int max;
        int min;
        if (from > to)
        {
            max = from;
            min = to;
        }
        else
        {
            min = from;
            max = to;
        }

        var distinctSortedList = new List<int>();
        for (int j = min; j < max; j++)
        {
            int valueToAdd;
            if(from < to)
            {
                valueToAdd = j + 1;
            }
            else
            {
                valueToAdd = max - j;
            }

            distinctSortedList.Add(valueToAdd);
        }

        for (int j = 0; j < max - min; j++)
        {
            if (rand.Next(100) < Randomness)
            { 
                int randomIndex = rand.Next(distinctSortedList.Count);
                var randomValue = distinctSortedList[randomIndex];
                distinctSortedList.RemoveAt(randomIndex);
                distinctList.Add(randomValue);
            }
            else
            {
                int sortedIndex = 0;
                var sortedValue = distinctSortedList[sortedIndex];
                distinctSortedList.RemoveAt(0);
                distinctList.Add(sortedValue);
            }
        }
    }
}
