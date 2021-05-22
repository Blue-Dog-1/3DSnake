using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace snake3D {
    public class HeadCollisionHandling : MonoBehaviour
    {
        public Control controlSnake;
        public UI snakeUI;
        public Text Points;
        public GameObject Food;
        public List<Color> colorFood;
        public Light LightFood;
        private int namberTypeFood;
        private string PointsText;
        public AudioClip[] clips;
        public AudioSource audio;

        // Start is called before the first frame update
        void Start()
        {
            PointsText = Points.text;
            PointsRealTime();
            namberTypeFood = Random.Range(-1, 3);
            namberTypeFood = Mathf.Clamp(namberTypeFood, 0, 2);
            Food.GetComponent<MeshRenderer>().material.color = colorFood[namberTypeFood];
            LightFood.color = colorFood[namberTypeFood];
        }


        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 8) // if collision food
            {
                ToEat(namberTypeFood);
                namberTypeFood = Random.Range(-1, 3);
                namberTypeFood = Mathf.Clamp(namberTypeFood, 0, 2);
                other.GetComponent<MeshRenderer>().material.color = colorFood[namberTypeFood];
                LightFood.color = colorFood[namberTypeFood];
                audio.clip = clips[0];
                audio.Play();
                return;
            }
            if (other.gameObject.layer == 12) // if collision wolls then teleport oposaide woll
            {
                gameObject.transform.Translate(Vector3.back * 10);
                audio.clip = clips[2];
                audio.Play();
                return;
            }
            crash();
        }
        public void ToEat(int i)
        {
            while (i >= 0)
            {
                Transform parent = controlSnake.segments[controlSnake.segments.Count - 1];
                GameObject newSegment = Instantiate(parent.gameObject, parent.position, parent.rotation);
                controlSnake.segments.Add(newSegment.transform);
                PointsRealTime();
                i--;
            }
        }
        private void crash()
        {
            
            foreach(Transform item in controlSnake.segments)
                item.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Collider>().isTrigger = false;
            controlSnake.StopAllCoroutines();
            if (PlayerPrefs.GetInt("vibration") == 1)
            Handheld.Vibrate();
            snakeUI.Crashed();
            if (PlayerPrefs.GetInt("record") < controlSnake.segments.Count)
            PlayerPrefs.SetInt("record", controlSnake.segments.Count);
            controlSnake.isCrash = true;
            audio.clip = clips[1];
            audio.Play();
        }
        public void PointsRealTime()
        {
            Points.text = PointsText + " " + controlSnake.segments.Count;
            Points.gameObject.transform.parent.gameObject.GetComponent<Image>().fillAmount = controlSnake.segments.Count * 0.001f;
        }
        


    }
}
