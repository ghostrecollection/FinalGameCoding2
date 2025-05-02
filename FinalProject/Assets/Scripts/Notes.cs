using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    public bool isVisible = true;
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        
        isVisible = !isVisible;
        objectRenderer.enabled = isVisible;
        
    }
}
