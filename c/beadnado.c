#include <stdio.h>

int main()
{

	float a = 3, b = 2, c;
	c = a / b; // float - lebegopontos
	printf("%0.2f\n", c);
	int c2;	
	c = (int)a / (int)b; // int - vagja a maradekot
	printf("%d\n", (int)c);

	int maradek;
	maradek = (int)a % (int)b; // maradekos osztas
	printf("%d\n", maradek);
	
		
	return 0;

}
