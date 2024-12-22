using UnityEngine;

public class LoadSaveSys : MonoBehaviour
{
    public void SaveLevel(string levelName)
    {
        PlayerPrefs.SetString("LastPlayedLevel", levelName);
    }
    public string LoadLevel()
    {
        return PlayerPrefs.GetString("LastPlayedLevel", "DefaultLevel");
    }
}
