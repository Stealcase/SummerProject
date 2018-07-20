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
    private Slider healthFill;
    public Slider delayedHealthFill;
    private float health;
    private float shield;
    private float maxHealth;

    [Header("Canvas Elements")]
    public RectTransform[] Bars;
    public RectTransform[] Amounts;



    void Update()
    {
        // this validation should be a unit's health-component
        health = Mathf.Clamp(healthValue.Value, 0, maxHealthValue.Value);
        shield = Mathf.Max(shieldValue.Value, 0);


        // relevant part beginning here:

        var max = Mathf.Max(maxHealthValue.Value, health + shield);

        float HealthPercent = (float)health / max;
        float PhysPercent = (float)shield / max;

        Bars[0].anchorMax = new Vector2(HealthPercent, 1);

        Bars[1].anchorMin = new Vector2(HealthPercent, 0);
        Bars[1].anchorMax = new Vector2(HealthPercent + PhysPercent, 1);


    }

}