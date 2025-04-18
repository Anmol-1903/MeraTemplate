using System.Collections;
using InnovateLabs;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
namespace BlackOut
{
    public class FadeInTeleportFadeout : Interaction
    {

        [SerializeField] bool automaticNextInteraction = false;
        private Canvas canvas;
        public float fadeInDuration = 1.5f;
        public float fadeOutDuration = 1.5f;
        private Image blackImage;
        private GameObject player;
        public Transform EndPosition;
        public bool copyRotation;
        public override bool isInteractionCompleted { get; set; }

        void Awake()
        {
            player = FindFirstObjectByType<XROrigin>().gameObject;
            canvas = player.transform.GetChildWithName("BlackOutCanvas").GetComponent<Canvas>();
            blackImage = canvas.transform.GetChildWithName("Image").GetComponent<Image>();
        }

        public override void InteractionCompleted()
        {
            locomotion.SetActive(true);
            //Destroy(canvas);
        }

        GameObject locomotion;
        public override void StartInteraction()
        {
            StartCoroutine(FadeIn());
        }

        IEnumerator FadeIn()
        {
            float elapsedTime = 0;
            locomotion = player.GetComponentInChildren<LocomotionMediator>().gameObject;
            locomotion.SetActive(false);

            while (elapsedTime < fadeInDuration)
            {
                blackImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, elapsedTime / fadeInDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            blackImage.color = Color.black;
            //yield return new WaitForSecondsRealtime(0.5f);
            player.transform.position = EndPosition.position;
            
            if (copyRotation)
            {
                float cameraLocalYRotation = Camera.main.transform.localEulerAngles.y;
                float endPositionRotation = EndPosition.rotation.eulerAngles.y;
                float rotationDifference = Mathf.DeltaAngle(cameraLocalYRotation, endPositionRotation);

                player.transform.rotation = Quaternion.Euler(0, rotationDifference, 0);
            }
            StartCoroutine(FadeOut());
        }

        IEnumerator FadeOut()
        {

            float elapsedTime = 0;
            while (elapsedTime < fadeOutDuration)
            {
                blackImage.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            blackImage.color = new Color(0, 0, 0, 0);
            isInteractionCompleted = true;
            if (automaticNextInteraction)
            {
                StepManager.instance?.MoveToNextInteraction();
            }
        }
    }
}