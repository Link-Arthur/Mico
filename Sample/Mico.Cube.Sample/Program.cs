﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Mico.Cube.Sample
{
    class Program
    {
        public static TransformMatrix matrix = new TransformMatrix();
        public static Mico.DirectX.ConstBuffer MatrixBuffer = new Mico.DirectX.ConstBuffer(matrix);

        static void Main(string[] args)
        {
            Window window = new Window();
            window.Run();
        }
    }
}