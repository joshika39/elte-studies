cmake_minimum_required(VERSION 3.26)
project(Algo_II)

set(CMAKE_CXX_STANDARD 17)

add_executable(Algo_II main.cpp
        AVL/avl-tree.h
        AVL/avl-tree.cpp
        bPlus/b-plus.cpp
        bPlus/b-plus.h)
