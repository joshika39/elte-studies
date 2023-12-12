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

const char *dot_str[] = {".", ".", ".", "\b\b\b   \b\b\b"};
#define countof(x) (sizeof(x)/sizeof((x)[0]))

static int next_state = 0;
void update_progress(void) {
    fputs(dot_str[next_state], stdout);
    next_state = (next_state + 1) % countof(dot_str);
    fflush(stdout);
}

static time_t last_time = 0;
void update_progress_if_time(void) {
    time_t now = time(NULL);
    if(now > last_time) {
        update_progress();
        last_time = now;
    }
}

void start_progress(const char *loading) {
    fputs(loading, stdout);
    next_state = 0;
    last_time = 0;
    fflush(stdout);
}

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

    fgets(buff, sizeof(char[100]), stdin);

    int len;
    len = strlen(buff);
    if(buff[len - 1] == '\n')
        buff[len - 1] = 0;

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

    char temp[50];
    fflush(stdin);
    fgets(temp, sizeof(char[100]), stdin);

    int len;
    len = strlen(temp);
    if (temp[len - 1] == '\n')
        temp[len - 1] = 0;

    *(varRef) = atof(temp);
    fflush(stdin);
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

node* filteredSearchType(node *head, char* type) {
    node *typesHead = NULL;
    while (head != NULL) {
        if (strcmp(head->data.fajta, type) == 0) {
            printf("%d;%s;%s;%.2f;%s\n", head->data.id, head->data.videk, head->data.nev, head->data.mennyiseg,
                   head->data.fajta);
            insertAtEnd(&typesHead, head->data);
        }
        head = head->next;
    }
    printf("\n");
    return typesHead;
}

float calculateSum(node* head){
    float sum = 0;
    while (head != NULL) {
        sum += head->data.mennyiseg;
        head = head->next;
    }

    return sum;
}

void handler(int signumber){
#if DEBUG
    printf("Signal with number %i has arrived\n",signumber);
#endif
}

void loader(char* text, int length){
    start_progress(text);
    for(int i = 0; i < length; i++) {
        update_progress();
        sleep(1);
    }
    fputs("DONE\n", stdout);
}

void doWineProcess(char* szoloType){
    printf("NSZT: Shipping grape %s for processing!\n", szoloType);

    loader("NSZT: Shipping", 3);

    pid_t pid;

    int p_c[2];
    if (pipe(p_c) == -1)
    {
        perror("Hiba a pipe nyitaskor!");
        exit(EXIT_FAILURE);
    }

    int c_p[2];
    if (pipe(c_p) == -1)
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
        // FELDOLGOZO
        char sz[100];
        close(p_c[1]);
        read(p_c[0], sz, sizeof(szoloType));
        close(p_c[0]);
        printf("WINERY: Grapes have arrived\n");
        printf("WINERY: Processing grape type: %s\n", sz);

        loader("WINERY: Processing", 7);

        srand(time(NULL));
        float randomNum = (float)rand() / RAND_MAX;
        float result = 0.6 + randomNum * 0.2;

        char convertedResult[64];
        int ret = snprintf(convertedResult, sizeof convertedResult, "%f", result);
        close(c_p[0]);
        write(c_p[1], convertedResult, sizeof(convertedResult));
        close(c_p[1]);
    }
    else
    {
        close(p_c[0]);
        write(p_c[1], szoloType, sizeof(szoloType));
        close(p_c[1]);

        int status;
        wait(&status);

        printf("NSZT: The grapes have arrived!\n");
        char result[100];
        close(c_p[1]);
        read(c_p[0], result, sizeof(result));
        close(c_p[0]);
        printf("NSZT: %sl wine was created\n", result);

    }
}

void processingWine(node* head) {
    char kg[10];
    getStringInput("NSZT: Enter the minimum grape mass (kg)", kg, 0);

    char tipus[50];
    getStringInput("Type", tipus, 0);

    node* filtered = filteredSearchType(head, tipus);

    float requiredKg = atof(kg);

    if(requiredKg > calculateSum(filtered)){
        printf("NSZT: Not enough grapes (%f < %f), come back later\n", calculateSum(filtered), requiredKg);
        return;
    }

    printf("NSZT: Received a total of %f grapes(%s)\n", calculateSum(filtered), tipus);

    signal(SIGTERM,handler);
    loader("NSZT: Preparing for shipping", 7);

    pid_t child=fork();
    if (child>0)
    {
        pause();
        int status;
        wait(&status);
        printf("WINERY: Wine processing ready to accept the grapes!\n");
        doWineProcess(tipus);
    }
    else
    {
        loader("NSZT: Preparation for receiving the grapes", 5);
        kill(getppid(),SIGTERM);
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
        char temp[10];
        getStringInput("Option", temp, 0);
        option = atoi(temp);
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
            case 7:
                break;
            default:
                printf("Wrong input\n");
                break;
        }
    } while (option != 7 && option != 6);

    if(option == 6){
        processingWine(head);
    }
}
