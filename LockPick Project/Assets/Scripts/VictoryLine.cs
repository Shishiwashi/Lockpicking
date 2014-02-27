using UnityEngine;
using System.Collections;

public class VictoryLine : MonoBehaviour
{
	private StageManager manager;
	public string key = "Name";

	void Start ()
	{
		manager = GameObject.Find("StageManager").GetComponent<StageManager>();
	}
	
	void OnTriggerEnter2D (Collider2D hit)
	{
		Debug.Log(hit.transform.name);
		if(hit.transform.name == key)
		{
			GameObject.Destroy(hit.transform.GetChild(0).collider2D);
			GameObject.Destroy(hit.transform.GetChild(0).GetComponent<DragRigidbody2D>());
			GameObject.Destroy(hit.rigidbody2D);
			GameObject.Destroy(hit);
			GameObject.Destroy(collider2D);
			GameObject.Destroy(rigidbody2D);
			manager.AddKey();
			GameObject.Destroy(this);
		}
	}
}