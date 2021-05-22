using snake3D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace snake3D
{
    public class UI : MonoBehaviour
    {
        [Header("pause menu")]
        public GameObject Crash_Pause;
        [Header("Text Button Crash / Pause")]
        public Text TextButtonPlay_rePlay;
        [Header("main text menu crash-pause")]
        public Text TextPauseMenu;
        public Text PointsRealTime;
        public Text Points;
        public Control SnakeControl;
        [Header("Animations when press game a proceed")]
        public List<Animation> animationsOn;
        [Header("Animations when press game a pause")]
        public List<Animation> animationsOff;
        public AudioClip[] clips;
        private AudioSource audio;

        private bool ProceedOrReplay; // true is Proceed, false is Peplay

        

        void Start()
        {
            ProceedOrReplay = false;
            audio = gameObject.GetComponent<AudioSource>();

            Camera.main.gameObject.GetComponent<AudioListener>().enabled = (PlayerPrefs.GetInt("sound") == 1) ? true : false;
        }
        IEnumerator ShowPauseMenu()
        {
                yield return new WaitForSeconds(0.8f);
                ProceedOrReplay = false;
                TextPauseMenu.text = "You Crashed";
                TextButtonPlay_rePlay.text = "REPLAY";
                SnakeControl.StartCoroutine("moveForward");
                Crash_Pause.SetActive(false);
        }
        public void ButtonPause()
        {
            ProceedOrReplay = true;
            SnakeControl.StopAllCoroutines();
            TextPauseMenu.text = "PAUSE";
            TextButtonPlay_rePlay.text = "PROCEED";
            Points.text = PointsRealTime.text;
            foreach (Animation anim in animationsOn)
            {
                anim[anim.gameObject.name].speed = -1;
                anim[anim.gameObject.name].time = anim[anim.gameObject.name].length;
                anim.Play();
                audio.clip = clips[0];
                audio.Play();
            }
            foreach (Animation anim in animationsOff)
            {
                anim[anim.gameObject.name].speed = 1f;
                //anim[anim.gameObject.name].time = anim[anim.gameObject.name].length;
            }
            Crash_Pause.SetActive(true);
        }

        public void Proceed_rePlay()
        {
            if (ProceedOrReplay) // proceed
            {
                audio.clip = clips[0];
                audio.Play();
                foreach (Animation anim in animationsOff)
                {
                    anim[anim.gameObject.name].speed = -1f;
                    anim[anim.gameObject.name].time = anim[anim.gameObject.name].length;
                    anim.Play();
                }
                StartCoroutine(ShowPauseMenu());
                foreach (Animation anim in animationsOn)
                {
                    anim[anim.gameObject.name].speed = 1;
                    //anim[anim.gameObject.name].time = anim[anim.gameObject.name].length;
                    anim.Play();
                }

                

            }
            else // Replay
            {
                Crash_Pause.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        public void Crashed()
        {
            Points.text = PointsRealTime.text;
            
            Crash_Pause.SetActive(true);
            foreach (Animation anim in animationsOn)
            {
                anim[anim.gameObject.name].speed = -1;
                anim[anim.gameObject.name].time = anim[anim.gameObject.name].length;
                anim.Play();
            }
            foreach (Animation anim in animationsOff)
            {
                anim[anim.gameObject.name].speed = 1f;
                //anim[anim.gameObject.name].time = anim[anim.gameObject.name].length;
                anim.Play();
            }

        }

    }
}