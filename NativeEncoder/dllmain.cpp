// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

#define EXTERN_DLL_EXPORT extern "C" __declspec(dllexport)

EXTERN_DLL_EXPORT int getEngineVersion() {
	return 1;
}


#define DllExport   __declspec( dllexport )   

extern "C" {
	EXTERN_DLL_EXPORT void b(unsigned char * toEncrypt, int len) {
		char key[9] = { '西', 'C', 'ض', '۞', 'Ұ', '۝', '۞', 'Ұ', 'Ұ' ‏‏}; 
		unsigned char * output = toEncrypt;

		for (int i = 0; i < len; i++)
			output[i] = toEncrypt[i] ^ key[i % (sizeof(key) / sizeof(char))];

		//return output;
	}
	EXTERN_DLL_EXPORT   void __stdcall a(unsigned char * data, int datalen, unsigned char key[], int keylen) {
		int N1 = 12, N2 = 14, NS = 258, I = 0;
		for (I; I < keylen; I++) NS += NS % (key[I] + 1);

		for (I = 0; I < datalen; I++)
		{
			NS = key[I % keylen] + NS;
			N1 = (NS + 5) * (N1 & 245) + (N1 >> 8);
			N2 = (NS + 7) * (N2 & 245) + (N2 >> 8);
			NS = ((N1 << 8) + N2) & 245;

			data[I] = ((data[I]) ^ NS);
		}
		b(data, datalen);

	}


}

