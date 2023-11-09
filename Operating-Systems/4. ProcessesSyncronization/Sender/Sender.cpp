#include <fstream>
#include <windows.h>
#include <conio.h>
#include <iostream>
#include "HelpFunctions.h"

int main(int argc, char* argv[]) {
	std::string fileName = argv[1];
	std::fstream file;

	HANDLE hStartEvent = OpenEvent(EVENT_MODIFY_STATE, FALSE, L"Process Started");
	if (hStartEvent == NULL)
	{
		std::cout << "Open event failed. \nInput any char to exit.\n";
		std::cin.get();
		return GetLastError();
	}

	HANDLE hInputReadySemaphore = OpenSemaphore(EVENT_ALL_ACCESS, FALSE, L"Input Semaphore started");
	if (hInputReadySemaphore == NULL)
		return GetLastError();

	HANDLE hOutputReadySemaphore = OpenSemaphore(EVENT_ALL_ACCESS, FALSE, L"Output Semaphore started");
	if (hOutputReadySemaphore == NULL)
		return GetLastError();

	HANDLE hMutex = OpenMutex(SYNCHRONIZE, FALSE, L"mut ex");

	SetEvent(hStartEvent);

	int key;
	EnterOption(key);

	while (true) {
		if (key == 1) {

			WaitForSingleObject(hMutex, INFINITE);
			file.open(fileName, std::ios::out | std::ios::app);
			std::string msg;
			std::cout << "Input message to add\n";
			std::cin >> msg;
			char message[20];
			for (int i = 0; i < msg.length(); i++)
			{
				message[i] = msg[i];
			}
			for (int i = msg.length(); i < 20; i++)
			{
				message[i] = '\0';
			}

			message[19] = '\n';
			ReleaseMutex(hMutex);
			ReleaseSemaphore(hOutputReadySemaphore, 1, NULL);
			if (ReleaseSemaphore(hInputReadySemaphore, 1, NULL) != 1) {
				std::cout << "file is full";
				ReleaseSemaphore(hOutputReadySemaphore, 1, NULL);
				WaitForSingleObject(hOutputReadySemaphore, INFINITE);
				ReleaseSemaphore(hOutputReadySemaphore, 1, NULL);
				ReleaseSemaphore(hInputReadySemaphore, 1, NULL);
				for (int i = 0; i < 20; i++)
				{
					file << message[i];
				}

			}
			else {
				for (int i = 0; i < 20; i++)
				{
					file << message[i];
				}
			}
			file.close();
			EnterOption(key);;
		}

		if (key == 2) {
			std::cout << "Process ended.";
			break;
		}
	}

	return 0;
}