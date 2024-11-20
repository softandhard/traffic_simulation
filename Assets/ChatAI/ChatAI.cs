using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Crosstales.RTVoice.Tool;
using Crosstales.RTVoice;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class ChatAI : MonoBehaviour
{
    public string token;
    //������д�ٶ�ǧ����ģ�����Ӧ��api key
    public string api_key = "pmCmzp0FM9qATJqFesvlANQg";
    //������д�ٶ�ǧ����ģ�����Ӧ��secret key
    public string secret_key = "xQBjsZHTI7tjEAqIsLUmqTk16gwJ9R7K";
    /// <summary>
    /// ѡ���ģ������
    /// </summary>
    [Header("����ģ������")]
    public ModelType m_ModelType = ModelType.ERNIE_Speed;
    //���Ͱ�ť
    public Button sendBtn;
    //�����
    public InputField info;
    //AI��Ӧ
    public Text responseText;
    // ��ʷ�Ի�
    private List<message> historyList = new List<message>();

    public void Awake()
    {
        //��ʼ������һ��,��ȡtoken
        StartCoroutine(GetToken());
        sendBtn.onClick.AddListener(OnSend);
    }

    public void OnSend()
    {
        OnSpeak(info.text);
    }
    //��ʼ�Ի�
    public void OnSpeak(string talk)
    {
        StartCoroutine(Request(talk));
    }
    private IEnumerator GetToken()
    {
        //��ȡtoken��api��ַ
        string _token_url = string.Format("https://aip.baidubce.com/oauth/2.0/token" + "?client_id={0}&client_secret={1}&grant_type=client_credentials", api_key, secret_key);
        using (UnityWebRequest request = new UnityWebRequest(_token_url, "POST"))
        {
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.isDone)
            {
                string msg = request.downloadHandler.text;
                TokenInfo mTokenInfo = JsonUtility.FromJson<TokenInfo>(msg);
                //����Token
                token = mTokenInfo.access_token;
            }
        }
    }

    /// <summary>
    /// ��������
    /// </summary> 
    /// <param name="_postWord"></param>
    /// <param name="_callback"></param>
    /// <returns></returns>
    public IEnumerator Request(string talk)
    {
        string url = "https://aip.baidubce.com/rpc/2.0/ai_custom/v1/wenxinworkshop/chat/";
        string postUrl = url + GetModelType(m_ModelType) + "?access_token=" + token;
        historyList.Add(new message("user", talk));
   
        RequestData postData = new RequestData
        {
            messages = historyList
        };
        using (UnityWebRequest request = new UnityWebRequest(postUrl, "POST"))
        {
            Debug.Log("talk=" + postData.messages);
            string jsonData = JsonUtility.ToJson(postData);
            Debug.Log("json=" + jsonData);

            byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(data);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.responseCode == 200)
            {
                string _msg = request.downloadHandler.text;
                ResponseData response = JsonConvert.DeserializeObject<ResponseData>(_msg);
 
                //������ʷ����
                historyList.Add(new message("assistant", response.result));
                responseText.text = response.result;
                Debug.Log("res=" + response);
            }
        }
    }


    /// <summary>
    /// ��ȡ��Դ
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private string GetModelType(ModelType type)
    {
        // ���
        if (type == ModelType.ERNIE_Speed)
        {
            return "ai_apaas";
        }
        // ���
        if (type == ModelType.Yi_34B_Chat)
        {
            return "yi_34b_chat";
        }
        // ��ʱ���
        if (type == ModelType.ERNIE_Speed_8K)
        {
            return "ernie_speed";
        }
        // �շ�
        if (type == ModelType.Qianfan_Chinese_Llama_2_7B)
        {
            return "qianfan_chinese_llama_2_7b";
        }
        return "";
    }

    //���͵�����
    private class RequestData
    {
        //���͵���Ϣ
        public List<message> messages = new List<message>();
        //�Ƿ���ʽ���
        public bool stream = false;
        public string user_id = string.Empty;
    }

    private class message
    {
        //��ɫ
        public string role = string.Empty;
        //�Ի�����
        public string content = string.Empty;
        public message() { }
        public message(string _role, string _content)
        {
            if (_role != "")
            {
                role = _role;
            }
            content = _content;
        }
    }
    //���յ�����
    private class ResponseData
    {
        //���ֶԻ���id 
        public string id = string.Empty;
        public int created;
        //��ʾ��ǰ�Ӿ�����,ֻ������ʽ�ӿ�ģʽ�»᷵�ظ��ֶ�
        public int sentence_id;
        //��ʾ��ǰ�Ӿ��Ƿ������һ��,ֻ������ʽ�ӿ�ģʽ�»᷵�ظ��ֶ�
        public bool is_end;
        //��ʾ��ǰ�Ӿ��Ƿ������һ��,ֻ������ʽ�ӿ�ģʽ�»᷵�ظ��ֶ�
        public bool is_truncated;
        //���ص��ı�
        public string result = string.Empty;
        //��ʾ�����Ƿ���ڰ�ȫ
        public bool need_clear_history;
        //��need_clear_historyΪtrueʱ�����ֶλ��֪�ڼ��ֶԻ���������Ϣ������ǵ�ǰ���⣬ban_round=-1
        public int ban_round;
        //tokenͳ����Ϣ��token�� = ������+������*1.3 
        public Usage usage = new Usage();
    }

    private class Usage
    {
        //����tokens��
        public int prompt_tokens;
        //�ش�tokens��
        public int completion_tokens;
        //tokens����
        public int total_tokens;
    }


    /// <summary>
    /// ģ������
    /// </summary>
    public enum ModelType
    {
        ERNIE_Speed,
        Yi_34B_Chat,
        ERNIE_Speed_8K,
        Qianfan_Chinese_Llama_2_7B,
    }

    /// <summary>
    /// ���ص�token
    /// </summary>
    [System.Serializable]
    public class TokenInfo
    {
        public string access_token = string.Empty;
    }
}

