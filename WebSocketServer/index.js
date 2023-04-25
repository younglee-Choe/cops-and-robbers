const WebSocket = require('ws');
const mysql = require('mysql');
const dbConfig = require('./db/db_config.js');
const conn = mysql.createConnection(dbConfig);
const port = 8080;

const wss = new WebSocket.Server({port: port}, () => {
    console.log("Server started!");
});

wss.on('connection', (ws) => {
    ws.on('message', (data) => {
        console.log(`❗️Data received; ${data}`);
        ws.send(data)
    });
    conn.connect();

    var sql = 'select * from topic';
    conn.query(sql, function(err, rows, fields) {
        if(err) {
            console.error("❗️error connecting:", + err.stack);
        }
        console.log(rows);
    });
    conn.end();
});

wss.on('listening', () => {
    console.log(`Server is listening on ${port}`);
});
