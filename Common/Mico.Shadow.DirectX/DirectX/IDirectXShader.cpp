#include "..\pch.hpp"

#include<wchar.h>


IDirectXShader::~IDirectXShader() {
	release(shaderblob);
}

void IDirectXShaderCreate(IDirectXShader** source,LPCWSTR filename,
	LPCWSTR entrypoint, int type) {
	std::ifstream shaderfile;

	This = new IDirectXShader();

	This->filename = filename;
	This->shadertype = type;
	
	
	int function_len = lstrlen(entrypoint);

	for (int i = 0; i < function_len; i++)
		This->function.push_back((char)entrypoint[i]);

	shaderfile.open(This->filename, std::ios::binary);

	while (shaderfile.eof() == false) 
		This->shadercode.push_back((byte)shaderfile.get());

	shaderfile.close();
	This->shadercode.pop_back();
}

void IDirectXShaderDestory(IDirectXShader* source) {
	if (source == nullptr) return;
	delete source;
}

void IDirectXShaderCompile(IDirectXShader* source) 
{
	ID3DBlob* errorblob = nullptr;

	LPCSTR target;

	switch (source->shadertype)
	{
	case VERTEX: 
		target = "vs_5_0";
		break;
	case PIXEL:
		target = "ps_5_0";
		break;
	default:
		break;
	}

	D3DCompileFromFile(&source->filename[0], nullptr,
		D3D_COMPILE_STANDARD_FILE_INCLUDE, &source->function[0],
		target, D3DCOMPILE_OPTIMIZATION_LEVEL2, 0, &source->shaderblob,
		&errorblob);

	release(errorblob);
}


