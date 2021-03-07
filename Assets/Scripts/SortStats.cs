using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortStats
{
    public Text sizeText;
    public Text accessesText;
    public Text comparesText;
    public Text swapsText;
    public Text delayText;
    private int size { get; set; }
    private int compares { get; set; }
    private int accesses { get; set; }
    private int swaps { get; set; }
    private float delay { get; set; }

    public int Size { get => size; 
    set
        {
            size = value;
            sizeText.text = size.ToString("N0");
        }
    }
    public int Compares
    {
        get => compares;
        set
        {
            compares = value;
            comparesText.text = compares.ToString();
        }
    }
    public int Accesses
    {
        get => accesses;
        set
        {
            accesses = value;
            accessesText.text = accesses.ToString();
        }
    }
    public int Swaps
    {
        get => swaps;
        set
        {
            swaps = value;
            swapsText.text = swaps.ToString();
        }
    }

    public float Delay
    {
        get => delay;
        set
        {
            delay = value;
            delayText.text = delay.ToString() + "ms";
        }
    }

    public SortStats(Text _sizeText, Text _accessesText, Text _comparesText, Text _swapsText, Text _delayText, int size, float delay)
    {
        sizeText = _sizeText;
        accessesText = _accessesText;
        comparesText = _comparesText;
        swapsText = _swapsText;
        delayText = _delayText;

        Size = size;
        Delay = delay;

        Init();
    }

    public void Init()
    {
        accesses = 0;
        swaps = 0;
        compares = 0;

        accessesText.text = string.Empty;
        comparesText.text = string.Empty;
        swapsText.text = string.Empty;
    }

}
