FROM nvidia/cuda:12.1.1-runtime-ubuntu22.04

ENV PYTHONUNBUFFERED=1

# SYSTEM
RUN apt-get update --yes --quiet && DEBIAN_FRONTEND=noninteractive apt-get install --yes --quiet --no-install-recommends \
    software-properties-common \
    build-essential apt-utils \
    wget curl kmod \
#    nvidia-driver-545 \
 && rm -rf /var/lib/apt/lists/*

# PYTHON 3.10
RUN add-apt-repository --yes ppa:deadsnakes/ppa && apt-get update --yes --quiet
RUN DEBIAN_FRONTEND=noninteractive apt-get install --yes --quiet --no-install-recommends \
    python3.10 \
    python3.10-dev \
    python3.10-distutils \
    python3.10-lib2to3 \
    pip

RUN update-alternatives --install /usr/bin/python3 python3 /usr/bin/python3.10 999 \
    && update-alternatives --config python3 && ln -s /usr/bin/python3 /usr/bin/python

RUN pip install --upgrade pip

ENV PYTHONPATH "${PYTHONPATH}:/opt/processing_backend/"
COPY ./processing_backend/requirements/image_captioning.requirements.txt ./requirements.txt
RUN pip3 install --upgrade -r requirements.txt

WORKDIR /opt
COPY processing_backend processing_backend

EXPOSE 8082
CMD ["uvicorn", "processing_backend.backend.image_captioning.entrypoints.captioning_api:app","--host", "0.0.0.0", "--port", "8082"]
