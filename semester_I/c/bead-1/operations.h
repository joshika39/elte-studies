//
// Created by joshika39 on 23/01/02.
//

#ifndef REVERSE_OPERATIONS_H
#define REVERSE_OPERATIONS_H

#include <stdio.h>
#include <stdlib.h>

typedef struct params{
    int max_lines;
    int mode;
    int lines;
} params_t;

int modeStrToInt(char *);

void write_line(int *, int, FILE *);
int read_line(int, int *, int, FILE *, int);
void reverse_str(int *, int);

typedef struct Node {
    int *line;
    int line_len;
    int line_num;
    struct Node* next;
} line;

line* assign_line(const int *, int , int);
void push(line *, const int *, int, int );
void reverse(line **);
void print_list(line *, FILE *, params_t);

#endif //REVERSE_OPERATIONS_H
