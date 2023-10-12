using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Item", menuName = "Scriptable object/Quest Item")]
public class QuestItem : ScriptableObject
{
	public bool collected;
	public bool submitted;

	public int questItemID;

	public Sprite questItemImage;
	public string questItemName;


}
