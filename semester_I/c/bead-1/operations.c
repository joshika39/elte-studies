//
// Created by joshika39 on 23/01/02.
//
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

#include "operations.h"

int modeStrToInt(char *mode){
    if(strcmp(mode, "linenums") == 0){
        return 0;
    }
    else if(strcmp(mode, "nolinenums") == 0) {
        return 1;
    }
    else{
        printf("Unknown format style: %s! Only available: linenums, nolinenums\n", mode);
        exit(EXIT_FAILURE);
    }
}

void reverse_str(int *str, int n)
{
    for (int i = 0; i < n / 2; ++i) {
        int tmp = str[i];
        str[i] = str[n - 1 - i];
        str[n - 1 - i] = tmp;
    }
}

int read_line(int sep, int *str, int n, FILE *stream, int maxLength)
{
    int length = 0;
    while (length < maxLength + 1 && length < n){
        int c = fgetc(stream);

        str[length++] = c;

        if(length + 1 == maxLength + 1 && c != sep){
            c = sep;
            str[length++] = c;
        }

        if(c == sep || c == EOF){
            break;
        }

    }
    return length;
}

void write_line(int *str, int n, FILE *stream)
{
    for (int i = 0;i < n; i++)
    {
        fputc(str[i], stream);
        if(str[i] == EOF){
            fputc(L'\n', stream);
        }
    }
}


line* assign_line(const int *input, int length, int num){
    line *new_node = (line *) malloc(sizeof(line));

    new_node->line = malloc(BUFSIZ * sizeof(char));

    for (int i = 0; i < length; ++i) {
        *(new_node->line + i) = *(input + i);
    }
    new_node->line_len = length;
    new_node->line_num = num;
    new_node->next = NULL;

    return new_node;
}

void push(line *head, const int *new_line, int new_line_len, int num) {
    line *current = head;
    while (current->next != NULL) {
        current = current->next;
    }

    current->next = assign_line(new_line, new_line_len, num);

    current->next->next = NULL;
}

void reverse(line ** head_ref)
{
    line* prev = NULL;
    line* current = *head_ref;
    line* next = NULL;
    while (current != NULL) {
        next = current->next;

        current->next = prev;

        prev = current;
        current = next;
    }
    *head_ref = prev;
}

void print_list(line *head, FILE *stream, params_t params) {
    line *current = head;
    int count = 0;
    while (current != NULL) {

        if(params.mode == 0){
            printf("%d ", current->line_num);
        }
        int len = current->line_len;
        reverse_str(current->line, current->line[len - 1] == L'\n' ? len - 1 : len);
        write_line(current->line, len, stream);

        current = current->next;
        count++;
    }
}
