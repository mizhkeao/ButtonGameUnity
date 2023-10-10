using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Quantum  {
    partial class RuntimePlayer  {
        // Add AssetRefEntityPrototype to the player data which is the Quantum equivalent to a prefab
        public AssetRefEntityPrototype _characterPrototype;

        partial void SerializeUserData(BitStream stream) {
            stream.Serialize (ref _characterPrototype);
        }
    }
}
