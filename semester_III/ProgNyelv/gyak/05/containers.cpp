#include <iostream>
#include <vector>

int main() {
	srand(time(0));

	std::vector<int> v1, v2(10);
	std::cout << "Capacity | v1: " << v1.capacity() << ", " << "v2: " << v2.capacity();
	std::cout << "MaxSize | v1: " << v1.max_size() << ", " << "v2: " << v2.max_size();
}