using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionButton : MonoBehaviour
{
    public GameObject temptext;
	[SerializeField]
	bool toggle = true;
	public void TempTextToggle()
	{
		if (toggle)
		{
			toggle = !toggle;
			ShowTxt();
		}
		else
		{
			toggle = !toggle;
			HideTxt();
		}
	}
	public void HideTxt()
	{
		temptext.SetActive(false);
	}
	public void ShowTxt()
	{
		temptext.SetActive(true);
	}
}
