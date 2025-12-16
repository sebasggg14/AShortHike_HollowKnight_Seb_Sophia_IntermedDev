using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PartyMenu : MonoBehaviour
{[Header("UI References")]
    [SerializeField] private RectTransform arrow;      // Arrow UI object
    [SerializeField] private RectTransform startItem;  // Start text RectTransform
    [SerializeField] private RectTransform quitItem;   // Quit text RectTransform

    [Header("Settings")]
    [SerializeField] private float arrowXOffset = -70f; // how far left of text the arrow sits

    private int index = 0; // 0 = Start, 1 = Quit
    AudioSource src;
    public AudioClip bgMusic;

    void Start()
    {
        UpdateArrowPosition();
        src = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Move selection (use GetKeyDown so it moves once per press)
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            index = 0;
            src = gameObject.AddComponent<AudioSource>();
            src.clip = bgMusic;
            src.spatialBlend = 0f; // 2D (THIS is why it's loud)
            src.volume = 1f;
            src.loop = false;
            src.Play();
            UpdateArrowPosition();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            index = 1;
            src = gameObject.AddComponent<AudioSource>();
            src.clip = bgMusic;
            src.spatialBlend = 0f; // 2D (THIS is why it's loud)
            src.volume = 1f;
            src.loop = false;
            src.Play();
            UpdateArrowPosition();
        }

        // Confirm
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (index == 0)
            {
                SceneManager.LoadScene("goodending");
            }
            else
            {
                SceneManager.LoadScene("gameover");
            }
        }
    }

    private void UpdateArrowPosition()
    {
        RectTransform target = (index == 0) ? startItem : quitItem;
        arrow.localPosition = target.localPosition + new Vector3(-0.04f, 0, 0);
    }
}
