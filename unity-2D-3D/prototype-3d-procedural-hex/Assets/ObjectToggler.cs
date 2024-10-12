using UnityEngine;
using UnityEngine.UI;
using TMPro; // Aggiungi questa riga se usi TextMeshPro

public class ObjectToggler : MonoBehaviour
{
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private Button toggleButton;
    [SerializeField] private TextMeshProUGUI buttonText; // Per TextMeshPro
    // [SerializeField] private Text buttonText; // Per UI Legacy Text

    private void Start()
    {
        ApplyState();

        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleObject);
        }

        UpdateButtonText();
    }

    public void ToggleObject()
    {
        isEnabled = !isEnabled;
        ApplyState();
        UpdateButtonText();
    }

    private void ApplyState()
    {
        gameObject.SetActive(isEnabled);
    }

    private void UpdateButtonText()
    {
        if (buttonText != null)
        {
            buttonText.text = isEnabled ? "Disabilita" : "Abilita";
        }
    }

    private void OnDestroy()
    {
        if (toggleButton != null)
        {
            toggleButton.onClick.RemoveListener(ToggleObject);
        }
    }
}