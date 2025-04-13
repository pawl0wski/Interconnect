cmake_minimum_required(VERSION 3.30)
project(library)

set(CMAKE_CXX_STANDARD 20)

add_library(library SHARED src/library.cpp)
