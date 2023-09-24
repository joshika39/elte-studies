#include <stdio.h>

int main()
{
	float a1 = 10000;
	float q = 0.5;
	float stop;
	printf("Kerek egy minimum erteket: ");
	scanf("%f", &stop);
	//for(int ai = 10000; ai >= stop; ai *= q){
	//	printf("%d\n", ai);
	//}
	int ai = a1;
	while(ai >= stop){
		printf("%d\n", ai);
		ai = ai * q;
	}	
	
	return 0;
}
