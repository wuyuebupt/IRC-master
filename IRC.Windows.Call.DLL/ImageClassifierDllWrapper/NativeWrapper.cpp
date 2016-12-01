#include <string>
#include <cstring>
#include "../ImageClassifierLib/IRCClassifier.h"
using namespace std;

#define DllExport   __declspec( dllexport )

extern "C" {

	DllExport void * Init()
	{
		IRCClassifier * hobj = new IRCClassifier();
		hobj->Init();
		return (void *)hobj;
	}

	DllExport void Predict(void* hobj_, char* imgFileName, char* predRes, int szPredRes)
	{
		IRCClassifier * hobj = (IRCClassifier *)hobj_;
		string results = hobj->Predict(imgFileName);
		strcpy_s(predRes, szPredRes, results.c_str());
	}

	DllExport void Release(void * hobj_)
	{
		IRCClassifier * hobj = (IRCClassifier *)hobj_;
		hobj->Release();
		delete hobj;
	}

}


