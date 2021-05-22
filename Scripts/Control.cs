using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace snake3D
{
    public class Control : MonoBehaviour
    {
        [Header("spead delay rotation camera")]
        public float speadRotation;
        [Header("spead delay move camera")]
        public float speadMove;
        public Transform cameraContainer;
        public List<Transform> segments;
        public Transform mirror;
        public bool isCrash;
        float timeDelay;
        private Transform mainCamera;
        float distancing;
        [SerializeField]
        Button[] buttons_of_Control;
        public Material DefauldMaterial;
        public Material FadeMaterial;
        RaycastHit[] hits = null;
        public Transform pointerCamera;

        private void Start()
        {
            timeDelay = PlayerPrefs.GetFloat("timeDelay");
            timeDelay = (timeDelay != 0) ? timeDelay : 1f;
            StartCoroutine(moveForward());
            distancing = 1f;
            mainCamera = cameraContainer.GetChild(0).transform;

        }

        public void StopMove()
        {
            StopAllCoroutines();
            isCrash = false;
        }
        // Update is called once per frame
        void Update()
        {
            if (!isCrash)
            {
                cameraContainer.rotation = Quaternion.Lerp(cameraContainer.rotation, transform.rotation, Time.deltaTime * speadRotation);
                cameraContainer.position = Vector3.Lerp(cameraContainer.position, transform.position, Time.deltaTime * speadMove);
            }
            else
            {
                cameraContainer.position = Vector3.Lerp(cameraContainer.position, Vector3.zero, Time.deltaTime * speadMove);
                distancing = Mathf.Lerp(distancing, 0, Time.deltaTime * 6);
                mainCamera.Translate(-Vector3.forward * distancing, Space.Self);
            }
        }

        public IEnumerator moveForward()
        {
            RaycastHit pointhit;
            while (true)
            {
                yield return new WaitForSeconds(timeDelay);

                for (int i = segments.Count - 1; i > 0; i--)
                    segments[i].position = segments[(i - 1)].position;


                //  return material hidden sigments {{ 
                if (hits != null)
                    foreach (RaycastHit hit in hits)
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material = DefauldMaterial; // }}

                segments[0].position = gameObject.transform.position;
                gameObject.transform.Translate(Vector3.forward);

                //  ray for Teleport {{
                if (Physics.Raycast(transform.position, transform.forward, out pointhit, Mathf.Infinity, (1 << 12)))
                {
                    mirror.position = pointhit.point;
                    mirror.rotation = transform.rotation;
                } // }}

                foreach (Button b in buttons_of_Control)
                    b.interactable = true;

                // Hidden segments between camera and snake head {{
                hits = Physics.RaycastAll(pointerCamera.position, pointerCamera.forward, 30f, (1 << 13));
                foreach (RaycastHit hit in hits)
                    hit.collider.gameObject.GetComponent<MeshRenderer>().material = FadeMaterial; // }}


            }
        }
        public void Turn(int kay)
        {
            switch (kay)
            {
                case 1:
                    transform.Rotate(Vector3.right, 90); // up
                    buttons_of_Control[0].interactable = false;
                    break;
                case 2:
                    transform.Rotate(Vector3.left, 90); // down
                    buttons_of_Control[1].interactable = false;
                    break;
                
                case 3:
                    transform.Rotate(Vector3.down, 90); // left
                    buttons_of_Control[2].interactable = false;
                    break;
                case 4:
                    transform.Rotate(Vector3.up, 90); // right
                    buttons_of_Control[3].interactable = false;
                    break;
            }
        }
        
    }
}
