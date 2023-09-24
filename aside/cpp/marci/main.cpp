#include <iostream>

using namespace std;

typedef struct Move {
    int From;
    int To;
} Move;

typedef struct Bucket {
    int base;
    Bucket *top;
} Bucket;

int getHeight(Bucket* bucket){
    if(bucket == nullptr){
        return 0;
    }

    int i = 1;
    while (bucket->top != nullptr){
        bucket = bucket->top;
        i++;
    }
    return i;
}

int getHighestLevel(Bucket** buckets, int size){
    int maxHeight = getHeight(*(buckets));
    for (int i = 0; i < size; ++i) {
        int newHeight = getHeight(*(buckets + i));
        if(newHeight > maxHeight){
            maxHeight = newHeight;
        }
    }

    return maxHeight;
}

Bucket* getTop(Bucket* bucket){
    while (bucket->top != nullptr){
        bucket = bucket->top;
    }
    return bucket;
}

Bucket* getNth(Bucket* bucket, int n){
    auto h = getHeight(bucket);
    if(h < n || bucket == nullptr){
        return nullptr;
    }
    int i = 0;
    bool found = false;
    while (bucket->top != nullptr && i < n){
        found = true;
        bucket = bucket->top;
        i++;
    }
    if(found){
        return bucket;
    }
    return nullptr;
}

bool getNeighbour(int from, int size, Bucket **buckets, int &neighbour) {
    if (buckets[from] == nullptr) {
        return false;
    }

    for (int i = from + 1; i < size; i++) {
        if (buckets[i] != nullptr) {
            neighbour = i;
            return true;
        }
    }
    return false;
}

bool getDifference(Bucket b1, Bucket b2, int &diff){
    int test1 = abs(getTop(&b1)->base- b2.base);
    int test2 = abs(b1.base - getTop(&b2)->base);

    if(test1 < test2){
        diff = test1;
    }
    else{
        diff = test2;
    }
    return true;
}

bool smallestGap(Bucket **buckets, int size, int &n1, int &n2) {
    if (size < 2) {
        return false;
    }
    int neighbour;
    bool success = true;
    int min = 1000;
    for (int i = 0; i < size - 1 && success; i++) {
        Bucket* curr = buckets[i];
        if (curr != nullptr) {
            success = getNeighbour(i, size, buckets, neighbour);
            int newVal;
            auto nB = *(buckets + neighbour);
            success = getDifference(*curr, *nB, newVal);
            if (i != neighbour && newVal < min) {
                min = newVal;
                n1 = i, n2 = neighbour;
            }
        }
    }
    return success;
}

void printBucketsOnLevel(Bucket **buckets, int size, int level){
    for (int i = 0; i < size; ++i) {
        if(level == 0){
            if(buckets[i] == nullptr){
                printf("00 ");
            }
            else{
                if(buckets[i]->base < 10){
                    printf("%d  ", buckets[i]->base);
                }else{
                    printf("%d ", buckets[i]->base);
                }
            }
        }
        else{
            auto bucket = getNth(buckets[i], level);
            if(bucket == nullptr){
                printf("   ");
            }
            else{
                if(bucket->base < 10){
                    printf("%d  ", bucket->base);
                }else{
                    printf("%d ", bucket->base);
                }
            }
        }
    }
    cout << "\n";
}

void printBuckets(Bucket **buckets, int size) {
    int highestLevel = getHighestLevel(buckets, size);
    for (int i = highestLevel - 1; i >= 0; i--) {
        printBucketsOnLevel(buckets, size, i);
    }
    cout << "\n";
}

int main() {
    bool success = true;
    int bucketCount = 20;
    int input[] = {10, 13, 14, 12, 11, 19, 20, 18, 17, 16, 15, 9, 8, 6, 7, 5, 4, 3, 2, 1};

    auto buckets = new Bucket *[bucketCount];
    for (int i = 0; i < bucketCount; ++i) {
        auto bucket = new Bucket;
        bucket->base = input[i];
        bucket->top = nullptr;
        *(buckets + i) = bucket;
    }

    Move moves[bucketCount];

    for (int i = 0; i < bucketCount - 1 && success; ++i) {
        int n1, n2;
        success = smallestGap(buckets, bucketCount, n1, n2);

        Move tmpMove;
        auto b1 = buckets[n1];
        auto b2 = buckets[n2];
        if (getTop(b1)->base < b2->base) {
            tmpMove.From = n1 + 1;
            tmpMove.To = n2 + 1;
            buckets[n2]->top = b1;
            buckets[n1] = nullptr;
        } else if (getTop(b2)->base < b1->base) {
            tmpMove.From = n2 + 1;
            tmpMove.To = n1 + 1;
            buckets[n1]->top = b2;
            buckets[n2] = nullptr;
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


