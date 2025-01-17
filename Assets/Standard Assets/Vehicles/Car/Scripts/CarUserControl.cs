using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace UnityStandardAssets.Vehicles.Car
{

    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        public Text speedText;
        public bool raceFinished = false;
        
        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

        public void finishRace()
        {
            raceFinished = true;
        }

        private void FixedUpdate()
        {
            if (!raceFinished)
            {
                // pass the input to the car!
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                float v = CrossPlatformInputManager.GetAxis("Accelerate");
                float b = CrossPlatformInputManager.GetAxis("Brake");
#if !MOBILE_INPUT
                float handbrake = CrossPlatformInputManager.GetAxis("Jump");
                m_Car.Move(h, v, b, handbrake);
                speedText.text = m_Car.Speed.ToString();
#else
            m_Car.Move(h, v, v, 0f);
#endif
            }
        }
    }
}
