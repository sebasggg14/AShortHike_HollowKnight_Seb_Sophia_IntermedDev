using UnityEngine;

public class GenericDialogue : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private GameObject[] dialogueBoxes; // assign in Inspector
    [SerializeField] private GameObject wBox;

    [Header("Audio")]
    [SerializeField] private AudioClip defaultNoise;

    private int currentDialogue = 0;
    private bool playerInside = false;

    void Start()
    {
        // Hide everything at start
        wBox.SetActive(false);

        foreach (var box in dialogueBoxes)
            box.SetActive(false);
    }

    void Update()
    {
        if (!playerInside) return;

        // Show prompt when not pressing W
        if (!Input.GetKey(KeyCode.W))
            wBox.SetActive(true);

        // Advance dialogue ONCE per key press
        if (Input.GetKeyDown(KeyCode.W))
        {
            wBox.SetActive(false);
            AdvanceDialogue();
        }
    }

    void AdvanceDialogue()
    {
        // Hide previous
        if (currentDialogue > 0)
            dialogueBoxes[currentDialogue - 1].SetActive(false);

        // Stop if finished
        if (currentDialogue >= dialogueBoxes.Length)
            return;

        // Show current
        dialogueBoxes[currentDialogue].SetActive(true);
        currentDialogue++;

        PlaySound();
    }

    void PlaySound()
    {
        if (defaultNoise == null) return;

        var go = new GameObject("DialogueSFX");
        go.transform.position = Camera.main.transform.position;

        var src = go.AddComponent<AudioSource>();
        src.clip = defaultNoise;
        src.spatialBlend = 0f;
        src.volume = 1f;
        src.Play();

        Destroy(go, defaultNoise.length);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInside = true;
        wBox.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;
        currentDialogue = 0;

        wBox.SetActive(false);
        foreach (var box in dialogueBoxes)
            box.SetActive(false);
    }
}