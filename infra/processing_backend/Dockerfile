FROM python:3.10.12-slim-buster

ENV PYTHONPATH "${PYTHONPATH}:/opt/processing_backend/"

RUN apt-get update && apt-get install libgl1 libglib2.0-0 wget curl -y

COPY ../processing_backend/requirements/core.requirements.txt ./requirements.txt
RUN pip3 install --upgrade -r requirements.txt

WORKDIR /opt
COPY processing_backend processing_backend

EXPOSE 8082
CMD ["uvicorn", "processing_backend.backend.core.entrypoints.processing_backend:app","--host", "0.0.0.0", "--port", "8082"]
