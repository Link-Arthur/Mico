﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


using Mico.Math;
using Mico.Shapes;
using Mico.World;
using Mico.Shadow.DirectX;

namespace Mico.Diep.Sample.GameObject
{
    class TankSource : Shape
    {
        bool g_isplaer = false;

        float g_speed = 0;

        public TankSource()
        {

        }


        public override void OnUpdate(object Unknown = null)
        {
            if (IsPlayer is true)
            {
                Transform.Forward = new Vector3(GameInput.Input.MousePos, 0) - Transform.Position;

                Vector3 off = new Vector3(0, 0, 0);
                if (GameInput.Input.IsKeyDown(ConsoleKey.W) is true) off.Y -= 1.0f;
                if (GameInput.Input.IsKeyDown(ConsoleKey.A) is true) off.X -= 1.0f;
                if (GameInput.Input.IsKeyDown(ConsoleKey.D) is true) off.X += 1.0f;
                if (GameInput.Input.IsKeyDown(ConsoleKey.S) is true) off.Y += 1.0f;


                Transform.Position +=
                    Speed * off * (float)Time.DeltaTime.TotalSeconds;
            }

            base.OnUpdate(Unknown);
        }


        public float Speed
        {
            get => g_speed;
            set => g_speed = value;
        }

        public bool IsPlayer
        {
            get => g_isplaer;
            set => g_isplaer = true;
        }
    }
}
