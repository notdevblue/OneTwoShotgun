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

   process(connectionCallback) {
      this.wss.on("connection", ws => {
         
         // save ip address
         const ipAddr = req.headers["x-forwarded-for"] || req.headers.host;

         // assign id to client
         ws.id = id++;
         this.clients[ws.id] = ws;

         if (connectionCallback != null)
            connectionCallback(ws);

         console.log(`[II] New client joined. ID:${ws.id}`, ipAddr);

         ws.on("message", data => {
            let object;
            let handle;
            let keepGoing = true;

            try {
               object = JSON.parse(data);
            } catch (e) {
               object = undefined;
               logger(`[EE] Error packet JSON parsing.\t received packet: ${data} from `, ipAddr);
               keepGoing = false;
            }

            if (!keepGoing) return;
            
            handle = this.handlers[object.type];
            
            if (handle == undefined) { // packet decoding error or no handler found
               logger(`[EE] Error packet pre-handling from client ${ws.id}\r\nPacket: ${data}`, ipAddr);
               return;
            }
            
            handle(object.payload);
         });

         ws.on("close", (code, reason) => {
            // delete client data
            logger(`[II] Client disconnected, code: ${code}, reason: ${reason}`, ipAddr);
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