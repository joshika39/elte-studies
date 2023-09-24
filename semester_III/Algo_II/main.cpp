#include <iostream>
#include "AVL/avl-tree.h"
#include "bPlus/b-plus.h"

void gyak1_avlTree() {
    bool isBalanced = true;
    Node *root = nullptr;
    Node *min;
    AVLInsert(root, 7, isBalanced);
    AVLInsert(root, 3, isBalanced);
    AVLInsert(root, 1, isBalanced);
    AVLInsert(root, 5, isBalanced);
    AVLInsert(root, 9, isBalanced);
    AVLInsert(root, 15, isBalanced);
    AVLInsert(root, 19, isBalanced);
    printPlainTree(root);
    std::cout << std::endl;
    AVLRemMax(root, min, isBalanced);
    AVLRemMax(root, min, isBalanced);
    AVLRemMax(root, min, isBalanced);
    printPlainTree(root);
    printTree(root);
    deleteTree(root);
}

void gyak2_bPlusTree() {
    auto t = new bTree(4);
    t->insert(8);
    t->insert(7);
    t->insert(6);
    t->insert(11);
    t->insert(15);
    t->insert(16);
    t->insert(17);
    t->insert(18);
    t->insert(20);
//    t.insert(23);
    std::cout << "The B-tree is: \n";
    t->display();
}

int main() {
//    gyak1_avlTree();
    gyak2_bPlusTree();
    return 0;
}