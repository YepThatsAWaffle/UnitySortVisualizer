using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject Prefab;

    private ObjectListController controller;

    

    // Start is called before the first frame update
    void Start()
    {
        controller = ScriptableObject.CreateInstance<ObjectListController>();
        OnSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSort()
    {
        var sortHandler = gameObject.AddComponent<SortHandler>();
        StartCoroutine(sortHandler.DoSort(controller));
        //Destroy(sortHandler);
    }

    public void OnSetup()
    {
        controller.CreateUnsortedList(Prefab);
    }

    public void UpdateNumBars(float value)
    {
        controller.UpdateNumBars((int)value);
        OnSetup();
    }

    public void OnSwapClick()
    {
        controller.SwapRandom();
    }
}
