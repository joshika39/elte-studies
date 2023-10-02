#include<iostream>
#include<cstddef>

int main() {
	int *p = NULL;

	int a,b;
	a = b = 6; // Itt a b = 6 hattal fog vissza terni es atadja az `a`-nak
	
	if (p == NULL) { // Nem jo otlet ilyet
		std::cout << "p NULL, nem mutat sehova" << std::endl;
	}

	if (a = 7) {
		std::cout << "a tenyleg het... mostmar" << std::endl;
	}

	std::cout << "a: " << a << std::endl;

	if (8 = a) {
		std::cout << "a tenyleg het... mostmar" << std::endl;
	}
}
