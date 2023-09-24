//
// Created by JoshH on 2023/09/23.
//

#ifndef ALGO_II_B_PLUS_H
#define ALGO_II_B_PLUS_H

class bNode {
    int *keys;
    int d;
    bNode **children;
    int n;
    bool leaf;

public:
    bNode(int t1, bool leaf1);

    void insertNonFull(int k);

    void splitChild(int i, bNode *y);

    void display();

    void traverse();
    friend class bTree;

};

class bTree {
    bNode *root;
    int d;

public:
    explicit bTree(int _t);

    void display();
    void insert(int k);
    void traverse();
};


#endif //ALGO_II_B_PLUS_H
