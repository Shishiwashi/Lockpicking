using UnityEngine;
using System.Collections;

public class PasswordButton : MonoBehaviour
{
	public string letter = "a";
	public Password password;

	public void AddLetter()
	{
		password.AddLetter(letter);
	}
}