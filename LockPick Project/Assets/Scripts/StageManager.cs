using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour
{
	private int totalStages = -1;
	private int totalKeys = -1;
	private int keyCounter = 0;
	private int stageCounter = 0;
	private float currentTime = -1;

	private GameObject tempStage;
	public SpriteText timeLabel;
	public GameObject transitionPanel;
	private GameObject previousPanel;
	private GameObject currentPanel;

	void Start ()
	{
		totalStages = Highlander.currentLevel.stages.Length;
		currentPanel = GameObject.Instantiate(transitionPanel, transitionPanel.transform.position, transitionPanel.transform.rotation) as GameObject;
		previousPanel = currentPanel;
		tempStage = GameObject.Instantiate(Highlander.currentLevel.stages[0].prefab, currentPanel.transform.position, currentPanel.transform.rotation) as GameObject;
		tempStage.transform.parent = currentPanel.transform;
		currentPanel.GetComponent<UIInteractivePanel>().BringIn();

		totalKeys = Highlander.currentLevel.stages[0].totalKeys;
		currentTime = Highlander.currentLevel.stages[0].time;
	}

	void Update()
	{
		currentTime -= Time.deltaTime;
		timeLabel.Text = Mathf.FloorToInt(currentTime).ToString();
	}

	public void AddKey()
	{
		keyCounter++;
		if(keyCounter>=totalKeys)
		{
			keyCounter = 0;
			stageCounter++;
			if(stageCounter>=totalStages)
			{
				GameObject.Destroy(tempStage);
				Debug.Log("cabou o level");
				stageCounter = 0;
			}
			else
			{
				Debug.Log("proximo stage");
				currentPanel = GameObject.Instantiate(transitionPanel, transitionPanel.transform.position, transitionPanel.transform.rotation) as GameObject;
				tempStage = GameObject.Instantiate(Highlander.currentLevel.stages[stageCounter].prefab, currentPanel.transform.position, currentPanel.transform.rotation) as GameObject;
				tempStage.transform.parent = currentPanel.transform;
				previousPanel.GetComponent<UIInteractivePanel>().Dismiss();
				currentPanel.GetComponent<UIInteractivePanel>().BringIn();
				Invoke("ResetPanels", 1);

				totalKeys = Highlander.currentLevel.stages[stageCounter].totalKeys;
				currentTime = Highlander.currentLevel.stages[stageCounter].time;
			}
		}
		else
		{
			Debug.Log("proxima key");
		}
	}

	void ResetPanels()
	{
		GameObject.Destroy(previousPanel);
		previousPanel = currentPanel;
	}
}