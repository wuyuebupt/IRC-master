#include <string>
#include <cstring>
#include "ImgClassifier/IRCClassifier.h"
using namespace std;
extern "C" {

    void * Init()
    {
	IRCClassifier * hobj = new IRCClassifier();
        hobj->Init();
        return (void *) hobj;
    }

   void Predict(void* hobj_,char* imgFileName, char* predRes )
   {
 	IRCClassifier * hobj = (IRCClassifier *) hobj_;
        string results = hobj->Predict(imgFileName);
        strcpy(predRes,results.c_str());
   }

   void Release(void * hobj_)
   {
        IRCClassifier * hobj = (IRCClassifier *) hobj_;
		hobj->Release();
        delete hobj;
   }

}


