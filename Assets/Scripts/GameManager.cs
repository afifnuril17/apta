using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using TMPro;

public class GameManager : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void FinishGame(string str);
	[DllImport("__Internal")]
	private static extern void AddRendered(bool str);
	// Start is called before the first frame update
	public static GameManager instanceHandler;
	public GameObject introPanel;
	public GameObject selesaiPanel;
	public GameObject restartPanel;
	public GameObject mainPanel;
	public TextMeshProUGUI intText;
	public float myInt;
	private int minutes;
	private int seconds;
	float selesaiFloat;
	string[] value;
	int getId_user;
	public float max;
	public bool lanjut = false;
	public int selesaiInt = 3;
	public TextMeshProUGUI timerText;
	string pathsplit;
	bool autoNext = true;
	string root_env = "";
	private void Awake()
	{
		Application.runInBackground = true;
		instanceHandler = this;
		DontDestroyOnLoad(this.gameObject);
	}
	void Start()
	{
		AddRendered(true);
	}
	void Update()
	{
		intText.text = ((int)selesaiInt).ToString();
		if (lanjut == true)
		{
			myInt -= Time.deltaTime;

			DisplayTime(myInt);

			if ((int)myInt == 0)
			{
				gameObject.GetComponent<SoalManager>().is20second = true;
				gameObject.GetComponent<SoalManager>().time = 20f;
				Next();
				lanjut = false;
			}
			if (GetComponent<SoalManager>().score >= 240)
			{
				lanjut = false;
				Next();
			}
		}
	}
	void DisplayTime(float timeDisplay)
	{
		minutes = Mathf.FloorToInt(timeDisplay / 60);
		seconds = Mathf.FloorToInt(timeDisplay % 60);
		timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}
	public void Next()
	{
		if (GetComponent<SoalManager>().score >= 240)
		{
			StartCoroutine(SendScore());
			selesaiPanel.SetActive(true);
			mainPanel.SetActive(false);
			restartPanel.SetActive(false);
		}
		else
		{
			selesaiPanel.SetActive(false);
			mainPanel.SetActive(false);
			restartPanel.SetActive(true);
		}
	}
	public void StartGame(string user_id)
	{
		introPanel.SetActive(false);
		mainPanel.SetActive(true);
		value = user_id.Split(',');
		getId_user = int.Parse(value[0]);
		pathsplit = value[1];
		root_env = value[2];
		lanjut = true;
	}
	IEnumerator SendScore()
	{
		WWWForm form = new WWWForm();
		form.AddField("user_id", getId_user);
		form.AddField("point", "240");
		form.AddField("game_id", 3);
		UnityWebRequest www = UnityWebRequest.Post(root_env + "/mini-game-add-score", form);
		yield return www.SendWebRequest();
		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log("error5 " + www.error);
		}
		else
		{
			StartCoroutine(MinWaktu());
			Debug.Log(www);
		}
	}
	IEnumerator MinWaktu()
	{
		while (selesaiInt >= 0)
		{
			selesaiFloat += Time.deltaTime;
			if (selesaiFloat > 1f)
			{
				selesaiFloat = 0f;
				selesaiFloat = 0f;
				selesaiInt--;
			}
			if (selesaiInt <= 0)
			{
				FinishGame(pathsplit);
				selesaiInt = 0;
				break;
			}
			yield return null;
		}
	}
	public void CobaLagi()
	{
		GetComponent<SoalManager>().Reset();
		myInt = max;
		lanjut = true;
		selesaiPanel.SetActive(false);
		mainPanel.SetActive(true);
		restartPanel.SetActive(false);
	}
}
