using System;
using Photon.Deterministic;
using Quantum;
using Quantum.Game;
using UnityEngine;

public class LocalInput : MonoBehaviour {
	private Vector3 _mouseDownPos;
	private bool _mouseDown = false;

	private void OnEnable () {
		QuantumCallback.Subscribe (this, (CallbackPollInput callback) => PollInput (callback));
		QuantumEvent.Subscribe<EventLogEvent> (this, (e) => Debug.Log ($"LogEvent {e.log}"));
	}

    private void Update () {
		if (UnityEngine.Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (UnityEngine.Input.mousePosition);
			if (Physics.Raycast (ray, out RaycastHit hit)) {
				_mouseDownPos = hit.point;
				Debug.Log (_mouseDownPos);
				_mouseDown = true;
			}
		}
		else if (UnityEngine.Input.GetMouseButtonUp (0)) {
			_mouseDown = false;
		}
    }

    public void PollInput (CallbackPollInput callback) {
		Quantum.Input input = new Quantum.Input ();
		
		input.MouseDownPos = _mouseDownPos.ToFPVector3 ();
		input.MouseDown = _mouseDown;

		callback.SetInput (input, DeterministicInputFlags.Repeatable);
	}
}
