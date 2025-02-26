using UnityEngine;
using UnityEngine.UIElements;


public class UIHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private VisualElement m_Healthbar;
    public static UIHandler instance { get; private set; }
    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);
    }

    public void SetHealthValue(float value)
    {
        m_Healthbar.style.width = Length.Percent(value * 100.0f);
    }
}
