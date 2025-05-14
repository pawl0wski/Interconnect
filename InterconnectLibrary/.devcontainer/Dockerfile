FROM mcr.microsoft.com/devcontainers/cpp:1-ubuntu-24.04

ENV DEBIAN_FRONTEND=noninteractive

# Instalacja narzędzi developerskich i Google Test
RUN apt-get update && apt-get install -y \
    build-essential \
    cmake \
    git \
    libvirt-dev \
    libpcap-dev \
    gdb \
    sudo \
    wget \
    unzip \
    pkg-config \
    libgtest-dev \
    libgmock-dev
