#include <iostream>

using namespace std;

typedef struct Move {
    int From;
    int To;
} Move;

typedef struct Bucket {
    int Top;
    int Bottom;
} Bucket;

bool getNeighbour(int from, int size, Bucket *buckets, int &neighbour) {
    if (buckets[from].Bottom == 0) {
        return false;
    }

    for (int i = from + 1; i < size; i++) {
        if (buckets[i].Bottom != 0) {
            neighbour = i;
            return true;
        }
    }
    return false;
}

bool getDifference(Bucket b1, Bucket b2, int &diff){
    if((b1.Top == b2.Bottom) || (b1.Bottom == b2.Top)){
        return false;
    }
    int test1 = abs(b1.Top - b2.Bottom);
    int test2 = abs(b1.Bottom - b2.Top);

    if(test1 < test2){
        diff = test1;
    }
    else{
        diff = test2;
    }
    return true;
}

bool smallestGap(Bucket *buckets, int size, int &n1, int &n2) {
    if (size < 2) {
        return false;
    }
    int neighbour;
    bool success = true;
    int min = 1000;
    for (int i = 0; i < size - 1 && success; i++) {
        if (buckets[i].Bottom != 0) {
            success = getNeighbour(i, size, buckets, neighbour);
            int newVal;
            success = getDifference(buckets[i], buckets[neighbour], newVal);
            if (i != neighbour && newVal < min) {
                min = newVal;
                n1 = i, n2 = neighbour;
            }
        }
    }
    return success;
}

void printBuckets(Bucket *buckets, int size) {
    for (int i = 0; i < size; ++i) {
        if(buckets[i].Bottom == buckets[i].Top){
            printf("   ");
        }
        else{
            if(buckets[i].Top < 10){
                printf("%d  ", buckets[i].Top);
            }else{
                printf("%d ", buckets[i].Top);
            }
        }
    }

    cout << "\n";
    for (int i = 0; i < size; ++i) {
        if(buckets[i].Bottom == 0){
            printf("00 ");
        }
        else {
            if(buckets[i].Bottom < 10){
                printf("%d  ", buckets[i].Bottom);
            }else{
                printf("%d ", buckets[i].Bottom);
            }
        }
    }
    cout << "\n\n";
}

int main() {
    bool success = true;
    int bucketCount = 10;
    int input[] = {9, 7, 5, 3, 1, 2, 4, 6, 8, 10};

    Bucket buckets[bucketCount];
    for (int i = 0; i < bucketCount; ++i) {
        Bucket bucket;
        bucket.Bottom = input[i];
        bucket.Top = input[i];
        buckets[i] = bucket;
    }

    Move moves[bucketCount];

    for (int i = 0; i < bucketCount - 1 && success; ++i) {
        int n1, n2;
        success = smallestGap(buckets, bucketCount, n1, n2);

        Move tmpMove;
        if (buckets[n1].Top < buckets[n2].Bottom) {
            tmpMove.From = n1 + 1;
            tmpMove.To = n2 + 1;
            buckets[n2].Top = buckets[n1].Top;
            buckets[n1].Bottom = 0;
            buckets[n1].Top = 0;
        } else if (buckets[n2].Top < buckets[n1].Bottom) {
            tmpMove.From = n2 + 1;
            tmpMove.To = n1 + 1;
            buckets[n1].Top = buckets[n2].Top;
            buckets[n2].Bottom = 0;
            buckets[n2].Top = 0;
        }
        moves[i] = tmpMove;
        printBuckets(buckets, bucketCount);
    }
    if(success){
        for (int i = 0; i < bucketCount - 1; ++i) {
            Move curr = moves[i];
            cout << curr.From << " " << curr.To << "\n";
        }
    }
    else{
        cout << "0\n";
    }

    return 0;
}


