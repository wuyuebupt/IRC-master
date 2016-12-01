#include<string>
using namespace std;


// for socket
#include <stdio.h>
#include <stdlib.h>
#include <netdb.h>
#include <netinet/in.h>
#include <errno.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <arpa/inet.h>

class IRCClassifier
{
    public:
        void Init();
	string Predict(char* imgFileName);
        void Release();
		// added by wuyue for socket
		int sockfd, portno, n;
		struct sockaddr_in serv_addr;
		struct in_addr ipv4addr;
		struct hostent *server;
		char buffer[256]; 
		// 
		string serverip;
		int serverport;

};

