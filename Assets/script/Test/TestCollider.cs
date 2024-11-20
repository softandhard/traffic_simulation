using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestCollider : MonoBehaviour
{
    private GameObject[] RybCenter;
    private List<float> rybcenterlist;
    private Dictionary<float, GameObject> rybcenterdic;
    private Vector3 centerposition;
    Collider[] colliders;
    public Vector3 size = new Vector3(5f,5f, 5f);
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        RybCenter = GameObject.FindGameObjectsWithTag("RYBpole");
        rybcenterlist = new List<float>();
        rybcenterdic = new Dictionary<float, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        getcenter();
        Vector3 position = transform.position;

        //�ڵ�ǰλ�ô���һ�������巶Χ
        colliders = Physics.OverlapBox(centerposition+offset, size / 2f);

        // ����������ײ��
        if (Input.GetMouseButtonDown(0))
        {
            foreach (Collider collider in colliders)
            {
                // �����ҵ�������
                GameObject obj = collider.gameObject;
                Debug.Log("Found object: " + obj.name);
            }
        }
 
       
    }

    public Vector3 getcenter()
    {
        rybcenterlist.Clear();
        rybcenterdic.Clear();

        for (int i = 0; i < RybCenter.Length; i++)
        {
            Vector3 direction = RybCenter[i].transform.position - transform.position; //λ�ò����                                                                //}
            float dis = Vector3.Distance(RybCenter[i].transform.position, transform.position);
            rybcenterdic.Add(dis, RybCenter[i]);
            if (!rybcenterlist.Contains(dis))
            {
                rybcenterlist.Add(dis);
            }
        }
        rybcenterlist.Sort();
        GameObject obj;
        if (rybcenterlist.Count != 0)
        {
            rybcenterdic.TryGetValue(rybcenterlist[0], out obj);
            centerposition = obj.transform.position;
        }
        else
        {
            centerposition = Vector3.zero;
        }
        return centerposition;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position+offset, size);
    }
}
