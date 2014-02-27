using UnityEngine;
using System.Collections;

public class World : MonoBehaviour
{
	public WorldProperties properties;
	public LevelsManager manager;
	//public GameObject levelPrefab;
	public GameObject levelPage;

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

		int itemsPerPage = levelPage.GetComponent<ItemsPage>().maximum;
		int numberOfPages = Mathf.CeilToInt((float)properties.levels.Length/(float)itemsPerPage);
		for(int i = 0; i<numberOfPages; i++)
		{
			manager.levelsScroll.CreateItem(levelPage);
			ItemsPage page = manager.levelsScroll.GetItem(manager.levelsScroll.Count-1).gameObject.GetComponent<ItemsPage>();
			for(int j = 0; j<page.levels.Length; j++)
			{
				if((i*itemsPerPage+j) > properties.levels.Length-1)
				{
					//Debug.Log("o item " + (i*itemsPerPage+j) + " foi desativado");
					page.levels[j].gameObject.SetActive(false);
				}
				else
				{
					page.levels[j].properties = properties.levels[i*itemsPerPage+j];
					page.levels[j].manager = manager;
				}
			}
		}
		
		/*foreach(LevelProperties p in properties.levels)
		{
			manager.levelsScroll.CreateItem(levelPrefab);
			Level newLevel = manager.levelsScroll.GetItem(manager.levelsScroll.Count-1).gameObject.GetComponent<Level>();
			newLevel.properties = p;
			newLevel.manager = manager;
		}*/
		manager.BringLevelPanel();
	}
}

[System.Serializable] public class WorldProperties
{
	public int index = -1;
	public bool enabled = false;
	public LevelProperties[] levels;
}