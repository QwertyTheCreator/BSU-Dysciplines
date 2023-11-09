#include <thread>
#include <chrono>
#include <iostream>
#include <utility>
#include <conio.h>

void FillArray(int* arr, int size, std::istream& is = std::cin)
{
	for (int i = 0; i < size; i++)
	{
		is >> arr[i];
	}
}

void PrintArray(int* arr, int size, std::ostream& os = std::cout)
{
	for (int i = 0; i < size; i++)
	{
		os << arr[i] << " ";
	}
}

void ChangeMinMaxOnAverage(int* arr, const int size, const int min, const int max, const int average)
{
	for (int i = 0; i < size; i++)
	{
		if (arr[i] == min || arr[i] == max)
		{
			arr[i] = average;
		}
	}
}

std::pair<int, int> FindMinMax(int* arr, const int size) //Thread 1
{
	int minIndex = 0;
	for (int i = 0; i < size; i++)
	{
		if (arr[minIndex] > arr[i])
		{
			minIndex = i;
		}

		std::this_thread::sleep_for(std::chrono::milliseconds(7));
	}

	int maxIndex = 0;
	for (int i = 0; i < size; i++)
	{
		if (arr[maxIndex] < arr[i])
		{
			maxIndex = i;
		}

		std::this_thread::sleep_for(std::chrono::milliseconds(7));
	}

	std::cout << "\nFIND_MIN_MAX THREAD:: Minimal is: " << arr[minIndex]
		<< " Max is: " << arr[maxIndex] << "\n\n";

	return { arr[minIndex], arr[maxIndex] };
}

int GetAverageElement(int* arr, const int size) //Thread 2
{
	int sum = 0;
	for (int i = 0; i < size; i++)
	{
		sum += arr[i];
		std::this_thread::sleep_for(std::chrono::milliseconds(12));
	}

	int average = sum / size;

	std::cout << "\n\nGET_AVERAGE_ELEMENT_THREAD:: Average is: " << average << "\n\n";

	return average;
}

int main()
{
	int size;
	std::cout << "Enter size of array: ";
	std::cin >> size;

	std::cout << "Enter numbers in array: ";
	int* arr = new int[size];
	FillArray(arr, size);

	std::pair<int, int> minAndMax;
	std::thread FindMinMaxThread([&]() {minAndMax = FindMinMax(arr, size); });
	
	int average;
	std::thread GetAverageElementThread([&]() {average = GetAverageElement(arr, size); });
	
	FindMinMaxThread.join();
	GetAverageElementThread.join();
	
	ChangeMinMaxOnAverage(arr, size, minAndMax.first, minAndMax.second, average);

	std::cout << "The result is: ";
	PrintArray(arr, size);
	delete[] arr;

	std::cout << "\n\nPress any key to quit";
	_getch();

	return 0;
}