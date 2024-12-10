using UnityEngine;

public static class PlayerData {
    /// Manually saves data
    public static void Save() {
        PlayerPrefs.Save();
    }

    #region process game

    public static int CurrentLevel {
        get {
            return PlayerPrefs.GetInt("CurrentLevel", 1);
        }
        set {
            PlayerPrefs.SetInt("CurrentLevel", value);
        }
    }

    public static int NewestLevel {
        get {
            return PlayerPrefs.GetInt("NewestLevel", 1);
        }
        set {
            PlayerPrefs.SetInt("NewestLevel", value);
        }
    }
    public static bool IsLevelUnlocked(int level) {
        return level <= NewestLevel;
    }

    public static void IncreaseLevel() {
        CurrentLevel++;

        if (CurrentLevel > GameData.levels.Length) {
                // TODO: Load credit scene
                CurrentLevel--;
                return;
        }

        Debug.Log($"PlayerData.DecreaseLevel() Increase to Level {CurrentLevel}");

        bool isUnlockingNewLevel = CurrentLevel > NewestLevel;

        if (isUnlockingNewLevel) {
            NewestLevel = CurrentLevel;
        }

    }

    public static void DecreaseLevel() {
        CurrentLevel--;

        if (CurrentLevel <= 0) {
            return;
        }

        Debug.Log($"PlayerData.DecreaseLevel() Decrease to Level {CurrentLevel}");
    }



    #endregion process game

    #region Settings

    public static bool Vibration {
        get {
            if (PlayerPrefs.HasKey("vibration")) {
                return PlayerPrefs.GetInt("vibration") == 1;
            }

            return true;
        }
        set {
            PlayerPrefs.SetInt("vibration", value ? 1 : 0);
        }
    }

    public static float Music {
        get {
            if (PlayerPrefs.HasKey("music")) {
                return PlayerPrefs.GetFloat("music");
            }
            return .8f;
        }
        set {
            PlayerPrefs.SetFloat("music", value);
        }
    }

    public static float Sound {
        get {
            if (PlayerPrefs.HasKey("sound")) {
                return PlayerPrefs.GetFloat("sound");
            }
            return .8f;
        }
        set {
            PlayerPrefs.SetFloat("sound", value);
        }
    }

    // Percentage
    public static int GameSpeed {
        get {
            if (PlayerPrefs.HasKey("GameSpeed")) {
                return PlayerPrefs.GetInt("GameSpeed");
            }
            return 100;
        }
        set {
            PlayerPrefs.SetInt("GameSpeed", value);
        }
    }

    #endregion Settings


}