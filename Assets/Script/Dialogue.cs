using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    //Fields
    public GameObject window;           //Window
 
    public GameObject indicator;       //Indicator

    public TMP_Text dialogueText;   //Text component

    public List<string> dialogues;        //Dialogues List

    public float writingSpeed;           //Writing speed
    
    private int index;                         //Index on dialogue

    private int charIndex;                 //Character index

    private bool started;                   //Started boolean

    private bool waitForNext;           //Wait for next boolean

    public AudioSource typingSound; // AudioSource component for playing effect sounds

    public AudioClip typingSoundClip; // AudioClip for storing effect sounds

    public float typingSoundSpeed = 0.06f;  //set default speed as 1.0f

    private void Awake()
    {
        ToggleIndicator(false);
        ToggleWindow(false);
    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    //Start Dialogue
    public void StartDialogue()
    {
        if (started)
        {
            return;
        }
        //Boolean to indicate that we have started
        started = true;
        //Show the window
        ToggleWindow(true);
        //hide the indicator
        ToggleIndicator(false);
        //Start with first dialogue
        GetDialogue(0);
    }

    private void GetDialogue(int i)
    {
        //start index at zero
        index = i;
        //Reset the character index
        charIndex = 0;
        //clear the dialogue component text
        dialogueText.text = string.Empty;
        //start writing 
        StartCoroutine(Writing());
    }

    //End Dialogue
    public void EndDialogue()
    {
        //Started is disabled
        started = false;
        //Disable wait for next as well
        waitForNext = false;
        //Stop all Ienumarators
        StopAllCoroutines();
        //Hide the window
        ToggleWindow(false);
    }


    //Writing logic
    IEnumerator Writing()
    {
        string currentDialogue = dialogues[index];
        //Write the character
        dialogueText.text += currentDialogue[charIndex];
        //Setting the playback speed of the effect sound
        typingSound.pitch = typingSoundSpeed;
        // Playing sound effect
        typingSound.PlayOneShot(typingSoundClip); 
        //increase the character index
        charIndex++;
        //Make sure you have reached the end of the sentence
        if(charIndex < currentDialogue.Length)
        {
            //wait x seconds
            yield return new WaitForSeconds(writingSpeed);
            //restart the same process
            StartCoroutine(Writing());
        }
        else
        {
            //End this sentence and wait for the next one
            waitForNext = true;
        }
       
    }

    private void Update()
    {
        if (!started)
        {
            return;
        }

        if(waitForNext && Input.GetKeyDown(KeyCode.I))
        {
            waitForNext = false;
            index++;
            if(index < dialogues.Count)
            {
                GetDialogue(index);
            }
            else
            {
                //End dialogue
                EndDialogue();
            }
          
        }
    }

}
