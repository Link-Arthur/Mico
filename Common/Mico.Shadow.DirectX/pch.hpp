#pragma once

#include<d2d1_3.h>
#include<d3d11_3.h>
#include<dwrite_3.h>
#include<wincodec.h>


#pragma comment(lib,"d2d1.lib")
#pragma comment(lib,"d3d11.lib")
#pragma comment(lib,"dwrite.lib")
#pragma comment(lib,"windowscodecs.lib")

#define This (*source)

template<typename Interface>
void release(Interface &T) {
	if (T == nullptr) return;
	T->Release();
	T = nullptr;
}

class IDirectXBrush {
public:
	ID2D1Brush* source;

	~IDirectXBrush();
};

class IDirectXFont {
public:
	IDWriteTextFormat* source;

	~IDirectXFont();
};

class IDirectXBitmap {
public:
	ID2D1Bitmap* source;

	~IDirectXBitmap();
};

class IDirectXDevice {
public:

	//factory
	ID2D1Factory1* d2d1_factory;
	IDWriteFactory* write_factory;
	IWICImagingFactory* image_factory;

	//d2d1
	ID2D1Device* device2d;
	ID2D1DeviceContext* context2d;

	//d3d11
	ID3D11Device* device3d;
	ID3D11DeviceContext* context3d;
	ID3D11RenderTargetView* targetview;
	ID3D11DepthStencilView* depthview;

	//dxgi
	IDXGISwapChain* chain;

	//dpi and feature
	float dpiX;
	float dpiY;

	D3D_FEATURE_LEVEL feature;
	UINT MSAA4xQuality;
	

	~IDirectXDevice();
};