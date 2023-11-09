#include <iostream>
#include <Windows.h>
#include <mutex>
#include <thread>
#include <vector>
#include <chrono>
#include <algorithm>
#include <condition_variable>


std::mutex mtx;
std::condition_variable cv;
std::vector<bool> threadExited;
std::vector<bool> threadSleep;


void EnterSizeOfArray(int& size, std::istream& is = std::cin, std::ostream& os = std::cout)
{
	os << "Enter size of the array: ";
	while (true)
	{

		is >> size;

		if (size <= 0)
		{
			system("cls");
			os << "Incorrect array size(it must be greater then 0)\n"
				<< "Enter size again: ";
			continue;
		}

		break;
	}
}


void EnterSizeOfMarker(int& count, std::istream& is = std::cin, std::ostream& os = std::cout)
{
	os << "Enter count of marker thread: ";
	while (true)
	{

		is >> count;

		if (count <= 0)
		{
			system("cls");
			os << "Incorrect count of threads(it must be greater then 0)\n"
				<< "Enter count again: ";
			continue;
		}

		break;
	}
}


void EnterStopId(int& stopId, const int max, std::istream& is = std::cin, std::ostream& os = std::cout)
{
	std::cout << "\nInput thread id stop(from 1 to n): ";
	while (true)
	{
		std::cin >> stopId;
		if (stopId <= 0 || stopId > max)
		{
			std::cout << "\n\nIncorrect, range is (1; n)\n"
				<< "Try again: ";
		}
		else
		{
			stopId;
			return;
		}
	}
	
}


void marker(size_t id, std::vector<int>& arr)
{
	srand(id);

	int size = arr.size();
	int countOfColored = 0;

	while (true)
	{
		std::unique_lock<std::mutex> ul(mtx);
		if (threadExited[id] == 1)
		{
			ul.unlock();
			break;
		}

		cv.wait(ul, [&]
			{
				return threadSleep[id] == 0;
			});

		if (threadExited[id] == 1)
		{
			ul.unlock();
			cv.notify_all();
			break;
		}

		int rng = rand() % size;
		if (arr[rng] == 0)
		{
			std::this_thread::sleep_for(std::chrono::milliseconds(5));
			arr[rng] = id + 1;
			
			std::this_thread::sleep_for(std::chrono::milliseconds(5));
			countOfColored++;
		}
		else
		{
			std::cout << "\nThread id: " << id + 1 << "\n"
				<< "Count of colored: " << countOfColored << "\n"
				<< "Impossible to color index: " << rng << "\n\n";

			threadSleep[id] = 1;
			countOfColored = 0;
		}

		ul.unlock();
		cv.notify_all();
	}

	cv.notify_all();
}


int main()
{
	int size;
	EnterSizeOfArray(size);

	std::vector<int> arr(size, 0);

	int countOfThreads;
	EnterSizeOfMarker(countOfThreads);

	std::vector<std::thread> threads;
	threadExited.resize(countOfThreads, 0);
	threadSleep.resize(countOfThreads, 0);

	for (int i = 0; i < countOfThreads; i++)
	{
		threads.emplace_back(marker, i, std::ref(arr));
	}
	
	cv.notify_all();

	int amountOfExitedThreads = 0;
	while (amountOfExitedThreads < countOfThreads)
	{
		std::unique_lock<std::mutex> ul(mtx);

		cv.wait(ul, [&]
			{return std::find(threadSleep.begin(), threadSleep.end(), 0) == threadSleep.end(); });

		if (amountOfExitedThreads == countOfThreads - 1)
		{
			ul.unlock();
			break;
		}

		for (auto i : arr)
		{
			std::cout << i << " ";
		}

		int stopId;
		EnterStopId(stopId, countOfThreads);

		for (size_t i = 0; i < size; i++)
		{
			if (arr[i] == stopId) //Вопрос на тему foreach
			{
				arr[i] = 0;
			}
		}

		threadExited[stopId - 1] = true;
		threadSleep = threadExited;

		amountOfExitedThreads++;
		ul.unlock();
		cv.notify_all();
	}

	for (size_t i = 0; i < countOfThreads; i++)
	{
		threads[i].detach();
	}

	std::cout << "\n\nFinal Array is:\n";
	for (auto i : arr)
	{
		std::cout << i << " ";
	}

	return 0;
}