using System.Collections;
using UnityEngine;
using UnityEngine.UI;   

public class AbilityInput : MonoBehaviour
{
    [SerializeField] private KeyCode _key = KeyCode.Q;
    [SerializeField] private Button _button;

    private void Update()
    {
        if (Input.GetKeyDown(_key) && _button.interactable)
        {
            _button.onClick.Invoke();
        }
    }
}
