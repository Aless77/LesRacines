using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NextDialogue : MonoBehaviour
{
    int index = 2;
    bool canPress = true; // pour empêcher les appuis continus

    void Update()
    {
        bool rightClick = Input.GetMouseButtonDown(1);

        bool vrButtonPressed = false;
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (rightHand.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryPressed))
        {
            vrButtonPressed = secondaryPressed;
        }

        // Si la touche est pressée et qu'on a le droit de la traiter
        if ((rightClick || vrButtonPressed) && canPress && transform.childCount > 1)
        {
            canPress = false;

            if (PlayerMovement.dialogue)
            {
                Transform child = transform.GetChild(index);

                if (child != null)
                {
                    child.gameObject.SetActive(true);
                    index += 1;

                    if (transform.childCount == index)
                    {
                        index = 2;
                        PlayerMovement.dialogue = false;
                    }
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        // Réactivation après relâchement de la touche B
        if (!vrButtonPressed && !rightClick)
        {
            canPress = true;
        }
    }
}
