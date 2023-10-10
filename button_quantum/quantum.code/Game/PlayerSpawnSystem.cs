using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Game {
    // Signals are similar to events in C#
    public unsafe class PlayerSpawnSystem : SystemSignalsOnly, ISignalOnPlayerDataSet {
        public void OnPlayerDataSet (Frame frame, PlayerRef player) {
            var playerData = frame.GetPlayerData (player);

            var playerPrototype = frame.FindAsset<EntityPrototype> (playerData._characterPrototype.Id);

            var entity = frame.Create (playerPrototype);

            var playerLink = new PlayerLink () {
                player = player,
            };
            frame.Add (entity, playerLink);

            if (frame.Unsafe.TryGetPointer<Transform3D> (entity, out var transform)) {
                transform -> Position.X = 0 + player;
            }
        }
    }
}
