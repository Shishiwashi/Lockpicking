using UnityEngine;
using System.Collections;

[System.Serializable] public class StageProperties
{
	public enum StageType
	{
		Slider, Password
	}
	
	public StageType type = StageType.Slider;
}