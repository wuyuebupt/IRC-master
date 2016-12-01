#include <cstring>
#include <string>
#include <iostream>
#include "IRCClassifier.h"
using namespace std;

void IRCClassifier::Init()
{
    cout << "Init model..." <<endl;
}

string IRCClassifier::Predict(char* imgFileName)
{
    cout << "Predicting... :"  << imgFileName << endl;
    string results = "tag1:0.95;tag2:0.32;tag3:0.05;tag4:0.04;tag5:0.01";
    return results;
}

void IRCClassifier::Release()
{
    cout << "Clean up..." <<  endl;
}

    
