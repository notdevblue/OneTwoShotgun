const query    = require("./Utils/mysql-extensions.js");
const DataVO   = require("./VO/DataVO.js");

const hs =
   new require("./HanSocket/HanSocket.js")
      .HanSocket(process.env.PORT);

hs.process(ws => {
   const payload = JSON.stringify({
      id: ws.id
   });

   hs.send(ws, hs.toJson("init", payload));
});