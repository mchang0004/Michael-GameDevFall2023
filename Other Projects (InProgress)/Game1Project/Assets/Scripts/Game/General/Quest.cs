using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable object/Quest")]
public class Quest : ScriptableObject
{
	public string questTitle;
	public string questText;
	public QuestItem questItem;

	public bool questActive;
	public bool questComplete;
	public bool questFailed;




}
