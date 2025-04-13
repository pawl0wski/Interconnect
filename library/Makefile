.PHONY: build tests

build:
	cmake -S . -B build/ && cmake --build build/

build-tests:
	cmake -DBUILD_TESTING=ON -S . build/ && cmake --build build/
