using UnityEngine;
using System.Collections;

public class Password : MonoBehaviour
{
	public string password = "123";
	public float reloadDelay = 1;
	public float victoryDelay = 1;
	public SpriteText passwordLabel;
	public PasswordButton[] buttons;

	private string initialGuess;
	private bool blocked = false;
	private StageManager manager;
	private int index = 0;

	void Start ()
	{
		manager = GameObject.Find("StageManager").GetComponent<StageManager>();
		initialGuess = passwordLabel.text;
	}

	public void AddLetter(string letter)
	{
		if(blocked) return;

		passwordLabel.Text = passwordLabel.text.Insert(index, letter);
		passwordLabel.Text = passwordLabel.text.Remove(index+1, 1);
		index++;

		if(index >= password.Length)
		{
			if(passwordLabel.text==password)
			{
				for(int i = 0; i<buttons.Length; i++)
				{
					buttons[i].GetComponent<UIButton>().controlIsEnabled = false;
					GameObject.Destroy(buttons[i]);
				}
				Invoke("Clear", victoryDelay);
			}
			else
			{
				blocked = true;
				for(int i = 0; i<buttons.Length; i++) buttons[i].GetComponent<UIButton>().controlIsEnabled = false;
				Invoke("Reset", reloadDelay);
			}
		}
	}

	void Reset()
	{
		index = 0;
		passwordLabel.Text = initialGuess;
		blocked = false;
		for(int i = 0; i<buttons.Length; i++) buttons[i].GetComponent<UIButton>().controlIsEnabled = true;
	}

	void Clear()
	{		
		manager.AddKey();
		GameObject.Destroy(this);
	}
}