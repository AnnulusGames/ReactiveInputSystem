using UnityEngine;
using UnityEngine.InputSystem;
using R3;
using ReactiveInputSystem;

namespace Sandbox
{
    public class InputActionSample : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float speed = 5f;

        Vector2 velocity;

        void Start()
        {
            var input = new InputAction();
            input.AddTo(this);

            input.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Left", "<Keyboard>/a")
                .With("Down", "<Keyboard>/s")
                .With("Right", "<Keyboard>/d");
            input.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/upArrow")
                .With("Left", "<Keyboard>/leftArrow")
                .With("Down", "<Keyboard>/downArrow")
                .With("Right", "<Keyboard>/rightArrow");
            
            input.Enable();

            input.PerformedAsObservable(destroyCancellationToken)
                .Merge(input.StartedAsObservable(destroyCancellationToken))
                .Merge(input.CanceledAsObservable(destroyCancellationToken))
                .Subscribe(x =>
                {
                    velocity = x.ReadValue<Vector2>();
                });

            Observable.EveryUpdate(destroyCancellationToken)
                .Subscribe(x =>
                {
                    target.transform.position += speed * Time.deltaTime * (Vector3)velocity;
                });
        }
    }
}