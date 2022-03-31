using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountAlert : MonoBehaviour
{
	public float myInt = 3;
	public bool is_show = false;

	// Update is called once per frame
	void Update()
	{
		if (is_show)
		{
			myInt -= Time.deltaTime;

			if ((int)myInt == 0)
			{
				is_show = false;
				gameObject.SetActive(false);
			}

		}
	}
}
