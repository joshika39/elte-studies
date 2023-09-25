#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <stdlib.h>

int main() {
    pid_t child = fork();
    fork();
    fork();

    printf("Hello akt pid %i, gyereke %i, szuloje: %i\n", child, getpid(), getppid());
    return 0;
}
