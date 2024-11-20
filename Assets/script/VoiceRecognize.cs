using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
//using UnityTools;
/// ����ʶ��
public class SpeechRecognition : MonoBehaviour
{
    // ����ʶ����
    private PhraseRecognizer m_PhraseRecognizer;
    // �ؼ���
    public string[] keywords = { };
    // ���Ŷ�
    public ConfidenceLevel m_confidenceLevel = ConfidenceLevel.Medium;
    void Start()
    {
        if (m_PhraseRecognizer == null)
        {
            //����һ��ʶ����
            m_PhraseRecognizer = new KeywordRecognizer(keywords, m_confidenceLevel);
            //ͨ��ע������ķ���
            m_PhraseRecognizer.OnPhraseRecognized += M_PhraseRecognizer_OnPhraseRecognized;
            //����ʶ����
            m_PhraseRecognizer.Start();
        }
    }
    /// ��ʶ�𵽹ؼ���ʱ��������������
    private void M_PhraseRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        _SpeechRecognition(args.text);
        print(args.text);
    }
    private void OnDestroy()
    {
        //�жϳ������Ƿ��������ʶ����������У��ͷ�
        if (m_PhraseRecognizer != null)
            m_PhraseRecognizer.Dispose();
    }
    /// ʶ�������Ĳ���
    void _SpeechRecognition(string msg)
    {
        switch (msg)
        {
            case "����":
                Debug.Log("���ڣ���˵");
                break;
            case "���":
                Debug.Log("���ڣ���˵");
                break;

            default:
                break;
        }
    }
}

