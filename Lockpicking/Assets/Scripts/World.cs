using UnityEngine;
using System.Collections;

public class World : MonoBehaviour
{
	public WorldProperties properties;
	public LevelsManager manager;
	public GameObject levelPrefab;
	
	void Start ()
	{
		transform.FindChild("Name").GetComponent<SpriteText>().Text = "World " + properties.index.ToString();
		
		if(PlayerPrefs.HasKey("world"+properties.index+"enabled") || properties.index == 0) properties.enabled = true;
		else properties.enabled = false;
		
		if(properties.enabled) transform.FindChild("Disabled").gameObject.SetActive(false);
	}
	
	void EnterWorld()
	{
		if(!properties.enabled) return;
		
		foreach(LevelProperties p in properties.levels)
		{
			manager.levelsScroll.CreateItem(levelPrefab);
			Level newLevel = manager.levelsScroll.GetItem(manager.levelsScroll.Count-1).gameObject.GetComponent<Level>();
			newLevel.properties = p;
			newLevel.manager = manager;
		}
		manager.BringLevelPanel();
	}
}

[System.Serializable] public class WorldProperties
{
	public int index = -1;
	public bool enabled = false;
	public LevelProperties[] levels;
}