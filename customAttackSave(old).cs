
using UnityEngine;
using System; 

public class customAttackSave : MonoBehaviour
{  
    // These are 'static', meaning they belong to the class itself, not an instance.
    public static int Damage;
    public static int Range;
    public static int Speed;
    public static int Cooldown;

    // An event that other scripts can subscribe to.
    // call this event called any method subscribed to it will be executed.
    public static event Action OnStatsLoaded;

   
    public void SaveStats() //Saves the current stat values to PlayerPrefs.
    {
        PlayerPrefs.SetInt("Damage", Damage);
        PlayerPrefs.SetInt("Range", Range);
        PlayerPrefs.SetInt("Speed", Speed);
        PlayerPrefs.SetInt("Cooldown", Cooldown);

        // saves to storage
        PlayerPrefs.Save();

        Debug.Log("preset saved");
    }

    
    // Loads the stat values from PlayerPrefs.
    
    public void LoadStats()
    {
        // use GetInt to retrieve the data. The second number is the default value
        // used if the key doesn't exist yet
        Damage = PlayerPrefs.GetInt("Damage",0);
        Range = PlayerPrefs.GetInt("Range",0);
        Speed = PlayerPrefs.GetInt("Speed", 0);
        Cooldown = PlayerPrefs.GetInt("Cooldown", 0);

        Debug.Log("preset loaded");

        // invoke the event to notify  sliders that the data has been updated and they to refresh.
        // ? mean only invoke if there are listeners.
        OnStatsLoaded?.Invoke();
    }
}
