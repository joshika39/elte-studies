#include <stdio.h>
#include <stdlib.h>

#include "operations.h"

int main(int argc, char *argv[]) {
    params_t params;

    if(argc < 3){
        printf("Usage:\n\trev [SHOW LINE NUMBERS] [MAX LINE LENGTH] files...\n");
        return 0;
    }

    char *conv_error;
    int base = 10;

    params.max_lines = (int)strtol(argv[2], &conv_error, base);
    if(*conv_error){
        printf("Wrong max character number: Unable to convert '%s' to base %d.", argv[2], base);
        return 1;
    }
    params.mode = modeStrToInt(argv[1]);
    params.lines = 1;

    int *buffer;
    int sep = L'\n';
    int len, bufferSize = BUFSIZ;
    int r_val = 0;
    FILE *fp = stdin;
    line *head = NULL;

    argv+=3;

    buffer = malloc(bufferSize * sizeof(int));

    if(argc == 3){
        while (!feof(fp)){
            buffer = realloc(buffer, bufferSize * sizeof(char));
            len = read_line(sep, buffer, bufferSize, fp, params.max_lines);
            if (len == 0)
                continue;

            if(buffer[0] != EOF){
                if(head == NULL){
                    head = assign_line(buffer, len, params.lines);
                } else{
                    push(head, buffer, len, params.lines);
                }
            }

            stdin->_IO_read_ptr = stdin->_IO_read_end;
            params.lines++;
        }
        reverse(&head);
        print_list(head, stdout, params);
    }
    else{
        do {
            if (*argv) {
                fp = fopen(*argv, "r");
                if (fp == NULL) {
                    printf("File opening unsuccessful: %s", *argv );
                    ++argv;
                    break;
                }
                argv++;
            }

            params.lines = 1;
            while (!feof(fp)){
                buffer = realloc(buffer, bufferSize * sizeof(char));
                len = read_line(sep, buffer, bufferSize, fp, params.max_lines);
                if (len == 0)
                    continue;

                if(buffer[0] != EOF){
                    if(head == NULL){
                        head = assign_line(buffer, len, params.lines);
                    } else{
                        push(head, buffer, len, params.lines);
                    }
                }

                params.lines++;
            }
            if (fp != stdin)
                fclose(fp);
        } while(*argv);

        reverse(&head);
        print_list(head, stdout, params);
    }

    free(buffer);
    free(head);
    return r_val;
}
