#include <stdio.h>
#include <stdlib.h>

const float pi = 3.1415;

int main(){
	Feladat1();
	Feladat2();
	Feladat3();
	Feladat4();
	return 0;
}

void Feladat1(){
	printf("Az en nevem hegedus joshua\n");
}

void Feladat2(){
	int a, b;
	printf("Kerek ket szamot (a b) alakban: ");
	scanf("%d %d", &a, &b);
	printf("a ket szam osszege %d\n", a + b);
}
void Feladat3(){
	float a, b;
	printf("Kerek ket szamot (a b) alakban: ");
	scanf("%f %f", &a, &b);
	printf("a ket szam hanyadosa %0.2f\n", a / b);
}
void Feladat4(){
	printf("Mit szeretne szamolni, kor vagy negyzet? (k, n)\n");
	char choice;
	// fflush();
	scanf(" %c", &choice);
	switch(choice){
		case 'n':
			float a, b;
			printf("Kerek negyzet oldalait (a b) alakban: ");
			scanf("%f %f", &a, &b);
			float terulet = a*b;
			float kerulet = 2*a + 2*b;
			printf("a negyzet kerulete %0.2f\n", kerulet);
			printf("a negyzet terulete %0.2f\n", terulet);
			break;
		case 'k':
			float r;
			printf("Kerek a kor sugarat: ");
			scanf("%f", &r);
			printf("a kor kerulete %0.2f\n", 2*pi*r);
			printf("a kor terulete %0.2f\n", pi*r*r);
			break;
	}
}

