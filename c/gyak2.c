#include <stdio.h>
void task_1 (void) { // fahrenheight
	float getFahren(int fahren){
		return (fahren - 32)/1.8;
	}
	for(int i = -20; i <= 200; i+=10){
		printf("Fahrenheit: %d --> Celsius %f\n", i, getFahren(i));	
	}
} 

void task_2(void){
	printf("\"Hello\"\n\"World\"\n");
}
void task_3(void){
	int szam, count = 0;
	printf("Kerek egy szamot: ");
	scanf("%d", &szam);
	int szamDigits = szam;
	while(szamDigits != 0){
		szamDigits /= 10;
		count++;
	}

	int digits[count];
	count = 0;
	while(szam != 0){
		digits[count] = szam % 10;
		szam = szam / 10;
		count++;
	}
	printf("Uj szam: ");
	for(int i = count - 1; i >= 0; i--){
		printf("%d", digits[i]);
	}
	printf("\n");
	
}

void task_4(void){
	int szam;
	printf("Kerek egy szamot: ");
	scanf("%d", &szam);
	printf("%d szam osztoi: ", szam);
	for(int i = 1; i <= szam; i++){
		if(i == szam)
			printf("%d\n", i);
		else if(szam % i == 0)
			printf("%d, ", i);
	}

}
void task_5(void){
	
	int gcd(int x, int y){
   		if (y == 0)
			return x;
		return gcd(y, x % y);
	}
	int a, b;
	printf("Kerek ket szamot(a b): ");
	
	scanf("%d %d", &a, &b);
	int gcdNum;
	gcdNum = gcd(a, b);
	printf("%d\n", gcdNum);

}

void task_6(void){
	for(int i = 0; i <= 10; i++){
		if(i == 0){
			printf("x");
			// printf("_\t");
		}
		for(int j = 0; j <= 10; j++){
			if(j == 0 && i == 0){
				printf("x");
			}
			else{
				printf("%d\t", i*j);
			}

		}
		printf("\n");
	}

}
void task_7(void){
	for(float i = 0.0; i <= 1.01; i += 0.1){
		printf("%.30f\n", i);
	}
}

void task_8(void){
	for(int i = 1; i <= 10; i++){
		for(int j = 1; j <= 10; j++){
			printf("%d\t", i*j);
		}
		printf("\n");
	}

}

int main()
{

	// task_1();
	// task_2();
	// task_3();
	// task_4();
	// task_5();
	task_6();
	// task_7();
	return 0;
}
