using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    [SerializeField]
    private Transform _window;
    
    public void OpenWindow() => _window.gameObject.SetActive(true);

    public void CloseWindow() => _window.gameObject.SetActive(false);

    public void LoadSceneByIndex(int index) => SceneManager.LoadScene(index);
}
