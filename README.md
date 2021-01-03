# GiG SpecFlow Assignment

This project follows the assignment defined for the Software Developer in Test role.

## Environment Setup

- Docker installation (hyper-v feature and BIOS virtualisation enabled)
- Pull a Zookeeper image and initialise container

```bash
docker run --name zookeeper  -p 2181:2181 -d zookeeper
```
- Assign container IP in an environment variable (particularly if hosted remotely or localhost != 127.0.0.1)

```bash
Zookeeper_Server_IP=$(docker inspect zookeeper --format='{{ .NetworkSettings.IPAddress }}')
echo ${Zookeeper_Server_IP}
```
- Pull a Kafka image and initialise container

```bash
docker run -p 9092:9092 --name kafka -e
KAFKA_ZOOKEEPER_CONNECT=${Zookeeper_Server_IP}:2181 -e
KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092 -e
KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR=1 -d confluentinc/cp-kafka
```

## Installation/Build

- Clone [master branch](https://github.com/ianchetcuti/GigSpecFlowProject)
- Visual Studio [Specflow extension](https://docs.specflow.org/projects/getting-started/en/latest/GettingStarted/Step1.html)
- NuGet Package SpecFlow.Plus.LivingDocPlugin
- dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
- NuGet Package Confluent.Kafka

## Usage

In Kafka container CLI (root folder):

- View messages on topic

```bash
bin/kafka-console-consumer --bootstrap-server localhost:9092 --topic test --from-beginning
```

- Mark topic for deletion (and purge all messages)
```bash
bin/kafka-topics --zookeeper ${Zookeeper_Server_IP}:2181 --delete --topic test
```

## Future Improvements

- Find a more effecient way of breaking infinite loop when consuming kafka messages from topic
- Generate living doc as part of test wrap up