//This script handles reading inputs from the player and passing it on to the vehicle. We 
//separate the input code from the behaviour code so that we can easily swap controls 
//schemes or even implement and AI "controller". Works together with the VehicleMovement script

using UnityEngine;

public class PlayerInput : MonoBehaviour, IShipInput
{
    public string verticalAxisName = "Vertical";
    public string horizontalAxisName = "Horizontal";
    public string brakingKey = "Brake";

    public float Thruster { get; set; }
    public float Rudder { get; set; }
    public bool IsBraking { get; set; }

    void Update()
    {
        //If the player presses the Escape key and this is a build (not the editor), exit the game
        if (Input.GetButtonDown("Cancel") && !Application.isEditor)
            Application.Quit();

        //If a GameManager exists and the game is not active...
        if (GameManager.instance != null && !GameManager.instance.IsActiveGame())
        {
            //...set all inputs to neutral values and exit this method
            Thruster = Rudder = 0f;
            IsBraking = false;
            return;
        }

        //Get the values of the thruster, rudder, and brake from the input class
        Thruster = Input.GetAxis(verticalAxisName);
        Rudder = Input.GetAxis(horizontalAxisName);
        IsBraking = Input.GetButton(brakingKey);
    }
}
