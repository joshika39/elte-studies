cmake_minimum_required(VERSION 3.26)
project(cpp)

set(CMAKE_CXX_STANDARD 14)

include_directories(gyak9/01)

add_executable(cpp
        gyak9/01/main.cpp
        gyak9/01/person.hpp)
