# Cash Flow

It's a PoC for a Solutions Architecture challenge.

# Using Docker Compose

CashFlow runs from Docker for container orchestration.

Warning: It's only for local development environment.

## Getting Started

These instructions will cover usage information and for the docker container.

### Prerequisities

In order to run this container you'll need docker installed.

* [Windows](https://docs.docker.com/windows/started)
* [OS X](https://docs.docker.com/mac/started/)
* [Linux](https://docs.docker.com/linux/started/)

Also you'll need docker compose installed.

* [Windows/OS X/Linux](https://docs.docker.com/compose/)

### Usage

#### Shell Commands

Run your shell in the same directory as the `docker-compose.yml` file

Start:

```shell
docker-compose -f "docker-compose.yml" up -d
```

Stop:

```shell
docker-compose -f "docker-compose.yml" down
```

#### Access

* [CashFlowControl API](https://localhost:5000)
* [CashFlowControl Swagger](https://localhost:5000/swagger/index.html)
