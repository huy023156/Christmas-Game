using System;
using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI pointsText;

    public void SetText(string text) {
        pointsText.text = text;
    }
}