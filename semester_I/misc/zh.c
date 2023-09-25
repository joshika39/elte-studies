#include <stdio.h>
 
int* get_min(int* a, int size){
    
    int *min = *a;
 
    for ( int c = 1 ; c < size ; c++ ) 
    {
        if ( *(a+c) < *min ) 
        {
           *min = *(a+c);
        }
  }
  return min;

}

int main() 
{
    int size;
    int a[] = {10, 40, 20, 45, 11, 5 };
 
    int* minim = get_min(a, 6);
 
 
    printf("Minimum elem: %d", *minim);
    return 0;
}
