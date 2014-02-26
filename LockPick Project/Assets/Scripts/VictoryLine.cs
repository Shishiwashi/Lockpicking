using UnityEngine;
using System.Collections;

public class VictoryLine : MonoBehaviour
{
	private StageManager manager;
	public string key = "Name";
	public MonoBehaviour toDestroy;

	void Start ()
	{
		manager = GameObject.Find("StageManager").GetComponent<StageManager>();
	}
	
	void OnTriggerEnter2D (Collider2D hit)
	{
		Debug.Log(hit.transform.name);
		if(hit.transform.name == key)
		{
			manager.AddKey();
			GameObject.Destroy(toDestroy);
			GameObject.Destroy(this);
		}
	}
}