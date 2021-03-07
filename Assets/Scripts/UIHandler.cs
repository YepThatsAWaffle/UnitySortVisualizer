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

    private int sortType;

    float delay = .001f;

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
        StartCoroutine(sortHandler.DoSort(controller, sortType, delay, stats));
    }

    public void OnSetup()
    {
        stats.Init();
        sortHandler.AbortSort();
        controller.CreateUnsortedList(Prefab);
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
        sortHandler.AbortSort();
        sortType = val;
        OnSetup();
    }

    public void OnDelayUpdate(float val)
    {
        delay = val/1000f;
        stats.Delay = val;
        sortHandler.UpdateDelay(delay);
    }
}
