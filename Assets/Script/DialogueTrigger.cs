using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    
    public Dialogue dialogueScript;
    private bool playerDetected;
    //Detect trigger with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If we triggered the player enable playerdetected and show indicator
        if(collision.tag == "Player")
        {
            playerDetected = true;
            dialogueScript.ToggleIndicator(playerDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //If we lost trigger with the player disable playerDetected and hide indicator
        if (collision.tag == "Player")
        {
            playerDetected = false;
            dialogueScript.ToggleIndicator(playerDetected);
            dialogueScript.EndDialogue();

            //End play sound effect
            dialogueScript.typingSound.Stop();
        }
    }

    //While detected if we interact start the dialogue
    private void Update()
    {
        if(playerDetected && Input.GetKeyDown(KeyCode.I))
        {
            dialogueScript.StartDialogue();
        }
    }
}
