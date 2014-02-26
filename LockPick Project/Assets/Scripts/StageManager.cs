using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour
{
	private int totalStages = -1;
	private int totalKeys = -1;
	private int keyCounter = 0;
	private int stageCounter = 0;

	private GameObject tempStage;

	void Start ()
	{
		totalStages = Highlander.currentLevel.stages.Length;
		tempStage = GameObject.Instantiate(Highlander.currentLevel.stages[0].prefab, transform.position, transform.rotation) as GameObject;
		totalKeys = Highlander.currentLevel.stages[0].totalKeys;
	}

	public void AddKey()
	{
		keyCounter++;
		if(keyCounter>=totalKeys)
		{
			keyCounter = 0;
			stageCounter++;
			GameObject.Destroy(tempStage);
			if(stageCounter>=totalStages)
			{
				Debug.Log("cabou o level");
				stageCounter = 0;
			}
			else
			{
				Debug.Log("proximo stage");
				tempStage = GameObject.Instantiate(Highlander.currentLevel.stages[stageCounter].prefab, transform.position, transform.rotation) as GameObject;
				totalKeys = Highlander.currentLevel.stages[stageCounter].totalKeys;
			}
		}
		else
		{
			Debug.Log("proxima key");
		}
	}
}