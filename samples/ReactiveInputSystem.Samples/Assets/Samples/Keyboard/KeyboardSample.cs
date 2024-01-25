using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using R3;
using ReactiveInputSystem;

public class KeyboardSample : MonoBehaviour
{
    [SerializeField] TMP_Text inputText;

    void Start()
    {
        InputRx.OnKeyDown(Key.Space, destroyCancellationToken)
            .Subscribe(_ =>
            {
                Debug.Log("Hello!");
            });

        InputRx.OnAnyKeyDown(destroyCancellationToken)
            .Subscribe(x =>
            {
                inputText.text = "Input: " + x.ToString();
            });
    }
}
