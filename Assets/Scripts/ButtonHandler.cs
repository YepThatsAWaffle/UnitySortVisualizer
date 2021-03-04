using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject prefab;

    private ObjectListController controller;

    

    // Start is called before the first frame update
    void Start()
    {
        controller = new ObjectListController(prefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSetup()
    {
        controller.CreateUnsortedList(prefab);
    }

    public void OnRepaint(Material mat)
    {
        controller.RepaintAll();
    }

    public void OnDestroyObjects()
    {
        controller.DestroyObjects();
    }
}
