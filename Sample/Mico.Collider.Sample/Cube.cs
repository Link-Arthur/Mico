﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using Mico.Math;
using Mico.Shapes;
using Mico.Objects;
using Mico.DirectX;

namespace Mico.Collider.Sample
{
    class Cube : Shape
    {
        static DirectX.Buffer vertexbuffer;
        static DirectX.Buffer indexbuffer;
        static DirectX.BufferLayout layout;

        Vector3 rotate_speed = new Vector3(1, 1, 0);
        Vector3 translation_direct = TVector3.Forward;
        float speed = 20;

        TVector4 color = new TVector4(Window.FLOAT, Window.FLOAT, Window.FLOAT, 1);

        protected override void OnUpdate(object Unknown = null)
        {
            if (Transform.Position.X >= Window.XLimit || Transform.Position.X <= -Window.XLimit)
                translation_direct.X = -translation_direct.X;
            if (Transform.Position.Y >= Window.YLimit || Transform.Position.Y <= -Window.YLimit)
                translation_direct.Y = -translation_direct.Y;


            //Update
            Transform.Rotate *= Quaternion.CreateFromYawPitchRoll(rotate_speed.Y * Time.DeltaSeconds,
                rotate_speed.X * Time.DeltaSeconds, rotate_speed.Z * Time.DeltaSeconds);

            Transform.Position += Vector3.Normalize(translation_direct) * speed * Time.DeltaSeconds;

            Collider.Center = Transform.Position;
            (Collider as BoxCollider).Rotate = Transform.Rotate;

            base.OnUpdate(Unknown);
        }

        protected override void OnCollide(Shape target)
        {
            translation_direct = Vector3.Normalize(Transform.Position - target.Transform.Position);
            color = new TVector4(Window.FLOAT, Window.FLOAT, Window.FLOAT, 1);
            base.OnCollide(target);
        }

        protected override void FixUpdate(object Unknown = null)
        {
            base.FixUpdate(Unknown);
        }

        protected override void OnExport(object Unknown = null)
        {
            Program.matrix.view = Micos.Camera;
            Program.matrix.world = Transform;
            Program.MatrixBuffer.Update(Program.matrix);
            Program.ColorBuffer.Update(color);

          

            Direct3D.SetBufferToVertexShader(Program.MatrixBuffer, 0);
            Direct3D.SetBufferToPixelShader(Program.ColorBuffer, 0);
            Direct3D.SetBufferLayout(layout);
            Direct3D.SetBuffer(vertexbuffer);
            Direct3D.SetBuffer(indexbuffer);
            Direct3D.DrawIndexed(36, 0);
            base.OnExport(Unknown);
        }

        static Cube()
        { 
            BufferLayout.Element[] element = new BufferLayout.Element[2];

            element[0] = new BufferLayout.Element()
            {
                Size = BufferLayout.ElementSize.eFloat3,
                Tag = "POSITION"
            };
            element[1] = new BufferLayout.Element()
            {
                Size = BufferLayout.ElementSize.eFlaot4,
                Tag = "COLOR"
            };

            layout = new BufferLayout(element);

            uint[] index = new uint[36] {
                0,1,2,0,2,3,4,6,5,4,7,6,
                4,5,1,4,1,0,3,2,6,3,6,7,
                1,5,6,1,6,2,4,0,3,4,3,7
            };

            Vertex[] vertex = new Vertex[8];

            float halfwidth = 0.5f;
            float halfheight = 0.5f;
            float halfdepth = 0.5f;

            vertex[0] = new Vertex(-halfwidth, -halfheight, -halfdepth);
            vertex[1] = new Vertex(-halfwidth, halfheight, -halfdepth);
            vertex[2] = new Vertex(halfwidth, halfheight, -halfdepth);
            vertex[3] = new Vertex(halfwidth, -halfheight, -halfdepth);
            vertex[4] = new Vertex(-halfwidth, -halfheight, halfdepth);
            vertex[5] = new Vertex(-halfwidth, halfheight, halfdepth);
            vertex[6] = new Vertex(halfwidth, halfheight, halfdepth);
            vertex[7] = new Vertex(halfwidth, -halfheight, halfdepth);

            vertexbuffer = new VertexBuffer(vertex, vertex.Length, 28);

            indexbuffer = new IndexBuffer(index);


        }

        public Cube(float width, float height, float depth)
        {

            Transform.Scale = new Vector3(width, height, depth);

            Collider = new BoxCollider(new System.Numerics.Vector3(0, 0, 0),
                new System.Numerics.Vector3(width / 2.0f, height / 2.0f, depth / 2.0f));
        }


        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public Vector3 RotateSpeed
        {
            get => rotate_speed;
            set => rotate_speed = value;
        }

        public Vector3 Forward
        {
            get => translation_direct;
            set => translation_direct = value;
        }


    }
}
