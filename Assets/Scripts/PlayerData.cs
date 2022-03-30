using UnityEngine;

public class PlayerData
{
    private const string name = "Name";
    public string Name
    {
        get { return PlayerPrefs.GetString(name); }
        set { PlayerPrefs.SetString(name, value); }
    }
    private const string level = "Level";
    public int Level
    {
        get { return PlayerPrefs.GetInt(level, 1); }
        set { PlayerPrefs.SetInt(level, value); }
    }
    private const string exp = "Experience";
    public int Experience
    {
        get { return PlayerPrefs.GetInt(exp); }
        set { PlayerPrefs.SetInt(exp, value); }
    }
    private const string money = "Money";
    public float Money
    {
        get { return PlayerPrefs.GetFloat(money); }
        set { PlayerPrefs.SetFloat(money, value); }
    }
    
    public const string levelState = "LevelState";
    public bool GetStateLevel(int level)
    {
        return PlayerPrefs.GetInt($"{levelState}1_{level}") == 1;
    }
    public void SetStateLevel(int level)
    {
        PlayerPrefs.SetInt($"{levelState}1_{level}", 1);
    }
}
