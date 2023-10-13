using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.ButtonGame {
    public unsafe class ButtonHandler : SystemMainThreadFilter<ButtonHandler.Filter>, ISignalOnSpawnButton {
        //public int RuntimeIndex => throw new NotImplementedException ();
        
        private FP _circleRadius = 1;
        private int _lifetime = 120;

        private FP _posXLimit = 10;
        private FP _posZLimit = 10;
        
        public struct Filter {
            public EntityRef entity;
            public IButton* button;
        }

        public override void Update (Frame f, ref Filter filter) {
            filter.button -> tick ++;
            if (filter.button -> tick > _lifetime) {
                f.Destroy (filter.entity);
            }
        }

        public void OnSpawnButton (Frame f) {
            var buttonPrototype = f.FindAsset<EntityPrototype> ("Resources/DB/Prefabs/SpawnedCircle|EntityPrototype");
            var buttonEntity = f.Create (buttonPrototype);

            var iButton = new IButton () {};
            f.Add (buttonEntity, iButton);

            if (f.Unsafe.TryGetPointer<Transform3D> (buttonEntity, out var transform)) {
                FPVector3 newPos = new FPVector3 (RandomNumber (f, _posXLimit), 0, RandomNumber (f, _posZLimit));
                while (! CheckPosValid (f, newPos)) newPos = new FPVector3 (RandomNumber (f, _posXLimit), 0, RandomNumber (f, _posZLimit));

                transform -> Position = newPos;
            }
        }

        private bool CheckPosValid (Frame f, FPVector3 pos) {
            var filtered = f.Filter<IButton> ();
            while (filtered.Next (out var entity, out var button)) {
                if (f.Unsafe.TryGetPointer<Transform3D> (entity, out var transform)) {
                    if (FPVector3.Distance (transform -> Position, pos) <= _circleRadius)
                        return false;
                }
            }

            return true;
        }

        private FP RandomNumber (Frame f, FP limit) { return limit * (f.RNG -> Next () - FP.FromFloat_UNSAFE (0.5f)); }
    }
}
