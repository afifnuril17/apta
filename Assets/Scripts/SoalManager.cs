using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoalManager : MonoBehaviour
{
	public TextMeshProUGUI score_text;
	public TextMeshProUGUI alert_salah;
	public TextMeshProUGUI soal_txt;
	public TMP_InputField answer;
	public string[] soal;
	public int[] jawaban;
	// Start is called before the first frame update
	public int score;
	public int idx = 0;
	public bool is20second = false;
	public float time = 20f;
	// Update is called once per frame
	void Update()
	{
		score_text.text = score.ToString();
		if (!is20second)
		{
			time -= Time.deltaTime;
			if ((int)time == 0)
			{
				is20second = true;
				NextPage();
			}
			soal_txt.text = soal[idx];
		}
	}
	public void CheckAnswer()
	{
		if (int.Parse(answer.text) == jawaban[idx])
		{
			score += 20;
			NextPage();
			alert_salah.gameObject.SetActive(false);
			answer.text = "";
		}
		else
		{
			alert_salah.gameObject.SetActive(true);
			alert_salah.gameObject.GetComponent<CountAlert>().myInt = 3f;
			alert_salah.gameObject.GetComponent<CountAlert>().is_show = true;
			alert_salah.text = "Jawaban yang Anda Masukkan Salah!";
		}
	}
	public void NextPage()
	{
		if (idx < soal.Length - 1)
		{
			time = 20f;
			soal_txt.text = soal[idx];
			idx += 1;
			is20second = false;
		}
		else
		{
			GameManager.instanceHandler.Next();
		}
	}
	public void Reset()
	{
		idx = 0;
		score = 0;
		is20second = false;
		time = 20f;
		alert_salah.gameObject.SetActive(false);
		answer.text = "";
	}
}
