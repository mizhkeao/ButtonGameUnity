using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.ButtonGame {
    public unsafe class GameManager : SystemMainThreadFilter<GameManager.Filter> {
        public struct Filter {
            public EntityRef entity;
            public PlayerLink* player;
        }

        public override void OnEnabled (Frame f) {
            base.OnEnabled (f);

            f.Global -> tick = 0;                                                       
        }

        public override void Update (Frame f, ref Filter filter) {
            f.Global -> tick ++;

            //f.Events.LogEvent ("" + f.Global -> tick);

            if (f.Global -> tick % 60 == 0) {
                f.Signals.OnSpawnButton ();
            }

            Input input = default;
            if (f.Unsafe.TryGetPointer (filter.entity, out PlayerLink* playerLink)) {
                input = *f.GetPlayerInput (playerLink -> player);
            }
            //f.Events.LogEvent ("" + input.MouseDown);
            if (input.MouseDown) {
                //f.Events.LogEvent ("" + input.MouseDownPos);
                MouseDownHandler (f, input.MouseDownPos);
            }
        }

        private void MouseDownHandler (Frame f, FPVector3 hitPoint) {
            var hit = f.Physics3D.Raycast (hitPoint + FPVector3.Up, FPVector3.Down, 10);
            if (hit != null) {
                f.Destroy (hit.Value.Entity);
            }
        }
    }
}
