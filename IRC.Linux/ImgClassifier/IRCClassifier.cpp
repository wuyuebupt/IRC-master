#include <cstring>
#include <string>
#include <iostream>
#include "IRCClassifier.h"
using namespace std;


#include <stdio.h>
#include <errno.h>
#include <netdb.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <err.h>
void IRCClassifier::Init()
{
    cout << "Init model..." <<endl;
	serverip = "127.0.0.1";
	// serverip = "155.33.199.0";
	serverport = 9999;
	// serverport = 9003;
	portno = serverport;
}

string IRCClassifier::Predict(char* imgFileName)
{
    cout << "Predicting... :"  << imgFileName << endl;
	
	sockfd = socket(AF_INET, SOCK_STREAM, 0);
	// sockfd = socket(AF_, SOCK_STREAM, 0);
	if (sockfd < 0) {
		perror("ERROR opening socket");
		exit(1);					      
	}
	// inet_pton(AF_INET, serverip.c_str(), &ipv4addr);
	// server = gethostbyaddr(&ipv4addr, sizeof ipv4addr, AF_INET);
	//if (server == NULL) {
	//	fprintf(stderr,"ERROR, no such host\n");
	//	exit(0);
	//}

	if (!inet_aton(serverip.c_str(), &ipv4addr))
		        errx(1, "can't parse IP address %s", serverip.c_str());
	if ((server = gethostbyaddr((const void *)&ipv4addr, sizeof ipv4addr, AF_INET)) == NULL)
		        errx(1, "no name associated with %s", serverip.c_str());

	printf("name associated with %s is %s\n", serverip.c_str(), server->h_name);

	bzero((char *) &serv_addr, sizeof(serv_addr));
	serv_addr.sin_family = AF_INET;
	bcopy((char *)server->h_addr, (char *)&serv_addr.sin_addr.s_addr, server->h_length);
	serv_addr.sin_port = htons(portno);


	// Now connect to the server
	if (connect(sockfd, (struct sockaddr*)&serv_addr, sizeof(serv_addr)) < 0) {
		perror("ERROR connecting");
		exit(1);
	}
    // Send message to the server
	// bzero(buffer,256);

	cout << strlen(imgFileName) << endl;
	n = write(sockfd, imgFileName, strlen(imgFileName)+1);

	if (n < 0) {
		perror("ERROR writing to socket");
		exit(1);
	}
	cout << n << endl;

	/* Now read server response */
	bzero(buffer,256);
	n = read(sockfd, buffer, 256);
	cout << n << endl;
	
	if (n < 0) {
		perror("ERROR reading from socket");
		exit(1);
	}
	
	printf("%s\n",buffer);
	close(sockfd);

	// string results = "dev:444;tag3:0.32;tag3:0.05;tag4:0.04;tag5:123";
    string results = buffer;
    return results;
}

void IRCClassifier::Release()
{
    cout << "Clean up..." <<  endl;
}

    
