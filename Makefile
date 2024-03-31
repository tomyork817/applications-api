build:
	docker-compose build applications-api

run: build
	docker-compose up applications-api

