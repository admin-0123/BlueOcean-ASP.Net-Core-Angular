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
	docker-compose up --build --remove-orphans

up:
	docker-compose up -d

stop:
	docker-compose stop

down:
	docker-compose down --remove-orphans

rebuild:
	docker-compose up -d --force-recreate --renew-anon-volumes --build

#Docker elastic stack
up-elastic:
	docker-compose up -f docker-compose.elastic.yaml -d

stop-elastic:
	docker-compose stop -f docker-compose.elastic.yaml

down-elastic:
	docker-compose down -f docker-compose.elastic.yaml --remove-orphans

rebuild-elastic:
	docker-compose up -f docker-compose.elastic.yaml -d --force-recreate --renew-anon-volumes --build
