# GiG SpecFlow Assignment

This project follows the assignment defined for the Software Developer in Test role.

## Setup

- Docker installation on the host machine
- Pull a Zookeeper image and initialise container

```bash
docker stop zookeeper kafka
docker rm zookeeper kafka
docker run --name zookeeper  -p 2181:2181 -d zookeeper
```
- Assign container IP in an environment variable
```bash
Zookeeper_Server_IP=$(docker inspect zookeeper --format='{{ .NetworkSettings.IPAddress }}')
```
- Pull a Kafka image and initialise container
```bash
docker run -p 9092:9092 --name kafka -e
KAFKA_ZOOKEEPER_CONNECT= ${Zookeeper_Server_IP}:2181 -e
KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092 -e
KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR=1 -d confluentinc/cp-kafka
```

## Installation

Use the package manager [pip](https://pip.pypa.io/en/stable/) to install foobar.

```bash
pip install foobar
```

## Usage

```python
import foobar

foobar.pluralize('word') # returns 'words'
foobar.pluralize('goose') # returns 'geese'
foobar.singularize('phenomena') # returns 'phenomenon'