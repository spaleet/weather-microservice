const http = require('http');
require('dotenv').config();
const { Kafka } = require('kafkajs')
const logger = require("./logger")

const kafka = new Kafka({
    clientId: "my-consumer",
    brokers: [process.env.KAFKA_HOST]
})

const consumer = kafka.consumer({ groupId: `${process.env.KAFKA_TOPIC}-group-1` })

const run = async () => {

    await consumer.connect()
    await consumer.subscribe({ topic: process.env.KAFKA_TOPIC, fromBeginning: true })

    await consumer.run({
        eachMessage: async ({ topic, partition, message }) => {

            const messageData = {
                key: message.key,
                value: message.value.toString()
            }

            logger.info(`Message received : Key: ${messageData.key} - Value: ${messageData.value}`);
        },
    })
}

run().catch(console.error)

const server = http.createServer();

const host = 'localhost';
const port = 8080;
server.listen(port, host, () => {
    console.log(`Server is running on http://${host}:${port}`);
});