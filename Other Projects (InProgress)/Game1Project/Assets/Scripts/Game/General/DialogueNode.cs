using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Node", menuName = "Scriptable object/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
	[TextArea(4, 10)]
	public string text; // Text displayed
	public string buttonText; //text that will appear on the button
	public bool endAfter;	//should the dialogue end after this?

	public DialogueNode option1;
	public DialogueNode option2;
	public DialogueNode option3;
	public DialogueNode option4;

	public Quest quest;
}