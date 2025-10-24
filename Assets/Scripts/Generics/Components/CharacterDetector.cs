using UnityEngine;

namespace Generics {

    [RequireComponent(typeof(SphereCollider))]
    public class CharacterDetector : MonoBehaviour
    {
        public delegate void CharacterDetectionAction();
        public static event CharacterDetectionAction OnCharacterDetected;
        public static event CharacterDetectionAction OnCharacterExit;


        private CharacterController m_characterController;
        public CharacterController Character
        {
            get { return m_characterController; }
            private set { m_characterController = value; }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterController character))
            {
                Character = character;

                Debug.Log("CharacterDetector :: Character detected !!");
                
                if(OnCharacterDetected!= null)
                    OnCharacterDetected();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CharacterController character))
            {
                Character = null;

                Debug.Log("CharacterDetector :: Character unseen !!");
                
                if(OnCharacterExit!= null)
                    OnCharacterExit();
            }
        }
    }
}
