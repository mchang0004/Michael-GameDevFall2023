using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
public class Item : ScriptableObject
{


	public string itemName;
	public int itemID;
	public Sprite icon;

	[TextArea(3, 10)]
	public string description;


}




