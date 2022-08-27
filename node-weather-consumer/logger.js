const { createLogger, format, transports } = require('winston');
const path = require('path');

const logger = createLogger({
    level: 'info',
    format: format.json(),
    defaultMeta: { service: 'user-service' },
    transports: [
        new transports.File({ filename: path.join(__dirname, './logs/error.log'), level: 'error' }),
        new transports.File({ filename: path.join(__dirname, './logs/all.log') }),
        new transports.Console({
            format: format.colorize({
                all: true
            }),
        })
    ],
});

module.exports = logger;