using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
	public LevelProperties properties;
	public LevelsManager manager;
	public GameObject[] stars;
	
	void Start ()
	{
		GetScore();
	}
	
	void GetScore()
	{
		transform.FindChild("Name").GetComponent<SpriteText>().Text = "Level " + properties.index.ToString();
		
		if(PlayerPrefs.HasKey("level"+properties.index+"enabled")) properties.enabled = true;
		else properties.enabled = false;
		
		if(PlayerPrefs.HasKey("level"+properties.index+"stars")) properties.stars = PlayerPrefs.GetInt("level"+properties.index+"stars");
		for(int i = 0; i<properties.stars; i++) stars[i].SetActive(true);
		
		if(PlayerPrefs.HasKey("level"+properties.index+"time"))
		{
			properties.time = PlayerPrefs.GetInt("level"+properties.index+"time");
			transform.FindChild("Time").GetComponent<SpriteText>().Text = "Time: " + properties.time.ToString();
		}
		else
		{
			transform.FindChild("Time").gameObject.SetActive(false);
		}
		
		if(properties.enabled || properties.index == 0) transform.FindChild("Disabled").gameObject.SetActive(false);
	}
	
	void EnterLevel()
	{
		Debug.Log("enter level");
		Highlander.currentLevel = properties;
		Application.LoadLevel("Game");
	}
}

[System.Serializable] public class LevelProperties
{
	[System.NonSerialized] public World world;
	public int index = -1;
	public int stars = 0;
	public int time = -1;
	public bool enabled = false;
	public StageProperties[] stages;
}