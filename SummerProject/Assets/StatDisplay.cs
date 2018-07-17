using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplay : MonoBehaviour {
    [SerializeField] string displayName;
    private string displayValue;
    public Sprite sprite;
    public Image image;
    public IntVariable variable;
    private int value;
    public TextMeshProUGUI statName;
    public TextMeshProUGUI statValue;

    private int statNumber;

    private void Start()
    {
        Prime(variable);
        statName.text = "<uppercase>" + displayName + "</uppercase>";
        statValue.text = displayValue;

    }
    private void Prime(IntVariable variable)
    {
        this.variable = variable;
        value = variable.Value;
        image.sprite = sprite;
        displayValue = value.ToString();
        }
    }

