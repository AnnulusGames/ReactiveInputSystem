using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using R3;
using ReactiveInputSystem;

public class RebindingSample : MonoBehaviour
{
    [SerializeField] TMP_Text label;
    [SerializeField] TMP_Text currentBindingText;

    InputAction inputAction;

    const string LabelText = "Press [R] to start rebinding.";
    const string WaitingText = "Waiting for input...";

    void Start()
    {
        inputAction = new InputAction(null, InputActionType.Button, InputControlPathEx.GetControlPath(Key.Space));
        inputAction.AddTo(this);
        inputAction.Enable();

        inputAction.PerformedAsObservable()
            .Subscribe(_ => Debug.Log("Hello!"));
            
        InputRx.OnKeyDown(Key.R, destroyCancellationToken)
            .SubscribeAwait(async (_, token) => await RebindAsync(token), AwaitOperations.Drop);

        label.text = LabelText;
        currentBindingText.text = inputAction.bindings[0].path;
    }

    async UniTask RebindAsync(CancellationToken cancellationToken)
    {
        label.text = WaitingText;
        inputAction.Disable();

        var path = await InputRx.OnAnyKeyDown(cancellationToken)
            .Select(x => InputControlPathEx.GetControlPath(x))
            .FirstAsync();

        inputAction.ApplyBindingOverride(0, path);
        inputAction.Enable();

        currentBindingText.text = path;
        label.text = LabelText;
    }
}
