using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEditor.PackageManager.Requests;
using Newtonsoft.Json.Linq;

public class GPTSoVITSTextToSpeech : TTS
{
    #region ��������
    [Header("���زο�����Ƶ����������")]
    [SerializeField] private AudioClip m_ReferenceClip = null;//�ο���Ƶ
    [Header("�ο���Ƶ���������ݣ���������")]
    [SerializeField] private string m_ReferenceText="";//�ο���Ƶ�ı�
    [Header("�ο���Ƶ������")]
    [SerializeField] private Language m_ReferenceTextLan = Language.����;//�ο���Ƶ������
    [Header("�ϳ���Ƶ������")]
    [SerializeField] private Language m_TargetTextLan= Language.����;//�ϳ���Ƶ������
    private string m_AudioBase64String = "";//�ο���Ƶ��base64����
    [SerializeField] private string m_SplitType = "����";//�ϳ��ı��зַ�ʽ
    [SerializeField] private int m_Top_k = 5;
    [SerializeField] private float m_Top_p = 1;
    [SerializeField] private float m_Temperature = 1;
    [SerializeField] private bool m_TextReferenceMode = false;
    #endregion

    private void Awake()
    {
        AudioTurnToBase64();
    }

    /// <summary>
    /// �����ϳɣ����غϳ��ı�
    /// </summary>
    /// <param name="_msg"></param>
    /// <param name="_callback"></param>
    public override void Speak(string _msg, Action<AudioClip, string> _callback)
    {
        StartCoroutine(GetVoice(_msg, _callback));
    }

    /// <summary>
    /// �ϳ���Ƶ
    /// </summary>
    /// <param name="_msg"></param>
    /// <param name="_callback"></param>
    /// <returns></returns>
    private IEnumerator GetVoice(string _msg, Action<AudioClip, string> _callback)
    {
        stopwatch.Restart();
        //���ͱ���
        string _postJson = GetPostJson(_msg);

        using (UnityWebRequest request = new UnityWebRequest(m_PostURL, "POST"))
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(_postJson);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(data);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.responseCode == 200)
            {
                string _text = request.downloadHandler.text;

                Response _response=JsonUtility.FromJson<Response>(_text);
                string _wavPath = _response.data[0].name;


                if (_wavPath == "")
                {
                    //����ϳ�ʧ�ܣ��ٳ���һ��
                    StartCoroutine(GetVoice(_msg, _callback));
                }
                else
                {
                    StartCoroutine(GetAudioFromFile(_wavPath, _msg, _callback));
                }

            }
            else
            {
                Debug.LogError("�����ϳ�ʧ��: " + request.error);
            }
        }

        stopwatch.Stop();
        Debug.Log("GPT-SoVITS�ϳɺ�ʱ��" + stopwatch.Elapsed.TotalSeconds);
    }


    /// <summary>
    /// �����͵�Json����
    /// </summary>
    /// <param name="_msg"></param>
    /// <param name="_lan"></param>
    /// <returns></returns>
    private string GetPostJson(string _msg)
    {

        if(m_ReferenceText==""|| m_ReferenceClip == null)
        {
            Debug.LogError("GPT-SoVITSδ���òο���Ƶ��ο��ı�");
            return null;
        }


        // �������ݽṹ
        var jsonData = new
        {
            data = new List<object>
            {
                new { name = "audio.wav", data = "data:audio/wav;base64,"+m_AudioBase64String },
                m_ReferenceText,
                m_ReferenceTextLan.ToString(),
                _msg,
                m_TargetTextLan.ToString(),
                m_SplitType,
                m_Top_k,
                m_Top_p,
                m_Temperature,
                m_TextReferenceMode
            }
        };

        // ������ת��ΪJSON��ʽ
        string jsonString = JsonConvert.SerializeObject(jsonData, Formatting.Indented);

        return jsonString;
    }

    /// <summary>
    /// ����ƵתΪbase64
    /// </summary>
    private void AudioTurnToBase64()
    {
        if (m_ReferenceClip == null)
        {
            Debug.LogError("GPT-SoVITSδ���òο���Ƶ");
            return;
        }
        byte[] audioData = WavUtility.FromAudioClip(m_ReferenceClip);
        string base64String = Convert.ToBase64String(audioData);
        m_AudioBase64String= base64String;
    }
    /// <summary>
    /// �ӱ��ػ�ȡ�ϳɺ����Ƶ�ļ�
    /// </summary>
    /// <param name="_path"></param>
    /// <param name="_msg"></param>
    /// <param name="_callback"></param>
    /// <returns></returns>
    private IEnumerator GetAudioFromFile(string _path,string _msg, Action<AudioClip, string> _callback)
    {
        string filePath = "file://" + _path;
        using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(filePath, AudioType.WAV))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(request);
                _callback(audioClip, _msg);
            }
            else
            {
                Debug.LogError("��Ƶ��ȡʧ�� ��" + request.error);
            }
        }


    }



    #region ���ݶ���

    /*
     ���͵����ݸ�ʽ

    {
  "data": [
    {"name":"audio.wav","data":"data:audio/wav;base64,UklGRiQAAABXQVZFZm10IBAAAAABAAEARKwAAIhYAQACABAAZGF0YQAAAAA="},
    "hello world",
    "����",
    "hello world",
    "����",
    ]}   

    */
    /*
    
    ���ص����ݸ�ʽ

    {
	"data": [
		{
			"name": "E:\\AIProjects\\GPT-SoVITS\\TEMP\\tmp53eoney1.wav",
			"data": null,
			"is_file": true
		}
	],
	"is_generating": true,
	"duration": 7.899699926376343,
	"average_duration": 7.899699926376343
    }

    */

    [Serializable]
    public class Response
    {
        public List<AudioBack> data=new List<AudioBack>();
        public bool is_generating = true;
        public float duration;
        public float average_duration;
    }
    [Serializable]
    public class AudioBack
    {
        public string name=string.Empty;
        public string data = string.Empty;
        public bool is_file = true;

    }

    public enum Language
    {
        ����,
        Ӣ��,
        ����,
        ��Ӣ���,
        ��Ӣ���,
        �����ֻ��
    }

 


    #endregion


}
