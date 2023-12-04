#include <stdio.h>
#include <string.h>
#include <malloc.h>
#include <stdarg.h>
#include <stdlib.h>
#include <signal.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <unistd.h>
#include <time.h>

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

char *constants[11][5] =
{
        {"1", "Egeri borvidek",          "Nagy Arpad",    "9.100000", "Bikaver"},
        {"2", "Villanyi borvidek",       "Kovacs Emese",  "8.600000", "Kadarka"},
        {"3", "Balatonlellei borvidek",       "Kovacs Emese",  "4.5", "Keknyelu"},
        {"4", "Szekszardi borvidek",     "Szabo Gergo",   "7.300000", "Olaszrizling"},
        {"5", "Balatonlellei borvidek",  "Horvath Timea", "6.800000", "Riesling"},
        {"6", "Soproni borvidek",        "Kiss Robert",   "7.4",      "Keknyelu"},
        {"7", "Villanyi borvidek",        "Kiss Robert",   "6.5",      "Olaszrizling"},
        {"8", "Egeri borvidek", "Szabo Andrea",  "8.2",      "Chardonnay"},
        {"9", "Balatonlellei borvidek",  "Szabo Gergo",   "9.800000", "Olaszrizling"},
        {"10", "Soproni borvidek",        "Kovacs Emese",  "7.4",      "Riesling"},
        {"11", "Egeri borvidek",        "Kovacs Emese",  "4.4",      "Kadarka"},
};
int size = sizeof(constants) / sizeof(constants[0]);

void populateInitFile(){
    FILE* init = fopen("init.dat", "w");
    for(int i = 0; i<size; i++)
    {
        fprintf(init, "%d;%s;%s;%f;%s\n", atoi(constants[i][0]), constants[i][1], constants[i][2], atof(constants[i][3]), constants[i][4]);
    }

    fclose(init);
}

void getStringInput(char *title, char *buff, int num, ...) {
    va_list valist;
    va_start(valist, num);
    if (num <= 0) {
        printf("%s: ", title);
    } else {
        printf("%s (%s): ", title, va_arg(valist, char*));
    }
    fflush(stdin);
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
    if (num <= 0) {
        printf("%s: ", title);
    } else {
        printf("%s (%f): ", title, va_arg(valist, double));
    }

    scanf("%f", &varRef);
    va_end(valist);
}

void getIntInput(char *title, int *varRef) {
    printf("%s: ", title);
    scanf("%d", &varRef);
    fflush(stdin);
}

char *combineString(char *title, char *value) {
    char *buffer = malloc(100 * sizeof(char));
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

void handler(int signumber){
#if DEBUG
    printf("Signal with number %i has arrived\n",signumber);
#endif
}

float doWineProcess(char* szoloType){
    int pipefd[2];
    int pipefd2[2];
    pid_t pid;
    char sz[100];

    if (pipe(pipefd) == -1 || pipe(pipefd2) == -1)
    {
        perror("Hiba a pipe nyitaskor!");
        exit(EXIT_FAILURE);
    }
    pid = fork();
    if (pid == -1)
    {
        perror("Fork hiba");
        exit(EXIT_FAILURE);
    }

    if (pid == 0)
    {
        sleep(3);
        close(pipefd[1]);
        printf("Szolo megerkezett\n");
        read(pipefd[0], sz, sizeof(szoloType));
        printf("Szolo feldolgozasa: %s", sz);
        printf("\n");
        close(pipefd[0]);
        sleep(5);
        srand(time(NULL));
        double randomNum = (double)rand() / RAND_MAX;
        double result = 0.6 + randomNum * 0.2;
        printf("Az adott mennyisegbol %f liter keszult el.", result);

        close(pipefd2[0]);
        write(pipefd2[1], &result, sizeof(result));
        close(pipefd2[1]);
    }
    else
    {    // szulo process
        printf("Szallitjuk a %s szolot a feldolgozasra!\n", szoloType);
        close(pipefd[0]);
        write(pipefd[1], szoloType, sizeof(szoloType));
        close(pipefd[1]);
        fflush(NULL);
        wait(NULL);

        float result;
        close(pipefd2[1]);
        read(pipefd2[0], &result, sizeof(result));
        close(pipefd2[0]);

        printf("Szulo befejezte! Eredmeny: %f\n", result);
        return result;
    }
    return 0.0;

}

void processingWine() {
    szallitmany uj_szallitmany;

    getFloatInput("Mennyi az elfogadhato mennyiseg? (kg)", &uj_szallitmany.mennyiseg, 0);
    fflush(stdin);

    char tipus[50];
    getStringInput("Milyen tipust szeretne kuldeni", uj_szallitmany.fajta, 0);
    fflush(stdin);

    printf("Feldolgozas inicializalasa...\n");
    signal(SIGTERM,handler);

    pid_t child=fork();
    if (child>0)
    {
        pause();
        int status;
        wait(&status);
        printf("Szolokeszitesi folyamat keszen all!\n");
        float litres = doWineProcess(uj_szallitmany.fajta);
    }
    else
    {
        kill(getppid(),SIGTERM);
        printf("Feldolgozas felkeszules a fogadasra...\n");
        sleep(3);
    }
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
    populateInitFile();
    node *head = getSavedList("init.dat");
    int option = 0;
    do {
        printf("1 - List all\n");
        printf("2 - Filtered search (by wine region)\n");
        printf("3 - Add new record\n");
        printf("4 - Edit a record\n");
        printf("5 - Delete a record\n");
        printf("6 - Start makin' wineee\n");
        printf("7 - Quit\n");
        printf("Option: ");
        scanf("%d", &option);
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
                processingWine();
                break;
            case 7:
                break;
            default:
                printf("Hibas bemenet!\n");
                break;
        }
    } while (option != 7);
}
