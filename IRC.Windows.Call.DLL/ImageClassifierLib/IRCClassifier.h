#include<string>
using namespace std;

class IRCClassifier
{
    public:
        void Init();
		string Predict(char* imgFileName);
        void Release();
};
