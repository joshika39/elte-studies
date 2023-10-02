#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>  //fork
#include <sys/wait.h> //waitpid
#include <errno.h>
#include "string.h"

char *getSingleLine(void) {
    char *line = malloc(100), *linep = line;
    size_t lenmax = 100, len = lenmax;
    int c;

    if (line == NULL)
        return NULL;

    for (;;) {
        c = fgetc(stdin);
        if (c == EOF)
            break;

        if (--len == 0) {
            len = lenmax;
            char *linen = realloc(linep, lenmax *= 2);

            if (linen == NULL) {
                free(linep);
                return NULL;
            }
            line = linen + (line - linep);
            linep = linen;
        }

        if ((*line++ = c) == '\n')
            break;
    }
    *line = '\0';
    return linep;
}

int main() {
    int status;

    pid_t child = fork(); //forks make a copy of variables
    if (child < 0) {
        perror("The fork calling was not succesful\n");
        exit(1);
    }
    if (child > 0) //the parent process, it can see the returning value of fork - the child variable!
    {
        waitpid(child, &status, 0);
        printf("The end of parent process\n");
    } else //child process
    {
//    char * cmd="./write";
//    char * arg[]={"./write","Operating Systems","5",NULL};

        char *line = getSingleLine();
        char **tokens = NULL;
        int token_count = 0;

        // Split the line at whitespaces using strtok
        char *token = strtok(line, " ");

        while (token != NULL) {
            // Allocate memory for the new token
            char *new_token = malloc(strlen(token) + 1);
            if (new_token == NULL) {
                perror("Memory allocation failed");
                exit(1);
            }

            // Copy the token into the newly allocated memory
            strcpy(new_token, token);

            // Add the token to the char** array
            tokens = realloc(tokens, (token_count + 1) * sizeof(char*));
            if (tokens == NULL) {
                perror("Memory allocation failed");
                exit(1);
            }
            tokens[token_count] = new_token;
            token_count++;

            // Get the next token
            token = strtok(NULL, " ");
        }

        *(tokens+token_count) = NULL;

//        for (int i = 0; i < token_count + 1; i++) {
//            printf("Token %d: %s\n", i + 1, tokens[i]);
//        }

        char** args = NULL;

        for (int i = 0; i < token_count + 1; ++i) {
            if(i < token_count){
                args[i] = tokens[i];
            }
            else{
                args[i] = NULL;
            }

        }

        char* command = *tokens;
        printf("Command: %s\n", command);
        execv(command, args);

        // Don't forget to free the allocated memory
//        for (int i = 0; i < token_count; i++) {
//            free(tokens[i]);
//        }
//        free(tokens);
    }
    return 0;
}

