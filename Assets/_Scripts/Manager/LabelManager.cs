using UnityEngine;

public class LabelManager : Singleton<LabelManager> {
    public enum LabelType {
        Circle,
        Vertical,
        Horizontal,
        V,
        W
    }

    public LabelType GetRandomLabelType() {
        return (LabelType)Random.Range(0, System.Enum.GetNames(typeof(LabelType)).Length);
    }

    public LabelType GetLabelTypeByString(string label) {
        if (System.Enum.TryParse(label, out LabelType labelType)) {
            return labelType;
        } else {
            Debug.LogError($"Label '{label}' not found, CHECK GESTURE ID");
            return LabelType.Circle; 
        }
    }
} 
