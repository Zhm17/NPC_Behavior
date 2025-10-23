using UnityEngine;

namespace Generics
{
    public class PlayerKeyboardInput : MonoBehaviour
    {
        [SerializeField]
        private string _horizontalInputAxis = "Horizontal";
        [SerializeField]
        private string _verticalInputAxis = "Vertical";

        //If this is enabled, Unity's internal input smoothing is bypassed;
        public bool useRawInput = true;

        public float GetHorizontalMovementInput()
        {
            if (useRawInput)
                return Input.GetAxisRaw(_horizontalInputAxis);
            else
                return Input.GetAxis(_horizontalInputAxis);
        }

        public float GetVerticalMovementInput()
        {
            if (useRawInput)
                return Input.GetAxisRaw(_verticalInputAxis);
            else
                return Input.GetAxis(_verticalInputAxis);
        }

    }
}
