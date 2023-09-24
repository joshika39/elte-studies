//
// Created by JoshH on 2023/09/23.
//

#ifndef ALGO_II_AVL_TREE_H
#define ALGO_II_AVL_TREE_H

#include <string>

class Node {
public:
    int key;
    Node *left;
    Node *right;
    int balance;

    Node();
    Node(int key);
};

void balanceMM0(Node *&tree, Node *&left);
void balanceMMm(Node *&tree, Node *&left);
void balanceMMp(Node *&tree, Node *&left);
void balancePP0(Node *&tree, Node *&right);
void balancePPm(Node *&tree, Node *&right);
void balancePPp(Node *&tree, Node *&right);
void balancePP(Node *&t, bool &d);
void balanceMM(Node *&t, bool &d);

void leftSubtreeShrunk(Node *&t, bool &d);
void rightSubtreeShrunk(Node *&t, bool &d);
void leftSubtreeGrown(Node *&t, bool &d);
void rightSubtreeGrown(Node *&t, bool &d);

void AVLInsert(Node *&t, int k, bool &d);
void AVLRemMin(Node *&t, Node *&minP, bool &d);
void AVLRemMax(Node *&t, Node *&maxP, bool &d);

void printTree(Node *root, std::string prefix = "", bool isLeft = true);
void printBinaryTree(Node *root, std::string prefix = "", bool isLeft = true);
void printPlainTree(Node *root, int indent = 0);
void deleteTree(Node *root);

#endif //ALGO_II_AVL_TREE_H
