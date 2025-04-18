using System.Collections;
using UnityEngine;
namespace BlackOut
{
    public class GoToPositionInteraction : Interaction
    {
        [SerializeField] bool automaticNextInteraction;
        [SerializeField] bool _isAssessment;
        [SerializeField] float distance = 1f;
        public override bool isInteractionCompleted { get; set; }

        public override void StartInteraction()
        {
            StepUIManager.Instance?.ToggleConfirmButton(!automaticNextInteraction);
            if (!_isAssessment)
            {
                StartCoroutine(ShowArrows());
            }
        }
        IEnumerator ShowArrows()
        {
            yield return new WaitForSeconds(.1f);
            PathDirector.instance.showArrows = true;
            PathDirector.instance.SetEndValue(transform.position);
        }
        public override void InteractionCompleted()
        {
            PathDirector.instance?.ClearArrows();
            PathDirector.instance.showArrows = false;
        }

        void Update()
        {
            isInteractionCompleted = IsCloseToPoint(Camera.main.gameObject, transform.position);
        }


        public bool IsCloseToPoint(GameObject obj, Vector3 targetPoint)
        {
            Vector3 objPosition = obj.transform.position;
            objPosition.y = targetPoint.y;

            float distance = Vector3.Distance(objPosition, targetPoint);

            if (distance <= this.distance)
            {
                PathDirector.instance?.ClearArrows();
                Invoke(nameof(DelayedMoveToNextInteraction), 1f);
                return true;
            }
            return false;
        }

        private void DelayedMoveToNextInteraction()
        {
            if (automaticNextInteraction)
            {
                StepUIManager.Instance?.UpdateStep();
                StepManager.instance?.MoveToNextInteraction();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, distance);
        }
#endif
    }
}