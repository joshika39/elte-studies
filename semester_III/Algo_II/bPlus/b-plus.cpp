//
// Created by JoshH on 2023/09/23.
//

#include <iostream>
#include <queue>
#include "b-plus.h"

bNode::bNode(int _d, bool _leaf) {
    d = _d;
    leaf = _leaf;
    keys = new int[d - 1];
    children = new bNode* [d];

    n = 0;
}

void bNode::display() {
    int i;
    for (i = 0; i < n; i++) {
        if (!leaf) {
            children[i]->display();
        }
        std::cout << " " << keys[i];
    }

    if (!leaf) {
        children[i]->display();
    }
}

void bNode::insertNonFull(int k) {
    int i = n - 1;

    if (leaf) {
        while (i >= 0 && keys[i] > k) {
            keys[i + 1] = keys[i];
            i--;
        }

        keys[i + 1] = k;
        n = n + 1;
    } else {
        while (i >= 0 && keys[i] > k)
            i--;

        if (children[i + 1]->n == d - 1) {
            splitChild(i + 1, children[i + 1]);

            if (keys[i + 1] < k)
                i++;
        }
        children[i + 1]->insertNonFull(k);
    }
}

void bNode::splitChild(int i, bNode *y) {
    auto *z = new bNode(y->d, y->leaf);
    z->n = d - 1;

    for (int j = 0; j < d - 1; j++)
        z->keys[j] = y->keys[j + d];

    if (!y->leaf) {
        for (int j = 0; j < d; j++)
            z->children[j] = y->children[j + d];
    }

    y->n = d - 1;
    for (int j = n; j >= i + 1; j--)
        children[j + 1] = children[j];

    children[i + 1] = z;

    for (int j = n - 1; j >= i; j--)
        keys[j + 1] = keys[j];

    keys[i] = y->keys[d - 1];
    n = n + 1;
}

bTree::bTree(int _d) {
    root = nullptr;
    d = _d;
}

void bTree::display() {
    if (root != nullptr) {
        root->display();
    }
}

void bTree::traverse() {
    if (root != nullptr) {
        root->traverse();
    }
}

void bTree::insert(int k) {
    if (root == nullptr) {
        root = new bNode(d, true);
        root->keys[0] = k;
        root->n = 1;
    } else {
        if (root->n == d - 1) {
            auto *s = new bNode(d, false);

            s->children[0] = root;
            s->splitChild(0, root);
            int i = 0;
            if (s->keys[0] < k)
            {
                i++;
            }
            s->children[i]->insertNonFull(k);

            root = s;
        } else
        {
            root->insertNonFull(k);
        }
    }
}

void bNode::traverse()
{
    std::queue<bNode*> queue;
    queue.push(this);
    while (!queue.empty())
    {
        auto current = queue.front();
        queue.pop();
        int i;
        for (i = 0; i < n; i++)
        {
            if (!leaf) {
                queue.push(current->children[i]);
            }
            std::cout << " " << current->keys[i];
        }
        std::cout << std::endl;
        if (!leaf) {
            queue.push(current->children[i]);
        }
    }
}