using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine.EventSystems;

namespace snake3D
{
    namespace GUI
    {
        public class MainMenu : MonoBehaviour
        {
            public Text record;
            public Slider complexity;
            public Button buttonPlay;
            public Text levelcomplaxity;
            
            public Sprite AudioOn;
            public Sprite AudioOff;
            public Sprite VibrOn;
            public Sprite VibrOff;
            float timeDelay;
            float sliderValue;
            // Start is called before the first frame update
            void Start()
            {
                timeDelay = PlayerPrefs.GetFloat("timeDelay");
                complexity.value = PlayerPrefs.GetFloat("sliderValue");
                if (PlayerPrefs.GetInt("record") != 0) 
                record.text += " " + PlayerPrefs.GetInt("record");
                GameObject.Find("AudioSwith").GetComponent<Image>().sprite =
                    (PlayerPrefs.GetInt("sound") == 0) ? AudioOff : AudioOn;
                GameObject.Find("VibrationSwith").GetComponent<Image>().sprite =
                    (PlayerPrefs.GetInt("vibration") == 0) ? VibrOff : VibrOn;


            }

            public void Play()
            {
                if (timeDelay != 0)
                {
                    PlayerPrefs.SetFloat("timeDelay", timeDelay);
                    PlayerPrefs.SetFloat("sliderValue", complexity.value);
                }
                PlayerPrefs.Save();
                SceneManager.LoadScene("MainScene");
            }
            public void Sound(Image img)
            {
                int swith = PlayerPrefs.GetInt("sound");
                img.sprite = (swith == 0) ? AudioOn : AudioOff;
                PlayerPrefs.SetInt("sound", (swith == 0) ? 1 : 0);
            }
            public void Vibration(Image img)
            {
                int swith = PlayerPrefs.GetInt("vibration");
                img.sprite = (swith == 0) ? VibrOn : VibrOff;
                PlayerPrefs.SetInt("vibration", (swith == 0)? 1 : 0);
            }

            public void Complexity(Slider slider)
            {
                levelcomplaxity.text = (Mathf.Round(Mathf.Lerp(1, 5, slider.value))).ToString();
                timeDelay = (Mathf.Round(Mathf.Lerp(10, 6, slider.value))) * 0.1f;
                 
            }

        }
    }
}
