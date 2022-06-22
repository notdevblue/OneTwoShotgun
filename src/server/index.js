const query    = require("./Utils/mysql-extensions.js");
const DataVO   = require("./VO/DataVO.js");

const hs = require("./HanSocket/HanSocket.js");

hs.process(ws => {
   ws.loggedin = false;

   const payload = JSON.stringify({
      id: ws.id
   });

   hs.send(ws, hs.toJson("init", payload));
}, ws => {
   if (ws.match != null)
      ws.match.leave(ws);
}, ["move", "fire"]);