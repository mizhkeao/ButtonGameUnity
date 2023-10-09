using System;
using Photon.Deterministic;
using Quantum;
using Quantum.Game;
using UnityEngine;

public class LocalInput : MonoBehaviour {
	private void OnEnable () {
		QuantumCallback.Subscribe (this, (CallbackPollInput callback) => PollInput (callback));
		QuantumEvent.Subscribe<EventLogEvent> (this, (e) => Debug.Log ($"LogEvent {e.log}"));
	}

	public void PollInput (CallbackPollInput callback) {
		Quantum.Input input = new Quantum.Input ();

		var x = UnityEngine.Input.GetAxisRaw ("Horizontal");
		var y = UnityEngine.Input.GetAxisRaw ("Vertical");

		input.Direction = new Vector2 (x, y).ToFPVector2 ();

		callback.SetInput (input, DeterministicInputFlags.Repeatable);
	}
}
