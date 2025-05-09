using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractableText : MonoBehaviour
{
    // NOTES: I want to add the slow reveal to the text as it pops up, maybe I'll add a fade animation


    TextMeshPro interactTextContainer;
    // Bools to understand when player is near the text and when the text is showing
    public bool nearPlayer;
    public bool showingText;
    // String for text
    public string interactText;
    // Animator
    Animator anim;

    // To clamp the rotation
    float xRotation;
    float yRotation;

    // Player transform reference
    public Transform playerTransform;
    // Speed of Rotation
    public float rotationSpeed = 5f;
    // Rotation empty gameobject ref
    public Transform textRotation;


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
        interactTextContainer = transform.GetChild(0).GetComponent<TextMeshPro>();
        interactTextContainer.text = "";
        anim = GetComponentInChildren<Animator>();
    }


    // Shows container with text
    private void Show()
    {
        StartCoroutine(TypeText());
    }

    // Hides container with text
    private void Hide()
    {
        interactTextContainer.text = "";
        StopAllCoroutines();
    }

    public IEnumerator TypeText()
    {
        foreach (char x in interactText)
        {

            // Currently Animating at the start
            anim.Play("Text_Animation_Test");

            interactTextContainer.text += x;
            yield return new WaitForSeconds(0.05f);
        }
        yield break;
    }

    private void Update()
    {

        // Makes the text follow the players position in rotation
        textRotation.transform.rotation = Quaternion.LookRotation((textRotation.position - playerTransform.position).normalized);

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        // Clamps the rotation to prevent flipping
        xRotation = Mathf.Clamp (xRotation, 20, 40);
        // It currently flips when the player goes under it, may want to change this, 

    }
}
