using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.ButtonGame {
    public unsafe class ButtonHandler : SystemSignalsOnly, ISignalOnSpawnButton {
        //public int RuntimeIndex => throw new NotImplementedException ();

        public void OnSpawnButton (Frame f) {
            var buttonPrototype = f.FindAsset<EntityPrototype> ("Resources/DB/Prefabs/SpawnedCircle|EntityPrototype");
            var buttonEntity = f.Create (buttonPrototype);

            var iButton = new IButton () {};
            f.Add (buttonEntity, iButton);

            if (f.Unsafe.TryGetPointer<Transform3D> (buttonEntity, out var transform)) {
                transform -> Position = new FPVector3 (0, 0, 0);
            }
        }
    }
}
