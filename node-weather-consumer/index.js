const http = require('http');
const { Kafka } = require('kafkajs')
const config = require("./config")
const logger = require("./logger")

const kafka = new Kafka({
    clientId: "my-consumer",
    brokers: [config.host]
})

const consumer = kafka.consumer({ groupId: `${config.topic}-group-1` })

const run = async () => {

    await consumer.connect()
    await consumer.subscribe({ topic: config.topic, fromBeginning: true })

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