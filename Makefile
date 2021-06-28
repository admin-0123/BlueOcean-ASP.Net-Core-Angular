BE:=./Backend
FE:=./Frontend

include .env
export $(shell sed 's/=.*//' .env)

.PHONY: check

check:
	node --version
	dotnet --version
	docker --version
	docker-compose --version
	git --version

certs:
	dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p ${ASPNETCORE_Kestrel__Certificates__Default__Password}

#Docker main
build:
	docker-compose -f docker-compose.yml -f docker-compose.elastic.yml up --build --remove-orphans

up:
	docker-compose -f docker-compose.yml -f docker-compose.elastic.yml up -d

stop:
	docker-compose -f docker-compose.yml -f docker-compose.elastic.yml stop

down:
	docker-compose -f docker-compose.yml -f docker-compose.elastic.yml down --remove-orphans

rebuild:
	docker-compose -f docker-compose.yml -f docker-compose.elastic.yml up -d --force-recreate --build
