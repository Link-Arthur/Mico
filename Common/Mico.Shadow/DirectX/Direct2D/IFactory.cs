﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mico.Math;

namespace Mico.Shadow.DirectX.Direct2D
{
    public partial class IFactory
    {
        IntPtr source;

        public IFactory()
        {
            IFactoryCreate(out source);
        }

        ~IFactory()
        {
            IFactoryDestory(source);
        }

        public Vector2 GetDesktopDpi()
        {
            IFactoryGetDesktopDpi(source, out float dpiX, out float dpiY);
            return new Vector2(dpiX, dpiY);
        }




        public static implicit operator IntPtr(IFactory factory)
        {
            return factory.source;
        }
    }
}