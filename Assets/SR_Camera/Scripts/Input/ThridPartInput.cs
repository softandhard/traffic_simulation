using UnityEngine;

namespace SR
{
    
    
    /// <summary>
    /// Here is a demonstration of third-party platform input, such as cockpit and joystick
    /// </summary>
    public class ThridPartInput:BaseInput
    {
        
        //The axis information can be rewritten here
        public override float GetAxis(InputAxis axis)
        {
            switch (axis)
            {
                case InputAxis.X:
                    return Input.GetAxis("CustomX");//Custom axes can be set here
                case InputAxis.Y:
                    return Input.GetAxis("CustomY");
                case InputAxis.Z:
                    return Input.GetAxis("CustomZ");
                case InputAxis.Horizontal:
                    return Input.GetAxis("CustomHorizontal");
                case InputAxis.Vertical:
                    return Input.GetAxis("CustomVertical");
                default:
                    return 0;
            }
        }
    }
}