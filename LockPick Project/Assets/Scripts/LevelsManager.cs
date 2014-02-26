using UnityEngine;
using System.Collections;

public class LevelsManager : MonoBehaviour
{
	public WorldProperties[] worlds;
	public GameObject worldPrefab;
	public int startingWorld;
	public UIScrollList levelsScroll;
	public UIScrollList worldsScroll;
	
	void Start ()
	{
		foreach(WorldProperties p in worlds)
		{
			worldsScroll.CreateItem(worldPrefab);
			World newWorld = worldsScroll.GetItem(worldsScroll.Count-1).gameObject.GetComponent<World>();
			newWorld.properties = p;
			newWorld.manager = this;
		}
	}
	
	public void BringLevelPanel()
	{
		levelsScroll.transform.parent.GetComponent<UIInteractivePanel>().BringIn();
		worldsScroll.transform.parent.GetComponent<UIInteractivePanel>().Dismiss();
	}
	
	public void BringWorldPanel()
	{
		levelsScroll.transform.parent.GetComponent<UIInteractivePanel>().Dismiss();
		worldsScroll.transform.parent.GetComponent<UIInteractivePanel>().BringIn();
		levelsScroll.ClearList(true);
	}
}