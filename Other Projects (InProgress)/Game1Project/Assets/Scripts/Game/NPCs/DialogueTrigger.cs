using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public DialogueNode startingDialogue;

    public DialogueManager dialogueManager;

    private bool triggered;

	// Start is called before the first frame update
	void Start()
    {
        //dialogueManager.inDialogue = false;
        dialogueManager = GameObject.Find("DialogueManager");
        triggered = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
	{
        if (!triggered && dialogueManager != null && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //dialogueManager.inDialogue = true;
			//Debug.Log("Dialogue Triggered");
            dialogueManager.startDialogue(startingDialogue);
			triggered = true;

		}
    }


}
