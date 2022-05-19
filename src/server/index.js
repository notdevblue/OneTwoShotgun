const WebSocket = require("ws");

require("dotenv").config();

const wss = new WebSocket.Server({ port: process.env.PORT }, () => {
   
});