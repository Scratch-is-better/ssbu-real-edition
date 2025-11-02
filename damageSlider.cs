using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class damageSlider : MonoBehaviour
{
    public enum StatType { Damage, Range, Speed, Cooldown }

    //make into menu in inspector
    [SerializeField] private StatType statToControl;

    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text valueText;

    // called when the object becomes enabled and active.
    private void OnEnable()
    {
        // Subscribe 'UpdateSliderPosition' method to the event.
        // when OnStatsLoaded is invoked method will be called.
        customAttackSave.OnStatsLoaded += UpdateSliderPosition;
    }

    // Tis is called when the object is disabled
    private void OnDisable()
    {
        // Unsubscribe and dislike to prevent errors if object is destroyed.
        customAttackSave.OnStatsLoaded -= UpdateSliderPosition;
    }

    void Awake()
    {
        if (slider == null) slider = GetComponent<Slider>();//if no slider give slider


        slider.onValueChanged.AddListener(UpdateStatValue);//add listener to check when slider is changed


        //it will call function and write to textbox
        UpdateStatValue(slider.value);

    }


    //takes float from slider
    public void UpdateStatValue(float value) //takes in the slider value
    {



        RatioAdjust();
        int intValue = (int)value; //convert to int7
        if (valueText != null) valueText.text = intValue.ToString(); //checks that there is a textbox added 
                                                                     //then writes to textbox



        switch (statToControl)//finds what slider this script is attached to 
        {
            case StatType.Damage: customAttackSave.Damage = intValue; break;
            case StatType.Range: customAttackSave.Range = intValue; break;
            case StatType.Speed: customAttackSave.Speed = intValue; break;
            case StatType.Cooldown: customAttackSave.Cooldown = intValue; break;
        }
        //finds what slider was changed and adjusts it value

    }


    // This method is called by the OnStatsLoaded event from the customAtkSave.
    // It reads the correct value from the manager and updates the slider's position.

    private void UpdateSliderPosition()
    {
        float newValue = 0;

        // Find the correct new value from the Manager
        switch (statToControl)
        {
            case StatType.Damage: newValue = customAttackSave.Damage; break;
            case StatType.Range: newValue = customAttackSave.Range; break;
            case StatType.Speed: newValue = customAttackSave.Speed; break;
            case StatType.Cooldown: newValue = customAttackSave.Cooldown; break;
        }

        // Update the slider's value and  trigger an update to the text field.
        slider.value = newValue;
    }

    void OnDestroy()
    {
        if (slider != null) slider.onValueChanged.RemoveListener(UpdateStatValue);//remove listener to avoid bugzz
    }

    public void RatioAdjust()
    {
        //speed - range  cooldown - damage

        int intValue = (int)slider.value;
        // Debug.Log(intValue);
        int a = 0;


        if (intValue > 50)
        {
            //Debug.Log(">50");

            switch (statToControl)
            {
                case StatType.Damage: // if slider being moved is damage
                                      // a = ;
                    a = 100 - customAttackSave.Damage;

                   
                    break;
                case StatType.Range:
                    a = 100 - customAttackSave.Range;

                    break;
                case StatType.Speed:
                    a = 100 - customAttackSave.Speed;

                    break;
                case StatType.Cooldown:
                    a = 100 - customAttackSave.Cooldown;

                    break;
            }

            switch (statToControl)
            {

                case StatType.Damage: // if slider being moved is damage
                    /*
                   ///////////////////////////////////////////////////////////////////////////////////////////////////// if (customAttackSave.Cooldown > 49)
                      */  customAttackSave.Cooldown = a;
                   // (?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?)(?'?'?))


                    //?????(?_??????????(?_??????????(?_??????????(?_??????????(?_?????))))))

                    break;
                case StatType.Range:
                   // if (customAttackSave.Speed > 49)
                        customAttackSave.Speed = a;
                    //  ?????(?_?????   

                    break;
                case StatType.Speed:
                  //  if (customAttackSave.Range > 49)
                        customAttackSave.Range = a;

                    break;
                case StatType.Cooldown:
                  //  if (customAttackSave.Damage > 49)
                        customAttackSave.Damage = a;

                    break;
            }


        }
        //  int aaaaaa = map(intValue, 22, 33, 44, 55);
        //as one approaches 100 the other should go to 0
        //
    }
}