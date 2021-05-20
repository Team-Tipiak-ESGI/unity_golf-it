using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public Button monBtn;

    void Start()
    {
        Button btn = monBtn.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        
        SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.name);
    }
}
