using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
//using UnityTools;
/// 语音识别
public class SpeechRecognition : MonoBehaviour
{
    // 短语识别器
    private PhraseRecognizer m_PhraseRecognizer;
    // 关键字
    public string[] keywords = { };
    // 可信度
    public ConfidenceLevel m_confidenceLevel = ConfidenceLevel.Medium;
    void Start()
    {
        if (m_PhraseRecognizer == null)
        {
            //创建一个识别器
            m_PhraseRecognizer = new KeywordRecognizer(keywords, m_confidenceLevel);
            //通过注册监听的方法
            m_PhraseRecognizer.OnPhraseRecognized += M_PhraseRecognizer_OnPhraseRecognized;
            //开启识别器
            m_PhraseRecognizer.Start();
        }
    }
    /// 当识别到关键字时，会调用这个方法
    private void M_PhraseRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        _SpeechRecognition(args.text);
        print(args.text);
    }
    private void OnDestroy()
    {
        //判断场景中是否存在语音识别器，如果有，释放
        if (m_PhraseRecognizer != null)
            m_PhraseRecognizer.Dispose();
    }
    /// 识别到语音的操作
    void _SpeechRecognition(string msg)
    {
        switch (msg)
        {
            case "在吗":
                Debug.Log("我在，你说");
                break;
            case "你好":
                Debug.Log("我在，你说");
                break;

            default:
                break;
        }
    }
}

