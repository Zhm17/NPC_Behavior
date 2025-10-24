using System.Collections;
using UnityEngine;

namespace Generics {

    [RequireComponent(typeof(SphereCollider))]
    public class CharacterDetector : MonoBehaviour
    {
        public delegate void CharacterDetectionAction();
        public static event CharacterDetectionAction OnCharacterDetected;
        public static event CharacterDetectionAction OnCharacterOutOfSight;
        public static event CharacterDetectionAction OnCharacterIsClose;

        [Header("Min Distance to Player")]
        [SerializeField] private float m_minDistanceToCharacter = 5f;
        public float MinDistanceCharacter => m_minDistanceToCharacter;

        [Header("Max Distance to Player")]
        [SerializeField] private float m_maxDistanceToCharacter = 100f;
        public float MaxDistanceCharacter => m_maxDistanceToCharacter;


        private Collider m_collider;
        public Collider Collider
        {
            get { return m_collider; }
            private set { m_collider = value; }
        }

        private CharacterController m_characterController;
        public CharacterController Character
        {
            get { return m_characterController; }
            private set { m_characterController = value; }
        }


        private void Awake()
        {
            Collider = GetComponent<Collider>();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        //private void Init()
        //{
        //    StopAllCoroutines();
        //    Collider.enabled = true;
        //}

        //IEnumerator ResetCoroutine()
        //{
        //    yield return new WaitForSeconds(2f);

        //    Init();
        //}

        IEnumerator CheckDistanceCoroutine()
        {
            while (true)
            {
                if (IsTargetOnSight(Character.transform))
                {
                    IsTargetClose(Character.transform);
                }
                yield return null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterController character))
            {
                OnSight(character);
            }
        }

        

        private float GetDistance(Transform target)
        {
            float distance = Vector3.Distance(transform.position,
                                    target.position);

            Debug.Log("GuardAI :: GetDistance() : " + distance);

            return distance;
        }

        public bool IsTargetOnSight(Transform target)
        {
            if (target == null) return false;

            if (GetDistance(target) < MaxDistanceCharacter)
                return true;

            StopAllCoroutines();
            OnOutOfSight();

            return false;
        }

        public bool IsTargetClose(Transform target)
        {
            if (target == null ||
                GetDistance(target) > MinDistanceCharacter)
                return false;

            StopAllCoroutines();

            if (OnCharacterIsClose != null)
                OnCharacterIsClose();

            //StartCoroutine(ResetCoroutine());

            return true;
        }

        private void OnSight(CharacterController character)
        {
            Character = character;

            Debug.Log("CharacterDetector :: Character detected !!");

            if (OnCharacterDetected != null)
                OnCharacterDetected();

            //Collider.enabled = false;

            StartCoroutine(CheckDistanceCoroutine());
        }

        private void OnOutOfSight()
        {
            Character = null;

            Debug.Log("CharacterDetector :: Character  !!");

            if (OnCharacterOutOfSight != null)
                OnCharacterOutOfSight();

            //Collider.enabled = true;
        }


    }
}
