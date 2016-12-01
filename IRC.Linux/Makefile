CC=g++

all: nativelib wrapper copy

nativelib:
	$(CC) -shared -fPIC -o libIRCClassifier.so ./ImgClassifier/IRCClassifier.cpp  


wrapper:
	$(CC) -shared -fPIC -o libNativeWrapper.so ./NativeWrapper.cpp -lIRCClassifier -L.

clean:
	@ rm libIRCClassifier.so libNativeWrapper.so

copy:
	cp libIRCClassifier.so libNativeWrapper.so ./bin/Debug
