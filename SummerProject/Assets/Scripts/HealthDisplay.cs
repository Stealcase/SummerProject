using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {
    /// <summary>
    /// This attributes should be read from a unit's Health-Component
    /// </summary>
    [Header("Health Attributes")]
    public IntVariable healthValue;
    public IntVariable maxHealthValue;
    public IntVariable shieldValue;
    public IntVariable MagicalShield;
    public Slider healthFill;
    public Slider delayedHealthSlider;
    

    [Header("Canvas Elements")]
    public RectTransform[] Bars;
    public RectTransform[] Amounts;


    void Update()   
    {
        healthFill.maxValue = maxHealthValue.Value;

        // this validation should be a unit's health-component
        int health = Mathf.Clamp(healthValue.Value, 0, maxHealthValue.Value);
        int shield = Mathf.Max(shieldValue.Value, 0);
        // relevant part beginning here:

        var max = Mathf.Max(maxHealthValue.Value, healthValue.Value + shieldValue.Value + MagicalShield.Value);
        float HealthPercent = (float)healthValue.Value / max;
        float PhysPercent = (float)shieldValue.Value / max;

        Bars[0].anchorMax = new Vector2(HealthPercent, 1);

        Bars[1].anchorMin = new Vector2(HealthPercent, 0);
        Bars[1].anchorMax = new Vector2(HealthPercent + PhysPercent, 1);

        Bars[2].anchorMin = new Vector2(HealthPercent + PhysPercent, 0);
        Bars[2].anchorMax = new Vector2(HealthPercent + PhysPercent, 1);


    }

}