using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Slider slider;
    private Image fill;
    public Gradient gradient;

    public void InitHealth(float health) {
        slider = GetComponent<Slider>();
        fill = transform.Find("Fill").GetComponent<Image>();
        slider.maxValue = health;
        SetHealth(health);
        gameObject.SetActive(false);
    }

    public void SetHealth(float health) {
        gameObject.SetActive(true);
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
