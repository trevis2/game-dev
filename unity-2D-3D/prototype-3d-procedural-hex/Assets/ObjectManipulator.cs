using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManipulator : MonoBehaviour
{
    [SerializeField] private HexRenderer scriptTarget;
    [SerializeField] private Slider integerSlider;
    [SerializeField] private TMP_Dropdown booleanDropdown;

    private void Start()
    {
        if (scriptTarget == null)
        {
            scriptTarget = GetComponent<HexRenderer>();
        }
        // Configura lo Slider
        if (integerSlider != null)
        {
            integerSlider.onValueChanged.AddListener(UpdateIntegerValue);
            integerSlider.value = scriptTarget.superiorHeight;
        }

        // Configura la Dropdown
        if (booleanDropdown != null)
        {
            booleanDropdown.onValueChanged.AddListener(UpdateBooleanValue);
            booleanDropdown.value = scriptTarget.applyRotation ? 1 : 0;
        }
    }

    private void UpdateIntegerValue(float value)
    {
        scriptTarget.superiorHeight = Mathf.RoundToInt(value);
    }
    private void UpdateBooleanValue(int index)
    {
        scriptTarget.applyRotation = (index == 1);
    }
    private void OnDestroy()
    {
        if (integerSlider != null)
            integerSlider.onValueChanged.RemoveListener(UpdateIntegerValue);

        if (booleanDropdown != null)
            booleanDropdown.onValueChanged.RemoveListener(UpdateBooleanValue);
    }
}
