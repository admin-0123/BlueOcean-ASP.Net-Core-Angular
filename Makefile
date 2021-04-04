BE:=./Api
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

#Setup
setup: install build-fe docker

docker:
	docker-compose up --build --remove-orphans

install:
	cd ${BE} && dotnet build
	cd ${FE} && npm install

#Docker
up:
	docker-compose up -d

stop:
	docker-compose stop

down:
	docker-compose down --remove-orphans

rebuild:
	docker-compose up -d --force-recreate --renew-anon-volumes --build

#Watch
watch: ; ${MAKE} -j2 watch-be watch-fe

watch-be:
	cd ${BE} && dotnet watch run

watch-fe:
	cd ${FE} && ng serve --watch

#Build
build: build-be build-fe

build-be:
	cd ${BE} && dotnet build

build-fe:
	cd ${FE} && ng build
