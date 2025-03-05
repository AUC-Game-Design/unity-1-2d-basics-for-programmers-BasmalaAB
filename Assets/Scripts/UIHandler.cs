using UnityEngine;
using UnityEngine.UIElements;


public class UIHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private VisualElement m_Healthbar;
    public static UIHandler instance { get; private set; }

    public float displayTime = 5.5f;
    private VisualElement m_NonPlayerDialogue;
    private float m_TimerDisplay;
    private Label m_text_in_label;

    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);

        m_text_in_label = uiDocument.rootVisualElement.Q<Label>("Label");
        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;
    }

    public void SetHealthValue(float value)
    {
        m_Healthbar.style.width = Length.Percent(value * 100.0f);
    }

    private void Update()
    {
        if (m_TimerDisplay > 0.0f)
        {
            m_TimerDisplay -= Time.deltaTime;

            if(m_TimerDisplay < 0.0f)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }
    }

    public void DisplayDialogue(string text)
    {
            m_text_in_label.text = text;
            m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
            m_TimerDisplay = displayTime;
    }
}
