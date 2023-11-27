#include <iostream>


int main() {
    int array[7] = {12, 8, 5, 17, 9, 3, 14};

    for (int i = 0; i < 7; ++i) {
        std::cout << array[i] << " ";
    }

    std::cout << std::endl;
    return 0;
}