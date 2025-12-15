using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuNavigator : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform arrow;      // Arrow UI object
    [SerializeField] private RectTransform startItem;  // Start text RectTransform
    [SerializeField] private RectTransform quitItem;   // Quit text RectTransform

    [Header("Settings")]
    [SerializeField] private float arrowXOffset = -70f; // how far left of text the arrow sits
    [SerializeField] private string startSceneName = "room1";

    private int index = 0; // 0 = Start, 1 = Quit

    void Start()
    {
        UpdateArrowPosition();
    }

    void Update()
    {
        // Move selection (use GetKeyDown so it moves once per press)
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            index = 0;
            UpdateArrowPosition();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            index = 1;
            UpdateArrowPosition();
        }

        // Confirm
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (index == 0)
            {
                SceneManager.LoadScene(startSceneName);
            }
            else
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }
    }

    private void UpdateArrowPosition()
    {
        RectTransform target = (index == 0) ? startItem : quitItem;
        if (index == 1)
        {
            arrow.localPosition = target.localPosition + new Vector3(-73, 0, 0);
        }
        arrow.localPosition = target.localPosition + new Vector3(-62, 0, 0);
    }
}