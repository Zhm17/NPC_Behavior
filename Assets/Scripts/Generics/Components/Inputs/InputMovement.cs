using UnityEngine;
using System.Collections;

namespace Generics
{

    [RequireComponent(typeof(PlayerKeyboardInput))]
    [RequireComponent(typeof(CharacterController))]
    public class InputMovement : MonoBehaviour
    {
        public delegate void Action();
        public static event Action OnIdle;
        public static event Action OnRunning;

        [Header("Attributes")]
        //Movement speed;
        [SerializeField]
        public float movementSpeed = 1f;

        //Orientation
        [SerializeField]
        public float rotateSpeed = 5f;


        // Direction
        private Vector3 m_moveDirection;


        [Header("3D Player Transform")]
        [SerializeField]
        private Transform m_characterTransf;

        //Components
        private CharacterController m_characterController => GetComponent<CharacterController>();
        private PlayerKeyboardInput m_input => GetComponent<PlayerKeyboardInput>();

        //Inputs
        private float _xMovement => m_input.GetHorizontalMovementInput();
        private float _zMovement => m_input.GetVerticalMovementInput();


        private void Start()
        {
            StartCoroutine(UpdateCoroutine());
        }

        // Update is called once per frame
        IEnumerator UpdateCoroutine()
        {
            while (true)
            {
                Move();

                Rotate();

                yield return null;
            }
        }

        /// <summary>
        /// Moves and animates the character
        /// </summary>
        private void Move()
        {

            GetMovement();

            if (m_moveDirection.magnitude > 0f)
            {
                m_characterController.Move(m_moveDirection * Time.deltaTime);

                if (OnRunning != null)
                    OnRunning();
            }
            else
            {
                if (OnIdle != null)
                    OnIdle();
            }
        }

        private void GetMovement()
        {
            float yStore = m_moveDirection.y;

            m_moveDirection = (transform.right * _xMovement)
                             + (transform.forward * _zMovement);

            //If necessary, clamp movement vector to magnitude of 1f;
            if (m_moveDirection.magnitude > 1f)
                m_moveDirection.Normalize();

            m_moveDirection *= movementSpeed;

            m_moveDirection.y = yStore;
            m_moveDirection.y += Physics.gravity.y * Time.deltaTime;
        }

        private void Rotate()
        {
            if (_xMovement != 0 || _zMovement != 0)
            {
                GetRotation();
            }
        }

        public void GetRotation()
        {
            Quaternion newRotation =
                Quaternion.LookRotation(new Vector3(m_moveDirection.x,
                                                    0f,
                                                    m_moveDirection.z));

            if (m_characterTransf != null)
            {
                m_characterTransf.rotation =
                    Quaternion.Slerp(m_characterTransf.rotation,
                                    newRotation,
                                    rotateSpeed * Time.deltaTime);
            }

        }

    }
}