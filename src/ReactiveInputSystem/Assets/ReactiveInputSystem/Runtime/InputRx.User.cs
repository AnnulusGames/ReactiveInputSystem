using System;
using System.Threading;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.LowLevel;
using R3;

namespace ReactiveInputSystem
{
    public static partial class InputRx
    {
        public static Observable<(InputUser, InputUserChange, InputDevice)> OnUserChange(CancellationToken cancellationToken = default)
        {
            return Observable.FromEvent<Action<InputUser, InputUserChange, InputDevice>, (InputUser, InputUserChange, InputDevice)>(
                h => (x, y, z) => h((x, y, z)),
                h => InputUser.onChange += h,
                h => InputUser.onChange -= h,
                cancellationToken
            );
        }

        public static Observable<(InputControl, InputEventPtr)> OnUnpairedDeviceUsed(CancellationToken cancellationToken = default)
        {
            return Observable.FromEvent<Action<InputControl, InputEventPtr>, (InputControl, InputEventPtr)>(
                h => (x, y) => h((x, y)),
                h => InputUser.onUnpairedDeviceUsed += h,
                h => InputUser.onUnpairedDeviceUsed -= h,
                cancellationToken
            );
        }
    }
}