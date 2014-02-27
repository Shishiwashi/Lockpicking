using UnityEngine;
using System.Collections;

public class LevelsManager : MonoBehaviour
{
	public WorldProperties[] worlds;
	//public GameObject worldPrefab;
	public GameObject worldPage;
	public int startingWorld;
	public UIScrollList levelsScroll;
	public UIScrollList worldsScroll;
	
	void Start ()
	{
		int itemsPerPage = worldPage.GetComponent<ItemsPage>().maximum;
		int numberOfPages = Mathf.CeilToInt((float)worlds.Length/(float)itemsPerPage);
		for(int i = 0; i<numberOfPages; i++)
		{
			worldsScroll.CreateItem(worldPage);
			ItemsPage page = worldsScroll.GetItem(worldsScroll.Count-1).gameObject.GetComponent<ItemsPage>();
			for(int j = 0; j<page.worlds.Length; j++)
			{
				if((i*itemsPerPage+j) > worlds.Length-1)
				{
					//Debug.Log("o item " + (i*itemsPerPage+j) + " foi desativado");
					page.worlds[j].gameObject.SetActive(false);
				}
				else
				{
					page.worlds[j].properties = worlds[i*itemsPerPage+j];
					page.worlds[j].manager = this;
				}
			}
		}

		/*foreach(WorldProperties p in worlds)
		{
			worldsScroll.CreateItem(worldPrefab);
			World newWorld = worldsScroll.GetItem(worldsScroll.Count-1).gameObject.GetComponent<World>();
			newWorld.properties = p;
			newWorld.manager = this;
		}*/
	}
	
	public void BringLevelPanel()
	{
		levelsScroll.transform.parent.GetComponent<UIPanel>().BringIn();
		worldsScroll.transform.parent.GetComponent<UIPanel>().Dismiss();
	}
	
	public void BringWorldPanel()
	{
		levelsScroll.transform.parent.GetComponent<UIPanel>().Dismiss();
		worldsScroll.transform.parent.GetComponent<UIPanel>().BringIn();
		levelsScroll.ClearList(true);
	}
}