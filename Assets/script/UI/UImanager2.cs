using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UImanager2 : MonoBehaviour
{
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject);
                GameObject obj = hit.collider.gameObject;
                if (obj.CompareTag("RYBpole"))
                {
                    CanvasGroup canvas1 = hit.collider.gameObject.transform.GetChild(0).GetComponent<CanvasGroup>();
                    canvas1.alpha = 1;
                    canvas1.blocksRaycasts = true;
                    canvas1.interactable = true;
                }
            }
        }
    }
    public void exit2to0()
    {
        SceneManager.LoadScene(0);
    }
}
