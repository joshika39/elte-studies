#include <stdio.h>
#include <string.h>
#include <malloc.h>
#include <stdarg.h>

typedef struct Szallitmany {
    int id;
    char videk[100];
    char nev[100];
    float mennyiseg;
    char fajta[100];
} szallitmany;

typedef struct Node {
    szallitmany data;
    struct Node *next;
} node;

void getStringInput(char *title, char *buff, int num, ...) {
    va_list valist;
    va_start(valist, num);
    if(num <= 0){
        printf("%s: ", title);
    } else {
        printf("%s (%s): ", title, va_arg(valist, char*));
    }
    fgets(buff, sizeof(char[100]), stdin);
    int len;
    len = strlen(buff);
    if (buff[len - 1] == '\n')
        buff[len - 1] = 0;
    fflush(stdin);
    va_end(valist);
}

void getFloatInput(char *title, float *varRef, int num, ...) {
    va_list valist;
    va_start(valist, num);
    if(num <= 0){
        printf("%s: ", title);
    } else {
        printf("%s (%f): ", title, va_arg(valist, double));
    }
    scanf("%f", varRef);
    fflush(stdin);
    va_end(valist);
}

void getIntInput(char *title, int *varRef) {
    printf("%s: ", title);
    scanf("%d", varRef);
    fflush(stdin);
}

char* combineString(char* title, char* value){
    char* buffer = malloc(100 * sizeof(char));
    snprintf(buffer, sizeof(buffer), "%s (%s)", title, value);
    return buffer;
}

void insertAtEnd(node **head_ref, szallitmany new_data) {
    node *new_node = (node *) malloc(sizeof(node));
    node *last = *head_ref; /* used in step 5*/

    new_node->data = new_data;
    new_node->next = NULL;

    if (*head_ref == NULL) {
        *head_ref = new_node;
        return;
    }

    while (last->next != NULL) last = last->next;

    last->next = new_node;
}

void deleteNode(node **head_ref, int id) {
    node *temp = *head_ref, *prev;

    if (temp != NULL && temp->data.id == id) {
        *head_ref = temp->next;
        free(temp);
        return;
    }

    while (temp != NULL && temp->data.id != id) {
        prev = temp;
        temp = temp->next;
    }

    if (temp == NULL) {
        return;
    }

    prev->next = temp->next;
    free(temp);
}

node *searchNode(node **head_ref, int id) {
    node *current = *head_ref;

    while (current != NULL) {
        if (current->data.id == id) {
            return current;
        }
        current = current->next;
    }

    return NULL;
}

int getNewId(node *node) {
    int highestId = 0;
    while (node != NULL) {
        if (node->data.id > highestId) {
            highestId = node->data.id;
        }
        node = node->next;
    }

    return highestId + 1;
}

node *getSavedList(char filename[50]) {
    FILE *fajl = fopen(filename, "r");
    if (fajl == NULL) {
        printf("Hiba a fajl megnyitasakor.\n");
        return NULL;
    }

    node *head = NULL;

    struct Szallitmany sz;
    while (fscanf(fajl, "%d;%49[^;];%49[^;];%f;%49[^\n]\n", &sz.id, sz.videk, sz.nev, &sz.mennyiseg, sz.fajta) != EOF) {
        insertAtEnd(&head, sz);
    }

    fclose(fajl);
    return head;
}

void save(node *head) {
    FILE *fajl = fopen("szolo.dat", "w");
    if (fajl == NULL) {
        printf("Hiba a fajl megnyitasakor.\n");
        return;
    }


    while (head != NULL) {
        fprintf(fajl, "%d;%s;%s;%.2f;%s\n", head->data.id, head->data.videk, head->data.nev, head->data.mennyiseg,
                head->data.fajta);
        head = head->next;
    }

    fclose(fajl);
}

void add(node *head) {
    szallitmany uj_szallitmany;
    uj_szallitmany.id = getNewId(head);

    getStringInput("Videk", uj_szallitmany.videk, 0);
    getStringInput("Termelo", uj_szallitmany.nev, 0);
    getFloatInput("Mennyiseg", &uj_szallitmany.mennyiseg, 0);
    getStringInput("Fajta", uj_szallitmany.fajta, 0);

    insertAtEnd(&head, uj_szallitmany);
    save(head);
}

void edit(node *head) {
    int id;
    getIntInput("Melyik termelo adatait szeretned modositani?\nAdja meg a termelo azonositojat", &id);

    node *result = searchNode(&head, id);

    if (result == NULL) {
        printf("Nem talalható ilyen termelo.\n");
        return;
    }

    getStringInput("Videk", result->data.videk, 1, result->data.videk);
    getStringInput("Termelo", result->data.nev, 1, result->data.nev);
    getFloatInput("Mennyiseg", &result->data.mennyiseg, 1, result->data.mennyiseg);
    getStringInput("Fajta", result->data.fajta, 1, result->data.fajta);

    save(head);
}

void delete(node *head) {
    int id;
    getIntInput("Melyik termelo adatait szeretned torolni?\nAdja meg a termelo azonositojat", &id);

    deleteNode(&head, id);
    save(head);
}

void filteredSearch(node *head) {
    char videk[50];
    getStringInput("Videk: ", videk, 0);

    while (head != NULL) {
        if (strcmp(head->data.videk, videk) == 0) {
            printf("%d;%s;%s;%.2f;%s\n", head->data.id, head->data.videk, head->data.nev, head->data.mennyiseg,
                   head->data.fajta);
        }
        head = head->next;
    }
    printf("\n");
}

void printList(node *head) {
    while (head != NULL) {
        printf("%d;%s;%s;%.2f;%s\n", head->data.id, head->data.videk, head->data.nev, head->data.mennyiseg,
               head->data.fajta);
        head = head->next;
    }
    printf("\n");
}


// Driver program
int main() {
    node *head = getSavedList("init.dat");
    int option = 0;
    do {
        printf("1 - List all\n");
        printf("2 - Filtered search (by wine region)\n");
        printf("3 - Add new record\n");
        printf("4 - Edit a record\n");
        printf("5 - Delete a record\n");
        printf("6 - Quit\n");
        printf("Option: ");
        fflush(stdin);
        scanf("%d", &option);
        fflush(stdin);
        switch (option) {
            case 1:
                printList(head);
                break;
            case 2:
                filteredSearch(head);
                break;
            case 3:
                add(head);
                break;
            case 4:
                edit(head);
                break;
            case 5:
                delete(head);
                break;
            case 6:
                break;
            default:
                printf("Hibas bemenet!\n");
                break;
        }
    } while (option != 6);
}