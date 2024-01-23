using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] TMP_Dropdown languageDropDown;
    [SerializeField] GameObject[] joysticks;
    [SerializeField] TextMeshProUGUI[] joystickTexts;
 
    private void Awake()
    {
        if (PlayerPrefs.GetString("language") == "ENG" || PlayerPrefs.GetString("language") == "")
        {
            languageDropDown.value = 1;
        }

        ActiveJoystick(PlayerPrefs.GetString("ActiveJoystick"));
    }

    void CheckTextLanguage(string language)
    {
        if (language == "TR")
        {
            joystickTexts[0].text = "Sol";
            joystickTexts[1].text = "Sað";
        }
        else
        {
            joystickTexts[0].text = "Left";
            joystickTexts[1].text = "Right";
        }
    }

    public void OnValueChange()
    {
        if (languageDropDown.value == 0)
        {
            PlayerPrefs.SetString("language", "TR");
            CheckTextLanguage("TR");
        }
        else 
        {
            PlayerPrefs.SetString("language", "ENG");
            CheckTextLanguage("ENG");
        }

    }

    public void ActiveJoystick(string activeJoystick)
    {
        if (activeJoystick == "Left")
        {
            JoystickColor(0, 1f);
            JoystickColor(1, 0.1f);

            PlayerPrefs.SetString("ActiveJoystick", "Left");
        }
        else if (activeJoystick == "Right")
        {
            JoystickColor(1, 1f);
            JoystickColor(0, 0.1f);

            PlayerPrefs.SetString("ActiveJoystick", "Right");
        }
    }

    void JoystickColor(int index,float alpha)
    {
        joysticks[index].GetComponent<Image>().color = new Color(255, 255, 255, alpha);
        joysticks[index].transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, alpha);
    }
}
