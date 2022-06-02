const WebSocket = require("ws");
const logger = require("../Utils/Logger.js");

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
   }

   listen() {
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
            let packet = this.handlers.find(x => x.type == object.type);
            
            if (packet == undefined) { // packet decoding error or no handler found
               logger(`Error packet pre-handling from client ${ws.id}\r\nPacket: ${data}`);
               return;
            }
            
            packet.handle(object.payload);
         });

         ws.on("close", data => {
            // delete client data
            this.clients[ws.id] = null;
            this.clients.splice(ws.id, 1);
         });
      });
   }

   addHandler(type, handle) {
      this.handlers.push({ type, handle });
   }

   toJson(type, payload) {
      JSON.stringify({ type: type, payload: payload });
   }
   
}

module.exports = HanSocket;