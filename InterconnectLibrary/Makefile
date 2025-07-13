.PHONY: build tests

build:
	cmake -S . -B build/ && cmake --build build/

build-tests:
	cmake -DBUILD_TESTING=ON -S . build/ && cmake --build build/

copy-shared-lib-to-backend:
	cp build/src/libinterconnect_shared.so ../InterconnectBackend/Interconnect/bin/Debug/net8.0/