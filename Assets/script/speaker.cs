using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crosstales.RTVoice.Tool;
using Crosstales.RTVoice;

public class speaker : MonoBehaviour
{
	public SpeechText SpeechText;
	public TextFileSpeaker fileSpeaker;

	void Start()
	{
		SpeechText.Text = "���,���ǲ����ı�";
	}


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			SpeechText.Text = "���,���ǲ����ı�";
			SpeechText.Speak();
		}
		if(Input.GetKeyDown(KeyCode.Return))
        {
			fileSpeaker.SpeakText();
        }
	}
}

