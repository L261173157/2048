using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreUI : MonoBehaviour
{
    public static scoreUI instance;
    private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _textMeshPro.text = "test";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(int score)
    {
        _textMeshPro.text = score.ToString();
    }
}
