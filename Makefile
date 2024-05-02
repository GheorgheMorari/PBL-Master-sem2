PROJECT_PATH = $(shell pwd)

ENV_FILE := .env

-include .env

all: setup-dotenv-from-example build-externals start-common-infrastructure start-processing-backend

setup-dotenv-from-example:
	@ echo "Setting up .env file from .env.example..."
	@ echo "DOMAIN=$${ENV_DOMAIN:-localtest.me}\n" > .env;
	@ cat .env.example >> .env;
	@ echo "Setting up .env file from .env.example... Done";

build-externals:
	@ echo -e "Creating the necessary volumes, networks and folders and setting the special rights ..."
	@ docker network create public-net || true


start-mongodb:
	@ echo "Starting mongodb..."
	@ docker compose --env-file ./.env -f ./infra/mongodb/docker-compose.yml -p mongodb up -d --force-recreate --renew-anon-volumes

stop-mongodb:
	@ echo "Stopping mongodb..."
	@ docker compose --env-file ./.env -f ./infra/mongodb/docker-compose.yml -p mongodb down


start-traefik: build-externals
	@ echo -e "$(BUILD_PRINT)Starting the Traefik services $(END_BUILD_PRINT)"
	@ docker compose -p common --file ./infra/traefik/docker-compose.yml --env-file ${ENV_FILE} up -d

stop-traefik:
	@ echo -e "$(BUILD_PRINT)Stopping the Traefik services $(END_BUILD_PRINT)"
	@ docker compose -p common --file ./infra/traefik/docker-compose.yml --env-file ${ENV_FILE} down


start-minio:
	@ echo "Starting minio..."
	@ docker compose --env-file ./.env -f ./infra/minio/docker-compose.yml -p minio up -d

stop-minio:
	@ echo "Stopping minio..."
	@ docker compose --env-file ./.env -f ./infra/minio/docker-compose.yml -p minio down


start-processing-backend:
	@ echo "Starting processing backend..."
	@ docker compose --env-file ./.env -f ./infra/processing-backend/docker-compose.yml -p processing-backend up -d --force-recreate

stop-processing-backend:
	@ echo "Stopping processing backend..."
	@ docker compose --env-file ./.env -f ./infra/processing-backend/docker-compose.yml -p processing-backend down


start-common-infrastructure:
	@ echo "Starting local infrastructure..."
	@ make start-traefik
	@ make start-mongodb
	@ make start-minio

stop-common-infrastructure:
	@ echo "Stopping local infrastructure..."
	@ make stop-mongodb
	@ make stop-minio
	@ make stop-traefik

restart-common-infrastructure:
	@ echo "Restarting local infrastructure..."
	@ make stop-common-infrastructure
	@ make start-common-infrastructure
