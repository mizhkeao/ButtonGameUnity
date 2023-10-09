using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Game {
    public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter> {
        // 使用filter来遍历拥有特定component的entity
        public struct Filter {
            public EntityRef entity;
            public CharacterController3D* characterController;
        }

        // The frame parameter represents the current frame on which the system runs and
        // gives access to the complete game state for that specific frame.
        // frame: 网络游戏的帧率 保存了物体位置等信息
        public override void Update (Frame f, ref Filter filter) {
            var input = *f.GetPlayerInput (0);

            if (input.Jump) {
                filter.characterController -> Jump (f);
                f.Events.LogEvent ("JUMPED");
            }

            // 第三个para是移动的vec
            filter.characterController -> Move (f, filter.entity, input.Direction.XOY);
        }
    }
}
