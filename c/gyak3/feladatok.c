#include <stdio.h>
#include <math.h>

void test(){
    printf("%ld\n", sizeof(int));
    printf("%ld\n", sizeof(unsigned int));
    printf("%ld\n", sizeof(long int));
    printf("%ld\n", sizeof(unsigned long int));
}

void feladat3(){
    int a = pow(2, sizeof(int)*8 - 1) - 1;
    printf("%d\n", a + 1);
}

void feladat4(){
    unsigned int a = pow(2, sizeof(unsigned int)*8) - 1;
    printf("%u\n", a + 1);
}
void feladat5(){
    printf("%d\n", '*');
    printf("%o\n", '\"');
    printf("%x\n", 'B');
    printf("%d\n", 0b101010);
}

void pascal(){
    int rows, coef = 1, space, i, j;
    printf("Enter the number of rows: ");
    scanf("%d", &rows);
    for (i = 0; i < rows; i++) {
        for (space = 1; space <= rows - i; space++)
            printf("  ");
        for (j = 0; j <= i; j++) {
            if (j == 0 || i == 0)
                coef = 1;
            else
                coef = coef * (i - j + 1) / j;
            printf("%4d", coef);
        }
        printf("\n");
    }
}

void gyak1(){
    int inputChar;
    char num[100];
    int i = 0;
    do{
        scanf("%c", &inputChar);
        if((inputChar >= '0' && inputChar <= '9') || (inputChar >= 'a' && inputChar <= 'd') || inputChar != 'q'){
            num[i] = inputChar;
            i++;
        }else{
            printf("Nem jo ertek\n");
        }
    }while(inputChar != 'q');
}

int main(){
    gyak1();
}