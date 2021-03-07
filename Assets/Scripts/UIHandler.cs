using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Text statsAccesses;
    public Text statsSwaps;
    public Text statsCompares;
    public Text statsSize;
    public Text statsDelay;
    public SortStats stats;

    public GameObject Prefab;

    private SortHandler sortHandler;

    private ObjectListController controller;

    int sortType = 0;
    float delay = .001f;
    int direction = 0;
    int listState = 0;

    // Start is called before the first frame update
    void Start()
    {
        sortType = 0;
        controller = ScriptableObject.CreateInstance<ObjectListController>();
        sortHandler = gameObject.AddComponent<SortHandler>();
        stats = new SortStats(statsSize, statsAccesses, statsCompares, statsSwaps, statsDelay, controller.GetSize(), delay);
        OnSetup();
        OnDelayUpdate(1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnSort()
    {
        StartCoroutine(sortHandler.DoSort(controller, sortType, delay, stats, direction));
    }

    public void OnSetup()
    {
        sortHandler.AbortSort();
        stats.Init();
        controller.CreateUnsortedList(Prefab, listState, direction);
    }

    public void UpdateNumBars(float value)
    {
        sortHandler.AbortSort();
        controller.UpdateNumBars((int)value);
        stats.Size = (int)value;
        OnSetup();
    }

    public void OnSortTypeChange(int val)
    {
        sortType = val;
    }

    public void OnDelayUpdate(float val)
    {
        delay = val/1000f;
        stats.Delay = val;
        sortHandler.UpdateDelay(delay);
    }

    public void OnDirectionChange(int val)
    {
        direction = val;
    }

    public void OnListStateChange(int val)
    {
        listState = val;
        OnSetup();
    }
}
