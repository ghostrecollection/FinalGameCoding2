using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractableText : MonoBehaviour
{
    // NOTES: I want to add the slow reveal to the text as it pops up, maybe I'll add a fade animation


    public GameObject interactTextContainer;
    // Bools to understand when player is near the text and when the text is showing
    public bool nearPlayer;
    public bool showingText;
    // Animator
    Animator anim;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nearPlayer = true;
            Show();
            showingText = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nearPlayer = false;
            Hide();
            showingText = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Shows container with text
    private void Show()
    {
        interactTextContainer.SetActive(true);
        anim.Play("Text_Animation_Test");
    }

    // Hides container with text
    private void Hide()
    {
        interactTextContainer.SetActive(false);
    }



}
