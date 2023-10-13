using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Game {
    // 此代码在玩家加入时创建角色实体，并通过向其添加 PlayerLink 组件将其链接到玩家
    public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter> {
        // 使用filter来遍历拥有特定component的entity
        public struct Filter {
            public EntityRef entity;
            public CharacterController3D* characterController;
        }

        // The frame parameter represents the current frame on which the system runs and
        // gives access to the complete game state for that specific frame.
        // frame: 帧数据 保存了物体位置等信息
        public override void Update (Frame frame, ref Filter filter) {
            /*
            Input input = default;
            if (frame.Unsafe.TryGetPointer (filter.entity, out PlayerLink* playerLink)) {
                input = *frame.GetPlayerInput (playerLink -> player);
            }

            if (input.Jump.WasPressed) {
                filter.characterController -> Jump (frame);
                //frame.Events.LogEvent ("JUMPED");
            }

            // 第三个para是移动的vec
            filter.characterController -> Move (frame, filter.entity, input.Direction.XOY);
            */
        }
    }
}
