#include <fstream>
#include <iostream>
#include <Windows.h>
#include <conio.h>
#include <string>
#include "HelpFunctions.h"


int main()
{
	std::string fileName;
	std::cout << "Enter binary file name: ";
	std::getline(std::cin, fileName);

	CreateBinaryFile(fileName);

	int numberOfNotes;
	EnterNums(numberOfNotes, "Enter number of notes in message: ");

	int numberOfSenders;
	EnterNums(numberOfSenders, "Enter number of senders processes: ");

	HANDLE hInputReadySemaphore = CreateSemaphore(NULL, 0, numberOfNotes, L"Input Semaphore started");
	if (hInputReadySemaphore == NULL)
	{
		return GetLastError();
	}

	HANDLE hOutputReadySemaphore = CreateSemaphore(NULL, 0, numberOfNotes, L"Output Semaphore started");
	if (hOutputReadySemaphore == NULL)
	{
		return GetLastError();
	}

	HANDLE hMutex = CreateMutex(NULL, 0, L"mutex");

	HANDLE* hEventStarted = new HANDLE[numberOfSenders];
	STARTUPINFO si;
	PROCESS_INFORMATION pi;
	LPWSTR lpwstrSenderCmd;
	for (int i = 0; i < numberOfSenders; i++)
	{
		std::string senderCmd = "Sender.exe " + fileName;
		std::wstring convertToLpwstr = std::wstring(senderCmd.begin(), senderCmd.end());
		lpwstrSenderCmd = &convertToLpwstr[0];
		ZeroMemory(&si, sizeof(STARTUPINFO));
		si.cb = sizeof(STARTUPINFO);
		if (!CreateProcess(NULL, lpwstrSenderCmd, NULL, NULL, TRUE, CREATE_NEW_CONSOLE, NULL, NULL, &si, &pi))
		{
			system("cls");
			std::cout << "The Sender Process is not started.\n";
			return GetLastError();
		}

		hEventStarted[i] = CreateEvent(NULL, FALSE, FALSE, L"Process Started");

		if (hEventStarted[i] == NULL)
		{
			return GetLastError();
		}
		CloseHandle(pi.hProcess);
		CloseHandle(pi.hThread);
	}

	WaitForMultipleObjects(numberOfSenders, hEventStarted, TRUE, INFINITE);

	ReleaseMutex(hMutex);

	int key;
	EnterOption(key);

	std::fstream file(fileName, std::ios::in);
	while (true)
	{
		if (key == 1)
		{
			std::string message;
			WaitForSingleObject(hInputReadySemaphore, INFINITE);
			WaitForSingleObject(hMutex, INFINITE);
			std::getline(file, message);
			std::cout << message;
			ReleaseSemaphore(hOutputReadySemaphore, 1, NULL);
			ReleaseMutex(hMutex);

			EnterOption(key);
		}
		else
		{
			std::cout << "Process ended";
			break;
		}
	}

	CloseHandle(hInputReadySemaphore);
	CloseHandle(hOutputReadySemaphore);
	for (int i = 0; i < numberOfSenders; i++)
	{
		CloseHandle(hEventStarted[i]);
	}
	delete[] hEventStarted; // Будет ли ошибка

	return 0;
}