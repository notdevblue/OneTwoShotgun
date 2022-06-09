const WebSocket = require("ws");
const logger = require("../Utils/Logger.js");
const path = require("path");
const fs = require("fs");

// TODO: Logger Loglevel 에 따라 console.log 출력

class HanSocket {
   constructor(port) {
      this.port = port;
      this.wss;
      this.clients = [];
      this.handlers = [];
      this.id = 0;

      this.wss = new WebSocket.Server({ port: port }, () => {
         console.log(`[II] Server started on port ${port}.`);
      });
      
      // dynamic handler importing
      fs.readdir(path.join(".", "Handlers"), (err, file) => {
         file.forEach(e => {
            console.log(`[II] Found heandler ${e}`);
            const handler = require(path.join("..", "Handlers", e));
            this.addHandler(handler.type, handler.handle);
         });
      });      

      this.wss.on("listening", () => {
         console.log("[II] Server is listening");
      });
   }

   process() {
      this.wss.on("connection", ws => {
         // assign id to client
         ws.id = id++;
         this.clients[ws.id] = ws;

         console.log(`[II] New client joined. ID:{ws.id}`);

         ws.on("message", data => {
            let object = JSON.parse(data);
            let handle = this.handlers[object.type];
            
            if (handle == undefined) { // packet decoding error or no handler found
               logger(`[EE] Error packet pre-handling from client ${ws.id}\r\nPacket: ${data}`);
               return;
            }
            
            handle(object.payload);
         });

         ws.on("close", data => {
            // delete client data
            this.clients[ws.id] = null;
            this.clients.splice(ws.id, 1);
         });
      });
   }

   addHandler(type, handle) {
      this.handlers[type] = handle;
   }

   toJson(type, payload) {
      JSON.stringify({ type: type, payload: payload });
   }
   
}

module.exports = HanSocket;