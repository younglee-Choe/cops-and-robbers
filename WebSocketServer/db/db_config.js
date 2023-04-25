const conn = {
    host: '',
    user: '',
    password: '',
    database: ''
};

// conn.connect();

// let sql = 'select * from topic';

// conn.query(sql, function(err, rows, fields) {
//     if(err) {
//         console.error("❗️error connecting:", + err.stack);
//     }
//     console.log(rows);
// });

// conn.end();

module.exports = conn;