﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using Mico.Math;
using Mico.Shapes;
using Mico.Objects;

using Builder;
using Presenter;

namespace Mico.Cube.Sample
{
    public class Window : GenericWindow
    {
        private Surface surface;
        private Shader vertexshader;
        private Shader pixelshader;

        private GraphicsPipelineState pipelinestate;
        private BufferLayout bufferlayout;
        private ResourceLayout resourcelayout;

        private FpsCounter fps;

        public Window((string Title, int Width, int Height) Definition) : base(Definition)
        {
            surface = new Surface(Handle, true);

            vertexshader = new VertexShader(@"..\..\Sample\Mico.Cube.Sample\shader.hlsl", "VSmain");
            pixelshader = new PixelShader(@"..\..\Sample\Mico.Cube.Sample\shader.hlsl", "PSmain");
           

            BufferLayout.Element[] bufferElements
                = new BufferLayout.Element[] {
                    new BufferLayout.Element(){ Tag = "POSITION", Size = ElementSize.eFloat3 },
                    new BufferLayout.Element(){ Tag = "COLOR",    Size = ElementSize.eFlaot4 },
                    new BufferLayout.Element(){ Tag = "TEXCOORD", Size = ElementSize.eFloat2 }
                };

            ResourceLayout.Element[] resouceElements
                = new ResourceLayout.Element[]
                {
                    new ResourceLayout.Element(ResourceType.ConstantBufferView, 0),
                    new ResourceLayout.Element(null,new ResourceLayout.SubResource(0,1))
                };
        
            bufferlayout = new BufferLayout(bufferElements);

            resourcelayout = new ResourceLayout(resouceElements, 1);

            pipelinestate = new GraphicsPipelineState(vertexshader as VertexShader,
                pixelshader as PixelShader, bufferlayout, resourcelayout);
            
            Manager.Surface = surface;

            Manager.GraphicsPipelineState = pipelinestate;

            Micos.Camera = new Camera();

            Micos.Camera.Transform.Position = new Vector3(0, 0, -10);
            Micos.Camera.Transform.Forward = Vector3.Zero - Micos.Camera.Transform.Position;

            Micos.Camera.Project =
                 TMatrix.CreatePerspectiveFieldOfViewLH(
                     (float)System.Math.PI * 0.55f, 800.0f / 600.0f, 1.0f, 2000.0f);

            Micos.Add(new Cube(3, 3, 3));

            Micos.Add(fps = new FpsCounter());

            Show();
        }

        public override void OnDestroyed(object sender)
        {
            base.OnDestroyed(sender);
        }

        public override void OnUpdate(object sender)
        {
            Manager.ClearObject();

            Micos.Exports();

            SetTitle(Program.AppTitle + " FPS: " + fps.Fps);

            Manager.FlushObject();

            Micos.Update();
        }
    }
}
