using UnityEngine;
using UnityEngine.UI;
using R3;
using ReactiveInputSystem;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class TouchSample : MonoBehaviour
{
    [SerializeField] Image[] touchImages;

    void Start()
    {
        for (int i = 0; i < touchImages.Length; i++)
        {
            InputRx.OnTouchPhaseChanged(i, destroyCancellationToken)
                .Subscribe(touchImages[i], (x, image) => image.enabled = x is TouchPhase.Moved or TouchPhase.Stationary or TouchPhase.Began);

            InputRx.OnTouchPositionChanged(i, destroyCancellationToken)
                .Subscribe(touchImages[i], (x, image) => image.rectTransform.position = x);
        }
    }
}
