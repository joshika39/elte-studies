#include <iostream>
#include <algorithm>

class Node {
public:
    int key;
    Node* left;
    Node* right;
    int balance;

    Node() {
        left = nullptr;
        right = nullptr;
        balance = 0;
    }

    Node(int key) {
        this->key = key;
        left = nullptr;
        right = nullptr;
        balance = 0;
    }
};


void balanceMMm(Node* &tree, Node* &left) {
    tree -> left = left -> right;
    left -> right = tree;
    left -> balance = 0;
    tree -> balance = 0;
    tree = left;
}

void balanceMMp(Node* &tree, Node* &left) {
    Node* right = left -> right;
    tree -> left = right -> right;
    left -> right = right -> left;
    right -> right = tree;
    right -> left = left;
    tree -> balance = (right -> balance + 1) / 2;
    right -> balance = (1 - right -> balance) / 2;
    right -> balance = 0;
    tree = right;
}

void balancePPm(Node* &tree, Node* &right) {
    Node* left = right -> left;
    tree -> right = left -> left;
    right -> left = left -> right;
    left -> left = tree;
    left -> right = right;
    tree -> balance = (left -> balance + 1) / 2;
    right -> balance = (1 - left -> balance) / 2;
    left -> balance = 0;
    tree = left;
}

void balancePPp(Node* &tree, Node* &right) {
    tree -> right = right -> left;
    right -> left = tree;
    right -> balance = 0;
    tree -> balance = 0;
    tree = right;
}

void leftSubtreeGrown(Node* &t, bool &d) {
    if(t -> balance == -1) {
        Node* left = t -> left;
        if(left -> balance == -1) {
            balanceMMm(t, left);
        }
        else{
            balanceMMp(t, left);
        }
        d = false;
    }
    else{
        t -> balance--;
        d = t -> balance < 0;
    }
}

void rightSubtreeGrown(Node* &t, bool &d) {
    if(t -> balance == 1) {
        Node* right = t -> right;
        if(right -> balance == 1) {
            balancePPp(t, right);
        }
        else {
            balancePPm(t, right);
        }
        d = false;
    }
    else{
        t -> balance++;
        d = t -> balance > 0;
    }
}

void AVLInsert(Node* &t, int k, bool &d) {
    if(t == nullptr) {
        if(k < t -> key) {
            AVLInsert(t -> left, k, d);
        }
        if(d) {
            leftSubtreeGrown(t, d);
        }

        if(k > t -> key) {
            AVLInsert(t -> right, k, d);
        }
         if(d) {
             rightSubtreeGrown(t, d);
         }
    }
    else{
        d = false;
    }
}

void printTree(Node* root, std::string prefix = "", bool isLeft = true) {
    if (root != nullptr) {
        std::cout << (isLeft ? "L--" : "R--");

        std::cout << root->key << " (Balance: " << root->balance << ")" << std::endl;

        // Recursively print the left and right subtrees
        printTree(root->left, prefix + (isLeft ? "|   " : "    "), true);
        printTree(root->right, prefix + (isLeft ? "|   " : "    "), false);
    }
}

void printBinaryTree(Node* root, std::string prefix = "", bool isLeft = true) {
    if (root != nullptr) {
        std::cout << (isLeft ? "L--" : "R--");
        std::cout << root->key << std::endl;

        // Recursively print the left and right subtrees
        printBinaryTree(root->left, prefix + (isLeft ? "|   " : "    "), true);
        printBinaryTree(root->right, prefix + (isLeft ? "|   " : "    "), false);
    }
}

void printPlainTree(Node* root, int indent = 0) {
    if (root != nullptr) {
        // Print right subtree with increased indentation
        printPlainTree(root->right, indent + 4);

        // Print the current node with appropriate indentation
        for (int i = 0; i < indent; i++) {
            std::cout << " ";
        }
        std::cout << root->key << "\n";

        // Print left subtree with increased indentation
        printPlainTree(root->left, indent + 4);
    }
}

void deleteTree(Node* root) {
    if (root != nullptr) {
        deleteTree(root->left);
        deleteTree(root->right);
        delete root;
    }
}

int main() {
    Node* root = new Node(10);
    root->left = new Node(5);
    root->right = new Node(15);
    root->left->left = new Node(2);
    root->left->right = new Node(7);

    printPlainTree(root);
    AVLInsert(root, 1);
    deleteTree(root);

    return 0;
}