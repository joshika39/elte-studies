#include <stdio.h>
#include <stdlib.h>

int compareInt(const void* a, const void* b){
	int x = *(int *)a,
		y = *(int *)b;
	// printf("x: %d, y: %d\n", x,y);
	// return x - y; // Nem jo a ket veglet miatt
	return (x > y) - (x < y); // novekvo
	// return (x < y) - (x > y);
}

void printTomb(int *t, int n){
	for(int i = 0; i < n; i++){
		printf("%d, ", *t+i);
	}
	printf("\n");
}

int main(){
	int tomb[] = {3,2,1,4,5,6,7,8,9,10};
	int n = sizeof(tomb) / sizeof(tomb[0]);

	int max = tomb[0];
	for(int i = 1; i < n; i++){
		if(max < tomb[i]){
			max = tomb[i];
		}
	}

	int min1 = tomb[0];
	int min2 = tomb[1];
	for (int i = 1; i < n; i++){
		if (min1 > tomb[i]){
			min2 = min1;
			min1 = tomb[i];

		} else if (min2 > tomb[i] && min1 < tomb[i]){
			min2 = tomb[i];
		}
	}
	qsort(tomb, n, sizeof(*tomb), compareInt);
	printf("A tomb legkisebb eleme: %d\n", min1);
	printf("A tomb masodik legkisebb eleme: %d\n", min2);
	printTomb(tomb, n);
}

